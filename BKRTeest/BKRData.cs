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
            //CleanupDatabase();
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
            CleanupDatabase();
            TestData testData = new TestData();
            // Set up expected results
            List<BKRData> expectedBKRData = new() { testData.bkrData1, testData.bkrData1_2, testData.bkrData2, testData.bkrData3 };

            BKRRegistration.BKRDailyProcessing(JsonFilePathCusts, JsonFilePathContracts);

            List<BKRData> actualBKRData = BKRData.GetAllBKRData(Constants.SQL_CONNECTION_STRING);
            AssertEqualBKRData(expectedBKRData, actualBKRData);

            List<Registration> expectedRegistrations = new() { testData.registration1, testData.registration2, testData.registration3, testData.registration4 };
            List<Registration> actualRegistrations = Registration.GetAllRegistrations(Constants.SQL_CONNECTION_STRING);

            AssertEqualRegistration(expectedRegistrations, actualRegistrations);
        }
        //[Fact]
        // TODO Test on combining
        //public void TestRegistrationDataWrittenToDatabaseCorrectly()
        //{
        //    TestData testData = new TestData();
        //    // Set up expected results
        //    List<Registration> expectedRegistrations = new() { testData.registration1, testData.registration2, testData.registration3, testData.registration4 };
        //    List<Registration> actualRegistrations = Registration.GetAllRegistrations(Constants.SQL_CONNECTION_STRING);

        //    AssertEqualRegistration(expectedRegistrations, actualRegistrations);
        //}
        private void AssertEqualBKRData(List<BKRData> expectedBKRDatas, List<BKRData> actualBKRDatas)
        {
            Assert.Equal(expectedBKRDatas.Count, actualBKRDatas.Count);
            for (int i = 0; i < expectedBKRDatas.Count; i++)
            {
                AssertEqualBKRData(expectedBKRDatas[i], actualBKRDatas[i]);
            }
        }

        private void AssertEqualRegistration(List<Registration> expectedRegistrations, List<Registration> actualRegistrations)
        {
            Assert.Equal(expectedRegistrations.Count, actualRegistrations.Count);
            for (int i = 0; i < expectedRegistrations.Count; i++)
            {
                AssertEqualRegistration(expectedRegistrations[i], actualRegistrations[i]);
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

        private void AssertEqualRegistration(Registration expected, Registration actual)
        {
            Assert.Equal(expected.TransactionCode, actual.TransactionCode);
            Assert.Equal(expected.Date, actual.Date);
            Assert.Equal(expected.ParticipantNo, actual.ParticipantNo);
            Assert.Equal(expected.ParticipantNo2, actual.ParticipantNo2);
            Assert.Equal(expected.Customer, actual.Customer);
            Assert.Equal(expected.Kredietnemernaam, actual.Kredietnemernaam);
            Assert.Equal(expected.Voorletters, actual.Voorletters);
            Assert.Equal(expected.Voornaam, actual.Voornaam);
            Assert.Equal(expected.Prefix, actual.Prefix);
            Assert.Equal(expected.Geboortedatum, actual.Geboortedatum);
            Assert.Equal(expected.Geslacht, actual.Geslacht);
            Assert.Equal(expected.Straat, actual.Straat);
            Assert.Equal(expected.Huisnummer, actual.Huisnummer);
            Assert.Equal(expected.Alfanumeriek1, actual.Alfanumeriek1);
            Assert.Equal(expected.Woonplaats, actual.Woonplaats);
            Assert.Equal(expected.Postcode, actual.Postcode);
            Assert.Equal(expected.Alfanumeriek2, actual.Alfanumeriek2);
            Assert.Equal(expected.LandCode, actual.LandCode);
            Assert.Equal(expected.GeboortedatumNieuw, actual.GeboortedatumNieuw);
            Assert.Equal(expected.Contractsoort, actual.Contractsoort);
            Assert.Equal(expected.Contract, actual.Contract);
            Assert.Equal(expected.ContractNieuw, actual.ContractNieuw);
            Assert.Equal(expected.LimietContractBedrag, actual.LimietContractBedrag);
            Assert.Equal(expected.Opnamebedrag, actual.Opnamebedrag);
            Assert.Equal(expected.DatumEersteAflossing, actual.DatumEersteAflossing);
            Assert.Equal(expected.DatumTLaatstAflossing, actual.DatumTLaatstAflossing);
            Assert.Equal(expected.DatumPLaatstAflossing, actual.DatumPLaatstAflossing);
            Assert.Equal(expected.SpecialCode, actual.SpecialCode);
            Assert.Equal(expected.RegRegistrDate, actual.RegRegistrDate);
            Assert.Equal(expected.JointContract, actual.JointContract);
            Assert.Equal(expected.NewName, actual.NewName);
            Assert.Equal(expected.CodeRemovalReason, actual.CodeRemovalReason);
            Assert.Equal(expected.BestandCode, actual.BestandCode);
        }


        private void CleanupDatabase()
        {
            SQLUtilities.ClearTables();
        }
    }
}
