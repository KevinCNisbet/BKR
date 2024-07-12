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
            // Set up expected results
            List<Contract> expectedContracts = new() { testData.contract1, testData.contract2, testData.contract3 };

            // Load Contracts from JSON file
            List<Contract> actualContracts = Contract.LoadContractsFromJson(JsonFilePath);
            AssertEqualContracts(expectedContracts, actualContracts);

            // Save Contracts to the database
            Contract.SaveContractsToDatabase(expectedContracts, Constants.SQL_CONNECTION_STRING);
            // Retrieve Contracts from the database
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
