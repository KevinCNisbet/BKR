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

            BKRRegistration.BKRDailyProcessing(@"C:\Users\kevin\source\repos\BKR\BKR\Files\customers.json",
                @"C:\Users\kevin\source\repos\BKR\BKR\Files\contracts.json");

            //BKRRegistration.BKRDailyProcessing(@"C:\Users\kevin\source\repos\BKR\BKR\Files\customers update.json",
            //    @"C:\Users\kevin\source\repos\BKR\BKR\Files\contracts update.json");

            //BKRRegistration.BKRDailyProcessing("", 
            //    @"C:\Users\kevin\source\repos\BKR\BKR\Files\contracts update 2.json");
        }
    }
}