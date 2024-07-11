using Microsoft.Data.SqlClient;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BKR.Classes
{
    public class Customer
    {
        public string Customernummer { get; set; }
        public string Kredietnemernaam { get; set; }
        public string Voorletters { get; set; }
        public string Prefix { get; set; }
        public DateTime Geboortedatum { get; set; }
        public string Straat { get; set; }
        public string Alfanumeriek1 { get; set; }
        public string Huisnummer { get; set; }
        public string Postcode { get; set; }
        public string Alfanumeriek2 { get; set; }
        public string Woonplaats { get; set; }
        public string Geslacht { get; set; }
        public string LandCode { get; set; }
        public Customer() { }
        public Customer(string customernummer, string kredietnemernaam, string voorletters, string prefix, DateTime geboortedatum,
                        string straat, string alfanumeriek1, string huisnummer, string postcode, string alfanumeriek2,
                        string woonplaats, string geslacht, string landCode)
        {
            Customernummer = customernummer;
            Kredietnemernaam = kredietnemernaam;
            Voorletters = voorletters;
            Prefix = prefix;
            Geboortedatum = geboortedatum;
            Straat = straat;
            Alfanumeriek1 = alfanumeriek1;
            Huisnummer = huisnummer;
            Postcode = postcode;
            Alfanumeriek2 = alfanumeriek2;
            Woonplaats = woonplaats;
            Geslacht = geslacht;
            LandCode = landCode;
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

    }
}
