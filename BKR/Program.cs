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
            string filePath = @"C:\Users\kevin\source\repos\BKR\BKR\Files\customers.json";
            List<Customer> customers = LoadCustomersFromJson(filePath);
            SaveCustomersToDatabase(customers, Constants.SQL_CONNECTION_STRING);

            filePath = @"C:\Users\kevin\source\repos\BKR\BKR\Files\contracts.json";
            List<Contract> contracts = LoadContractsFromJson(filePath);
            SaveContractsToDatabase(contracts, Constants.SQL_CONNECTION_STRING);

            var bkrProcessor = new BKRProcessor();
            var bkrList = bkrProcessor.CombineData(customers, contracts);
            BKRRepository.InsertBKRList(bkrList);
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
                foreach (var contract in contracts)
                {
                    string sql = @"
                    INSERT INTO tblContract (
                        Customer1, Customer2, Contractnummer, Contractsoort,
                        Deelnemernummer, Registratiedatum, LimietContractBedrag, Opnamebedrag,
                        DatumEersteAflossing, DatumTLaatsteAflossing, DatumPLaatsteAflossing,
                        IndicatieBKRAfgelost, NumberOfPaymentsMissed
                    ) VALUES (
                        @Customer1, @Customer2, @Contractnummer, @Contractsoort,
                        @Deelnemernummer, @Registratiedatum, @LimietContractBedrag, @Opnamebedrag,
                        @DatumEersteAflossing, @DatumTLaatsteAflossing, @DatumPLaatsteAflossing,
                        @IndicatieBKRAfgelost, @NumberOfPaymentsMissed
                    )";
                    connection.Execute(sql, contract);
                }
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
                connection.Open();
                foreach (var customer in customers)
                {
                    string sql = @"
                    INSERT INTO tblCustomer (
                        Customer, Kredietnemernaam, Voorletters, Prefix, Geboortedatum,
                        Straat, Huisnummer, Alfanumeriek1, Postcode, Alfanumeriek2,
                        Woonplaats, Geslacht, LandCode
                    ) VALUES (
                        @Customernummer, @Kredietnemernaam, @Voorletters, @Prefix, @Geboortedatum,
                        @Straat, @Huisnummer, @Alfanumeriek1, @Postcode, @Alfanumeriek2,
                        @Woonplaats, @Geslacht, @LandCode
                    )";
                    connection.Execute(sql, customer);
                }
            }
        }
    }
}