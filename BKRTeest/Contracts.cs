using Xunit;
using BKR.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Data.SqlClient;
using Dapper;
using BKR.Utilities;

namespace BKRTest
{
    public class ContractTests : IDisposable
    {
        private const string JsonFilePath = @"C:\\Users\\kevin\\source\\repos\\BKR\\BKR\\Files\\Contracts.json";
        private const string JsonFilePath2 = @"C:\\Users\\kevin\\source\\repos\\BKR\\BKR\\Files\\Contracts update.json";
        private const string JsonFilePath3 = @"C:\\Users\\kevin\\source\\repos\\BKR\\BKR\\Files\\Contracts update 2.json";


        // This method runs before each test
        public ContractTests()
        {
            // Ensure the database is clean before running tests
            CleanupDatabase();
        }

        // This method runs after each test
        public void Dispose()
        {
            // Ensure the database is clean after running tests
            CleanupDatabase();
        }

        [Fact]
        public void TestContractsWrittenToDatabaseCorrectly()
        {
            TestData testData = new TestData();
            // Load file of new contracts
            List<Contract> expectedContracts = new() { testData.contract1, testData.contract2, testData.contract3 };
            List<Contract> actualContracts = Contract.LoadContractsFromJson(JsonFilePath);
            AssertEqualContracts(expectedContracts, actualContracts);

            // Save and retrieve contracts from database
            Contract.SaveContractsToDatabase(expectedContracts, Constants.SQL_CONNECTION_STRING);
            actualContracts = Contract.GetAllContracts(Constants.SQL_CONNECTION_STRING);
            AssertEqualContracts(expectedContracts, actualContracts);

            // Change expected results
            expectedContracts.Add(testData.contract4);
            testData.contract1.IndicatieBKRAfgelost = "Y";
            testData.contract2.DatumEersteAflossing = new DateTime(2023, 03, 28);
            testData.contract2.DatumTLaatsteAflossing = new DateTime(2024, 03, 15);
            testData.contract2.NumberOfPaymentsMissed = 1;
            testData.contract3.NumberOfPaymentsMissed = 3;
            // Process JSON file of contract changes
            actualContracts = Contract.LoadContractsFromJson(JsonFilePath2);
            AssertEqualContracts(expectedContracts, actualContracts);

            // Save and retrieve from database
            Contract.SaveContractsToDatabase(expectedContracts, Constants.SQL_CONNECTION_STRING);
            actualContracts = Contract.GetAllContracts(Constants.SQL_CONNECTION_STRING);
            AssertEqualContracts(expectedContracts, actualContracts);

            // Change expected results
            testData.contract4.NumberOfPaymentsMissed = 3;
            testData.contract3.NumberOfPaymentsMissed = 0;
            expectedContracts = new() { testData.contract3, testData.contract4 };
            // Process JSON file of contract changes
            actualContracts = Contract.LoadContractsFromJson(JsonFilePath3);
            AssertEqualContracts(expectedContracts, actualContracts);

            // Save and retrieve from database
            Contract.SaveContractsToDatabase(expectedContracts, Constants.SQL_CONNECTION_STRING);
            expectedContracts = new() { testData.contract1, testData.contract2, testData.contract3, testData.contract4 }; 
            actualContracts = Contract.GetAllContracts(Constants.SQL_CONNECTION_STRING);
            AssertEqualContracts(expectedContracts, actualContracts);
        }
        private void AssertEqualContracts(List<Contract> expectedContracts, List<Contract> actualContracts)
        {
            Assert.Equal(expectedContracts.Count, actualContracts.Count);
            foreach (var expectedContract in expectedContracts)
            {
                var actualContract = actualContracts.SingleOrDefault(c => c.Contractnummer == expectedContract.Contractnummer);
                Assert.NotNull(actualContract);
                AssertEqualContract(expectedContract, actualContract);
            }
        }

        private void AssertEqualContract(Contract expected, Contract actual)
        {
            Assert.Equal(expected.Customer1, actual.Customer1);
            Assert.Equal(expected.Customer2, actual.Customer2);
            Assert.Equal(expected.Contractnummer, actual.Contractnummer);
            Assert.Equal(expected.Contractsoort, actual.Contractsoort);
            Assert.Equal(expected.Deelnemernummer, actual.Deelnemernummer);
            Assert.Equal(expected.LimietContractBedrag, actual.LimietContractBedrag);
            Assert.Equal(expected.Opnamebedrag, actual.Opnamebedrag);
            Assert.Equal(expected.DatumEersteAflossing, actual.DatumEersteAflossing);
            Assert.Equal(expected.DatumTLaatsteAflossing, actual.DatumTLaatsteAflossing);
            Assert.Equal(expected.DatumPLaatsteAflossing, actual.DatumPLaatsteAflossing);
            Assert.Equal(expected.IndicatieBKRAfgelost, actual.IndicatieBKRAfgelost);
            Assert.Equal(expected.NumberOfPaymentsMissed, actual.NumberOfPaymentsMissed);
            Assert.Equal(expected.IndicatieSpecialCode, actual.IndicatieSpecialCode);
        }

        private void CleanupDatabase()
        {
            SQLUtilities.SQLExecute("DELETE FROM tblContract");
        }
    }
}
