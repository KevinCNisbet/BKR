using BKR.Classes;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Globalization;

namespace BKR.Processing
{
    public class BKRRegistration
    {
        public static void CompareAndRegisterChanges(string connectionString)
        {
            Contract contract = new();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var masterData = connection.Query<BKRData>("SELECT * FROM tblBKR_Master").ToList();
                var deltaData = connection.Query<BKRData>("SELECT * FROM tblBKR_Delta").ToList();

                foreach (var delta in deltaData)
                {
                    var master = masterData.Find(m => m.Contract == delta.Contract && m.Geboortedatum == delta.Geboortedatum);

                    if (master == null)
                    {
                        // No matching row in tblBKRMaster, insert with TransactionCode 01
                        new Registration(delta, new BKRTransaction("01", "")).InsertRegistration(connection);
                    }
                    else
                    {
                        if (delta.Contractnummer != contract.Contractnummer)
                        { contract = BKR.Classes.Contract.GetContract(Constants.SQL_CONNECTION_STRING, delta.Contractnummer); }
                        // Matching row found, check for differences
                        List<BKRTransaction> bKRTransactions = master.DetermineBKRTransactions(delta, contract);
                        foreach (var bKRTransaction in bKRTransactions)
                        {
                            new Registration(delta, bKRTransaction).InsertRegistration(connection);
                        }
                    }
                }
                connection.Execute("drop table tblBKR_Master;");
                connection.Execute("EXEC sp_rename 'tblBKR_Delta', 'tblBKR_Master';");
                connection.Execute("Select Top 0 * into tblBKR_Delta from tblBKR_Master;");
            }
        }
    }
}