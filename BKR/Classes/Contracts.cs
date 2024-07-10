using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
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
        public string IndicatieSpecialCode { get; set; }

        public Contract() { }
        public static Contract GetContract(string connectionString, string contractNummer)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM tblContract WHERE Contractnummer = @Contractnummer";
                return connection.QueryFirstOrDefault<Contract>(sql, new { Contractnummer = contractNummer });
            }
        }
        public static List<Contract> LoadContractsFromJson(string filePath)
        {
            string jsonData = System.IO.File.ReadAllText(filePath);
            List<Contract> contracts = JsonConvert.DeserializeObject<List<Contract>>(jsonData);
            return contracts;
        }
        public static List<Contract> GetAllContracts(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM tblContract";
                var contracts = connection.Query<Contract>(sql).AsList();
                return contracts;
            }
        }

        public static void SaveContractsToDatabase(List<Contract> contracts, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"
            MERGE tblContract AS target
            USING (VALUES 
                (@Customer1, @Customer2, @Contractnummer, @Contractsoort,
                 @Deelnemernummer, @LimietContractBedrag, @Opnamebedrag,
                 @DatumEersteAflossing, @DatumTLaatsteAflossing, @DatumPLaatsteAflossing,
                 @IndicatieBKRAfgelost, @NumberOfPaymentsMissed)
            ) AS source (Customer1, Customer2, Contractnummer, Contractsoort,
                         Deelnemernummer, LimietContractBedrag, Opnamebedrag,
                         DatumEersteAflossing, DatumTLaatsteAflossing, DatumPLaatsteAflossing,
                         IndicatieBKRAfgelost, NumberOfPaymentsMissed)
            ON target.Contractnummer = source.Contractnummer
            WHEN MATCHED THEN
                UPDATE SET 
                    Customer1 = source.Customer1,
                    Customer2 = source.Customer2,
                    Contractsoort = source.Contractsoort,
                    Deelnemernummer = source.Deelnemernummer,
                    LimietContractBedrag = source.LimietContractBedrag,
                    Opnamebedrag = source.Opnamebedrag,
                    DatumEersteAflossing = source.DatumEersteAflossing,
                    DatumTLaatsteAflossing = source.DatumTLaatsteAflossing,
                    DatumPLaatsteAflossing = source.DatumPLaatsteAflossing,
                    IndicatieBKRAfgelost = source.IndicatieBKRAfgelost,
                    NumberOfPaymentsMissed = source.NumberOfPaymentsMissed
            WHEN NOT MATCHED THEN
                INSERT (Customer1, Customer2, Contractnummer, Contractsoort,
                        Deelnemernummer, LimietContractBedrag, Opnamebedrag,
                        DatumEersteAflossing, DatumTLaatsteAflossing, DatumPLaatsteAflossing,
                        IndicatieBKRAfgelost, NumberOfPaymentsMissed)
                VALUES (source.Customer1, source.Customer2, source.Contractnummer, source.Contractsoort,
                        source.Deelnemernummer, source.LimietContractBedrag, source.Opnamebedrag,
                        source.DatumEersteAflossing, source.DatumTLaatsteAflossing, source.DatumPLaatsteAflossing,
                        source.IndicatieBKRAfgelost, source.NumberOfPaymentsMissed);";

                connection.Execute(sql, contracts);
            }
        }
    }
}
