using Microsoft.Data.SqlClient;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKR.Classes
{
    public class BKRData
    {
        public string Contract { get; set; }
        public string Kredietnemernaam { get; set; }
        public string Voorletters { get; set; }
        public string Prefix { get; set; }
        public DateTime Geboortedatum { get; set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
        public string Alfanumeriek1 { get; set; }
        public string Postcode { get; set; }
        public string Alfanumeriek2 { get; set; }
        public string Woonplaats { get; set; }
        public string Contractnummer { get; set; }
        public string Contractsoort { get; set; }
        public string Deelnemernummer { get; set; }
        public DateTime? Registratiedatum { get; set; }
        public DateTime? DatumLaatsteMutatie { get; set; }
        public decimal LimietContractBedrag { get; set; }
        public decimal Opnamebedrag { get; set; }
        public DateTime? DatumEersteAflossing { get; set; }
        public DateTime? DatumTLaatsteAflossing { get; set; }
        public DateTime? DatumPLaatsteAflossing { get; set; }
        public string IndicatieBKRAfgelost { get; set; }
        public string IndicatieAchterstCode { get; set; }
        //public string AchterstCode1 { get; set; }
        //public DateTime? DatumAchterstCode1 { get; set; }
        //public string AchterstCode2 { get; set; }
        //public DateTime? DatumAchterstCode2 { get; set; }
        //public string AchterstCode3 { get; set; }
        //public DateTime? DatumAchterstCode3 { get; set; }
        //public string AchterstCode4 { get; set; }
        //public DateTime? DatumAchterstCode4 { get; set; }
        //public string AchterstCode5 { get; set; }
        //public DateTime? DatumAchterstCode5 { get; set; }
        //public string AchterstCode6 { get; set; }
        //public DateTime? DatumAchterstCode6 { get; set; }
        //public string AchterstCode7 { get; set; }
        //public DateTime? DatumAchterstCode7 { get; set; }
        public string Geslacht { get; set; }
        public string LandCode { get; set; }

        public BKRData() {}
            public BKRData(Customer customer, Contract contract)
        {
            Contract = contract.Contractnummer;
            Kredietnemernaam = customer.Kredietnemernaam;
            Voorletters = customer.Voorletters;
            Prefix = customer.Prefix;
            Geboortedatum = customer.Geboortedatum;
            Straat = customer.Straat;
            Huisnummer = customer.Huisnummer;
            Alfanumeriek1 = customer.Alfanumeriek1;
            Postcode = customer.Postcode;
            Alfanumeriek2 = customer.Alfanumeriek2;
            Woonplaats = customer.Woonplaats;
            Contractnummer = contract.Contractnummer;
            Contractsoort = contract.Contractsoort;
            Deelnemernummer = contract.Deelnemernummer;
            LimietContractBedrag = contract.LimietContractBedrag;
            Opnamebedrag = contract.Opnamebedrag;
            DatumEersteAflossing = contract.DatumEersteAflossing;
            DatumTLaatsteAflossing = contract.DatumTLaatsteAflossing;
            DatumPLaatsteAflossing = contract.DatumPLaatsteAflossing;
            IndicatieBKRAfgelost = contract.IndicatieBKRAfgelost;
            Geslacht = customer.Geslacht;
            LandCode = customer.LandCode;
        }
        public List<BKRTransaction> DetermineBKRTransactions(BKRData delta, Contract contract)
        {
            List<BKRTransaction> bKRTransactions = new();
            if (this.Straat != delta.Straat ||
                   this.Huisnummer != delta.Huisnummer ||
                   this.Alfanumeriek1 != delta.Alfanumeriek1 ||
                   this.Postcode != delta.Postcode ||
                   this.Alfanumeriek2 != delta.Alfanumeriek2 ||
                   this.Woonplaats != delta.Woonplaats
                   )
            {
                bKRTransactions.Add(new BKRTransaction("05", ""));
            }
            if (this.DatumTLaatsteAflossing != delta.DatumTLaatsteAflossing)
            {
                bKRTransactions.Add(new BKRTransaction("09", ""));
            }
            if (this.DatumEersteAflossing != delta.DatumEersteAflossing)
            {
                bKRTransactions.Add(new BKRTransaction("12", ""));
            }
            if (contract.IndicatieBKRAfgelost == "Y" && this.IndicatieBKRAfgelost == "N")
            {
                //this.IndicatieBKRAfgelost = "Y";
                bKRTransactions.Add(new BKRTransaction("02", ""));
            }

            if (contract.NumberOfPaymentsMissed >= 3 && contract.IndicatieSpecialCode != "A")
            {
                delta.IndicatieAchterstCode = "A";
                //contract.IndicatieSpecialCode = "A";
                UpdateBKRTable(Constants.SQL_CONNECTION_STRING, contract.Contractnummer, "A");
                bKRTransactions.Add(new BKRTransaction("03", "A"));
            }

            if (contract.NumberOfPaymentsMissed == 0 && contract.IndicatieSpecialCode == "A")
            {
                delta.IndicatieAchterstCode = "";
                //contract.IndicatieSpecialCode= "";
                UpdateBKRTable(Constants.SQL_CONNECTION_STRING, contract.Contractnummer, "");
                bKRTransactions.Add(new BKRTransaction("03", "H"));
            }
            return bKRTransactions;
        }
        public static void UpdateBKRTable(string connectionString, string contractNummer, string newIndicatieAchterstCode)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string updateQuery = @"
                    UPDATE tblBKR_Delta
                    SET IndicatieAchterstCode = @IndicatieAchterstCode
                    WHERE Contractnummer = @Contractnummer;";

                var parameters = new
                {
                    IndicatieAchterstCode = newIndicatieAchterstCode,
                    Contractnummer = contractNummer
                };

                int rowsAffected = connection.Execute(updateQuery, parameters);

                updateQuery = @"
                    UPDATE tblContract
                    SET IndicatieSpecialCode = @IndicatieAchterstCode
                    WHERE Contractnummer = @Contractnummer;";

                parameters = new
                {
                    IndicatieAchterstCode = newIndicatieAchterstCode,
                    Contractnummer = contractNummer
                };

                rowsAffected = connection.Execute(updateQuery, parameters);
            }
        }
       
        public static void InsertBKRList(List<BKRData> bkrList, string BKRFile)
        {
            using (var connection = new SqlConnection(Constants.SQL_CONNECTION_STRING))
            {
                connection.Open();

                var sql = "INSERT INTO " + BKRFile + @"
                    (Contract, Kredietnemernaam, Voorletters, Prefix, Geboortedatum, Straat, Huisnummer, 
                    Alfanumeriek1, Postcode, Alfanumeriek2, Woonplaats, Contractnummer, Contractsoort, 
                    Deelnemernummer, Registratiedatum, DatumLaatsteMutatie, LimietContractBedrag, 
                    Opnamebedrag, DatumEersteAflossing, DatumTLaatsteAflossing, DatumPLaatsteAflossing, 
                    IndicatieBKRAfgelost, IndicatieAchterstCode,
                    --AchterstCode1, DatumAchterstCode1, AchterstCode2, DatumAchterstCode2, 
                    --AchterstCode3, DatumAchterstCode3, AchterstCode4, DatumAchterstCode4, AchterstCode5, 
                    --DatumAchterstCode5, AchterstCode6, DatumAchterstCode6, AchterstCode7, DatumAchterstCode7, 
                    Geslacht, LandCode)
                VALUES (
                    @Contract, @Kredietnemernaam, @Voorletters, @Prefix, @Geboortedatum, @Straat, @Huisnummer, 
                    @Alfanumeriek1, @Postcode, @Alfanumeriek2, @Woonplaats, @Contractnummer, @Contractsoort, 
                    @Deelnemernummer, @Registratiedatum, @DatumLaatsteMutatie, @LimietContractBedrag, 
                    @Opnamebedrag, @DatumEersteAflossing, @DatumTLaatsteAflossing, @DatumPLaatsteAflossing, 
                    @IndicatieBKRAfgelost, @IndicatieAchterstCode,
                    --@AchterstCode1, @DatumAchterstCode1, @AchterstCode2, @DatumAchterstCode2, 
                    --@AchterstCode3, @DatumAchterstCode3, @AchterstCode4, @DatumAchterstCode4, @AchterstCode5, 
                    --@DatumAchterstCode5, @AchterstCode6, @DatumAchterstCode6, @AchterstCode7, @DatumAchterstCode7, 
                    @Geslacht, @LandCode);";

                connection.Execute(sql, bkrList);
            }
        }
    }
}