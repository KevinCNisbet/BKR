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
    public class CustomerTests : IDisposable
    {
        private const string JsonFilePath = @"C:\\Users\\kevin\\source\\repos\\BKR\\BKR\\Files\\customers.json";
        private const string JsonFilePath2 = @"C:\\Users\\kevin\\source\\repos\\BKR\\BKR\\Files\\customers update.json";

        // This method runs before each test
        public CustomerTests()
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
        public void TestCustomersWrittenToDatabaseCorrectly()
        {
            CleanupDatabase();

            TestData testData = new TestData();
            
            // File with 3 new customers
            List<Customer> expectedCustomers = new() {testData.customer1, testData.customer2, testData.customer3};

            // Load customers from JSON file
            List<Customer> actualCustomers = Customer.LoadCustomersFromJson(JsonFilePath);
            AssertEqualCustomers(expectedCustomers, actualCustomers);
            
            // Save and retrieve customers
            Customer.SaveCustomersToDatabase(expectedCustomers, Constants.SQL_CONNECTION_STRING);
            actualCustomers = Customer.GetAllCustomers(Constants.SQL_CONNECTION_STRING);
            AssertEqualCustomers(expectedCustomers, actualCustomers);

            // File with one new and one amended customer
            testData.customer1.Woonplaats = "DEN HAAG";
            testData.customer1.Straat = "Schapenlaan";
            testData.customer1.Postcode = "2512";
            testData.customer1.Alfanumeriek2 = "HT";
            testData.customer1.Huisnummer = "111";
            expectedCustomers = new() { testData.customer1, testData.customer4 };
            actualCustomers = Customer.LoadCustomersFromJson(JsonFilePath2);
            AssertEqualCustomers(expectedCustomers, actualCustomers);

            // Save and retrieve customers
            expectedCustomers = new() { testData.customer1, testData.customer2, testData.customer3, testData.customer4 };
            Customer.SaveCustomersToDatabase(expectedCustomers, Constants.SQL_CONNECTION_STRING);
            actualCustomers = Customer.GetAllCustomers(Constants.SQL_CONNECTION_STRING); AssertEqualCustomers(expectedCustomers, actualCustomers);

        }
        private void AssertEqualCustomers(List<Customer> expectedCustomers, List<Customer> actualCustomers)
        {
            Assert.Equal(expectedCustomers.Count, actualCustomers.Count);
            foreach (var expectedCustomer in expectedCustomers)
            {
                var actualCustomer = actualCustomers.SingleOrDefault(c => c.Customernummer == expectedCustomer.Customernummer);
                Assert.NotNull(actualCustomer);
                AssertEqualCustomer(expectedCustomer, actualCustomer);
            }
        }

        private void AssertEqualCustomer(Customer expected, Customer actual)
        {
            Assert.Equal(expected.Customernummer, actual.Customernummer);
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
            Assert.Equal(expected.Geslacht, actual.Geslacht);
            Assert.Equal(expected.LandCode, actual.LandCode);
        }

        private void CleanupDatabase()
        {
                SQLUtilities.SQLExecute("DELETE FROM tblCustomer");
        }
    }
}
