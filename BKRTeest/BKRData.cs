using Xunit;
using BKR.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Data.SqlClient;
using Dapper;
using BKR.Utilities;
using BKR.Processing;

namespace BKRTest
{
    public class BKRDataTests : IDisposable
    {
        private const string JsonFilePathContracts = @"C:\\Users\\kevin\\source\\repos\\BKR\\BKR\\Files\\Contracts.json";
        private const string JsonFilePathCusts = @"C:\\Users\\kevin\\source\\repos\\BKR\\BKR\\Files\\Customers.json";

        // This method runs before each test
        public BKRDataTests()
        {
            // Ensure the database is clean before running tests
            CleanupDatabase();
        }

        // This method runs after each test
        public void Dispose()
        {
            // Ensure the database is clean after running tests
            //CleanupDatabase();
        }

        [Fact]
        // TODO Test on combining
        public void TestBKRDataWrittenToDatabaseCorrectly()
        {
            TestData testData = new TestData();
            // Set up expected results
            List<BKRData> expectedBKRData = new() { testData.bkrData1, testData.bkrData1_2, testData.bkrData2, testData.bkrData3 };

            BKRRegistration.BKRDailyProcessing(JsonFilePathCusts, JsonFilePathContracts);

            List<BKRData> actualBKRData = BKRData.GetAllBKRData(Constants.SQL_CONNECTION_STRING);
            AssertEqualContracts(expectedBKRData, actualBKRData);
        }
        private void AssertEqualContracts(List<BKRData> expectedBKRDatas, List<BKRData> actualBKRDatas)
        {
            Assert.Equal(expectedBKRDatas.Count, actualBKRDatas.Count);
            foreach (var expectedBKRData in expectedBKRDatas)
            {
                // TODO: Would be better to have an ID rather than DoB matching
                var actualBKRData = actualBKRDatas.SingleOrDefault(c => c.Contractnummer == expectedBKRData.Contractnummer && c.Geboortedatum == expectedBKRData.Geboortedatum);
                Assert.NotNull(actualBKRData);
                AssertEqualBKRData(expectedBKRData, actualBKRData);
            }
        }

        private void AssertEqualBKRData(BKRData expected, BKRData actual)
        {
            Assert.Equal(expected.Contract, actual.Contract);
            Assert.Equal(expected.Kredietnemernaam, actual.Kredietnemernaam);
            Assert.Equal(expected.Voorletters, actual.Voorletters);
            Assert.Equal(expected.Prefix, actual.Prefix);
            Assert.Equal(expected.Geboortedatum, actual.Geboortedatum);
            Assert.Equal(expected.Straat, actual.Straat);
            Assert.Equal(expected.Huisnummer, actual.Huisnummer);
            Assert.Equal(expected.Alfanumeriek1, actual.Alfanumeriek1);
            Assert.Equal(expected.Postcode, actual.Postcode);
            Assert.Equal(expected.Alfanumeriek2, actual.Alfanumeriek2);
            Assert.Equal(expected.Woonplaats, actual.Woonplaats);
            Assert.Equal(expected.Contractnummer, actual.Contractnummer);
            Assert.Equal(expected.Contractsoort, actual.Contractsoort);
            Assert.Equal(expected.Deelnemernummer, actual.Deelnemernummer);
            Assert.Equal(expected.Registratiedatum, actual.Registratiedatum);
            Assert.Equal(expected.DatumLaatsteMutatie, actual.DatumLaatsteMutatie);
            Assert.Equal(expected.LimietContractBedrag, actual.LimietContractBedrag);
            Assert.Equal(expected.Opnamebedrag, actual.Opnamebedrag);
            Assert.Equal(expected.DatumEersteAflossing, actual.DatumEersteAflossing);
            Assert.Equal(expected.DatumTLaatsteAflossing, actual.DatumTLaatsteAflossing);
            Assert.Equal(expected.DatumPLaatsteAflossing, actual.DatumPLaatsteAflossing);
            Assert.Equal(expected.IndicatieBKRAfgelost, actual.IndicatieBKRAfgelost);
            Assert.Equal(expected.IndicatieAchterstCode, actual.IndicatieAchterstCode);
            Assert.Equal(expected.Geslacht, actual.Geslacht);
            Assert.Equal(expected.LandCode, actual.LandCode);
        }

        private void CleanupDatabase()
        {
            SQLUtilities.ClearTables();
        }
    }
}
