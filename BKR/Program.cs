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
            List<Customer> customers = Customer.LoadCustomersFromJson(filePath);
            Customer.SaveCustomersToDatabase(customers, Constants.SQL_CONNECTION_STRING);

            filePath = @"C:\Users\kevin\source\repos\BKR\BKR\Files\contracts.json";
            List<Contract> contracts = Contract.LoadContractsFromJson(filePath);
            Contract.SaveContractsToDatabase(contracts, Constants.SQL_CONNECTION_STRING);

            customers = Customer.GetAllCustomers(Constants.SQL_CONNECTION_STRING);
            contracts = Contract.GetAllContracts(Constants.SQL_CONNECTION_STRING);

            var bkrList = BKRProcessor.CombineData(customers, contracts);
            BKRData.InsertBKRList(bkrList, "tblBKR_Delta");

            BKRRegistration.CompareAndRegisterChanges(Constants.SQL_CONNECTION_STRING);


            filePath = @"C:\Users\kevin\source\repos\BKR\BKR\Files\customers update.json";
            customers = Customer.LoadCustomersFromJson(filePath);
            Customer.SaveCustomersToDatabase(customers, Constants.SQL_CONNECTION_STRING);

            filePath = @"C:\Users\kevin\source\repos\BKR\BKR\Files\contracts update.json";
            contracts = Contract.LoadContractsFromJson(filePath);
            Contract.SaveContractsToDatabase(contracts, Constants.SQL_CONNECTION_STRING);

            customers = Customer.GetAllCustomers(Constants.SQL_CONNECTION_STRING);
            contracts = Contract.GetAllContracts(Constants.SQL_CONNECTION_STRING);

            bkrList = BKRProcessor.CombineData(customers, contracts);
            BKRData.InsertBKRList(bkrList, "tblBKR_Delta");

            BKRRegistration.CompareAndRegisterChanges(Constants.SQL_CONNECTION_STRING);

            
            filePath = @"C:\Users\kevin\source\repos\BKR\BKR\Files\contracts update 2.json";
            contracts = Contract.LoadContractsFromJson(filePath);
            Contract.SaveContractsToDatabase(contracts, Constants.SQL_CONNECTION_STRING);

            customers = Customer.GetAllCustomers(Constants.SQL_CONNECTION_STRING);
            contracts = Contract.GetAllContracts(Constants.SQL_CONNECTION_STRING);

            bkrList = BKRProcessor.CombineData(customers, contracts);
            BKRData.InsertBKRList(bkrList, "tblBKR_Delta");

            BKRRegistration.CompareAndRegisterChanges(Constants.SQL_CONNECTION_STRING);

        }
    }
}