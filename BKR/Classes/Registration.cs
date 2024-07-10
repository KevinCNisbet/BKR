using Microsoft.Data.SqlClient;
using Dapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKR.Classes
{
    public class Registration
    {
        public string TransactionCode { get; set; }
        public string Date { get; set; }
        public string ParticipantNo { get; set; }
        public string ParticipantNo2 { get; set; }
        public string Customer { get; set; }
        public string Kredietnemernaam { get; set; }
        public string Voorletters { get; set; }
        public string Voornaam { get; set; }
        public string Prefix { get; set; }
        public string Geboortedatum { get; set; }
        public string Geslacht { get; set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
        public string Alfanumeriek1 { get; set; }
        public string Woonplaats { get; set; }
        public string Postcode { get; set; }
        public string Alfanumeriek2 { get; set; }
        public string LandCode { get; set; }
        public string GeboortedatumNieuw { get; set; }
        public string Contractsoort { get; set; }
        public string Contract { get; set; }
        public string ContractNieuw { get; set; }
        public string LimietContractBedrag { get; set; }
        public string Opnamebedrag { get; set; }
        public string DatumEersteAflossing { get; set; }
        public string DatumTLaatstAflossing { get; set; }
        public string DatumPLaatstAflossing { get; set; }
        public string SpecialCode { get; set; }
        public string RegRegistrDate { get; set; }
        public string JointContract { get; set; }
        public string NewName { get; set; }
        public string CodeRemovalReason { get; set; }
        public string BestandCode { get; set; }
        public Registration(BKRData data, BKRTransaction bKRTransaction)
        {
            TransactionCode = bKRTransaction.TransactionCode;
            Date = "";
            ParticipantNo = data.Deelnemernummer;
            ParticipantNo2 = "";
            Kredietnemernaam = data.Kredietnemernaam;
            Voorletters = data.Voorletters;
            Voornaam = "";
            Prefix = data.Prefix;
            Geboortedatum = data.Geboortedatum.ToString("yyyyMMdd");
            GeboortedatumNieuw = "";
            Geslacht = data.Geslacht;
            Straat = data.Straat;
            Huisnummer = data.Huisnummer;
            Alfanumeriek1 = data.Alfanumeriek1;
            Woonplaats = data.Woonplaats;
            Postcode = data.Postcode;
            Alfanumeriek2 = data.Alfanumeriek2;
            LandCode = data.LandCode;
            Contractsoort = data.Contractsoort;
            Contract = data.Contract;
            LimietContractBedrag = ((int)Math.Floor(data.LimietContractBedrag)).ToString(CultureInfo.InvariantCulture);
            Opnamebedrag = ((int)Math.Floor(data.Opnamebedrag)).ToString(CultureInfo.InvariantCulture);
            DatumEersteAflossing = data.DatumEersteAflossing?.ToString("yyyyMMdd") ?? "";
            DatumTLaatstAflossing = data.DatumTLaatsteAflossing?.ToString("yyyyMMdd") ?? "";
            DatumPLaatstAflossing = data.DatumPLaatsteAflossing?.ToString("yyyyMMdd") ?? "";
            SpecialCode = bKRTransaction.SpecialCode;
            RegRegistrDate = "";
            JointContract = ""; // No direct mapping
            NewName = ""; // No direct mapping
            CodeRemovalReason = ""; // No direct mapping
            BestandCode = "DE"; // No direct mapping
        }

        public void InsertRegistration(SqlConnection connection)
        {
            string sql = @"
            INSERT INTO tblRegistration (
                TransactionCode, Date, ParticipantNo, ParticipantNo2, Customer, Kredietnemernaam, 
                Voorletters, Voornaam, Prefix, Geboortedatum, Geslacht, Straat, Huisnummer, Alfanumeriek1, 
                Woonplaats, Postcode, Alfanumeriek2, LandCode, GeboortedatumNieuw, Contractsoort, Contract, 
                ContractNieuw, LimietContractBedrag, Opnamebedrag, DatumEersteAflossing, DatumTLaatstAflossing, 
                DatumPLaatstAflossing, SpecialCode, RegRegistrDate, JointContract, NewName, CodeRemovalReason, BestandCode
            ) VALUES (
                @TransactionCode, @Date, @ParticipantNo, @ParticipantNo2, @Customer, @Kredietnemernaam, 
                @Voorletters, @Voornaam, @Prefix, @Geboortedatum, @Geslacht, @Straat, @Huisnummer, @Alfanumeriek1, 
                @Woonplaats, @Postcode, @Alfanumeriek2, @LandCode, @GeboortedatumNieuw, @Contractsoort, @Contract, 
                @ContractNieuw, @LimietContractBedrag, @Opnamebedrag, @DatumEersteAflossing, @DatumTLaatstAflossing, 
                @DatumPLaatstAflossing, @SpecialCode, @RegRegistrDate, @JointContract, @NewName, @CodeRemovalReason, @BestandCode
            )";

            connection.Execute(sql, this);
        }
    }
}
