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
            //CleanupDatabase();
        }

        [Fact]
        public void TestCustomersWrittenToDatabaseCorrectly()
        {
            // Set up expected results
            List<Customer> expectedCustomers = new();
            expectedCustomers.Add(new Customer("CUST001", "Joosten", "V", "", new DateTime(1980, 1, 1), "Zandweg", "", "31", "3233", "ET", "OOSTVORNE", "M", "NLD"));
            expectedCustomers.Add(new Customer("CUST002", "Mulder", "J", "van", new DateTime(1990, 2, 2), "De Matestraat", "", "293", "7447", "BA", "OVERIJSSEL", "V", "NLD"));
            expectedCustomers.Add(new Customer("CUST003", "Wel", "A", "de", new DateTime(2000, 3, 3), "Sint Michaelstraat", "A", "26", "5861", "BV", "LIMBURG", "V", "NLD"));

            // Load customers from JSON file
            List<Customer> actualCustomers = Customer.LoadCustomersFromJson(JsonFilePath);
            AssertEqualCustomers(expectedCustomers, actualCustomers);
            
            // Save customers to the database
            Customer.SaveCustomersToDatabase(expectedCustomers, Constants.SQL_CONNECTION_STRING);
            // Retrieve customers from the database
            actualCustomers = Customer.GetAllCustomers(Constants.SQL_CONNECTION_STRING);
            AssertEqualCustomers(expectedCustomers, actualCustomers);
        }
        private void AssertEqualCustomers(List<Customer> expectedCustomers, List<Customer> actualCustomers)
        {
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
