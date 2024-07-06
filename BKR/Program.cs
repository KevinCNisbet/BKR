using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using BKR.Classes;
using static System.Net.WebRequestMethods;
using BKR.Processing;



namespace BKR
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Utilities.SQLUtilities.ClearTables();

            string filePath = @"C:\Users\kevin\source\repos\BKR\BKR\Files\customers.json";
            List<Customer> customers = LoadCustomersFromJson(filePath);
            SaveCustomersToDatabase(customers, Constants.SQL_CONNECTION_STRING);

            filePath = @"C:\Users\kevin\source\repos\BKR\BKR\Files\customers update.json";
            customers = LoadCustomersFromJson(filePath);
            SaveCustomersToDatabase(customers, Constants.SQL_CONNECTION_STRING);


            filePath = @"C:\Users\kevin\source\repos\BKR\BKR\Files\contracts.json";
            List<Contract> contracts = LoadContractsFromJson(filePath);
            SaveContractsToDatabase(contracts, Constants.SQL_CONNECTION_STRING);

            filePath = @"C:\Users\kevin\source\repos\BKR\BKR\Files\contracts update.json";
            contracts = LoadContractsFromJson(filePath);
            SaveContractsToDatabase(contracts, Constants.SQL_CONNECTION_STRING);

            customers = GetAllCustomers(Constants.SQL_CONNECTION_STRING);
            contracts = GetAllContracts(Constants.SQL_CONNECTION_STRING);

            var bkrList = BKRProcessor.CombineData(customers, contracts);
            BKRRepository.InsertBKRList(bkrList, "tblBKR_Delta");

            var RegistrationList = BKRRegistration.ConvertBKRDataToRegistrations(bkrList);

            BKRRegistration.InsertRegistrations(RegistrationList);
        }
        public static List<Customer> GetAllCustomers(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM tblCustomer";
                var customers = connection.Query<Customer>(sql).AsList();
                return customers;
            }
        }
        public static List<Contract> LoadContractsFromJson(string filePath)
        {
            string jsonData = System.IO.File.ReadAllText(filePath);
            List<Contract> contracts = JsonConvert.DeserializeObject<List<Contract>>(jsonData);
            return contracts;
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
        public static List<Customer> LoadCustomersFromJson(string filePath)
        {
            string jsonData = System.IO.File.ReadAllText(filePath);
            List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(jsonData);
            return customers;
        }

        public static void SaveCustomersToDatabase(List<Customer> customers, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string sql = @"
            MERGE tblCustomer AS target
            USING (VALUES 
                (@Customernummer, @Kredietnemernaam, @Voorletters, @Prefix, @Geboortedatum,
                 @Straat, @Huisnummer, @Alfanumeriek1, @Postcode, @Alfanumeriek2,
                 @Woonplaats, @Geslacht, @LandCode)
            ) AS source (Customernummer, Kredietnemernaam, Voorletters, Prefix, Geboortedatum,
                         Straat, Huisnummer, Alfanumeriek1, Postcode, Alfanumeriek2,
                         Woonplaats, Geslacht, LandCode)
            ON target.Customernummer = source.Customernummer
            WHEN MATCHED THEN
                UPDATE SET 
                    Kredietnemernaam = source.Kredietnemernaam,
                    Voorletters = source.Voorletters,
                    Prefix = source.Prefix,
                    Geboortedatum = source.Geboortedatum,
                    Straat = source.Straat,
                    Huisnummer = source.Huisnummer,
                    Alfanumeriek1 = source.Alfanumeriek1,
                    Postcode = source.Postcode,
                    Alfanumeriek2 = source.Alfanumeriek2,
                    Woonplaats = source.Woonplaats,
                    Geslacht = source.Geslacht,
                    LandCode = source.LandCode
            WHEN NOT MATCHED THEN
                INSERT (Customernummer, Kredietnemernaam, Voorletters, Prefix, Geboortedatum,
                        Straat, Huisnummer, Alfanumeriek1, Postcode, Alfanumeriek2,
                        Woonplaats, Geslacht, LandCode)
                VALUES (source.Customernummer, source.Kredietnemernaam, source.Voorletters, source.Prefix, source.Geboortedatum,
                        source.Straat, source.Huisnummer, source.Alfanumeriek1, source.Postcode, source.Alfanumeriek2,
                        source.Woonplaats, source.Geslacht, source.LandCode);";
                connection.Execute(sql, customers);

            }
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
    }
}