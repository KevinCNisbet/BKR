using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKR.Classes
{
    public class Contract
    {
        public string Customer1 { get; set; }
        public string Customer2 { get; set; }
        public string Contractnummer { get; set; }
        public string Contractsoort { get; set; }
        public string Deelnemernummer { get; set; }
        public decimal LimietContractBedrag { get; set; }
        public decimal Opnamebedrag { get; set; }
        public DateTime DatumEersteAflossing { get; set; }
        public DateTime DatumTLaatsteAflossing { get; set; }
        public DateTime DatumPLaatsteAflossing { get; set; }
        public string IndicatieBKRAfgelost { get; set; }
        public decimal NumberOfPaymentsMissed { get; set; }


        public static Contract GetContract(string connectionString, string contractNummer)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM tblContract WHERE Contractnummer = @Contractnummer";
                return connection.QueryFirstOrDefault<Contract>(sql, new { Contractnummer = contractNummer });
            }
        }
    }
    }
