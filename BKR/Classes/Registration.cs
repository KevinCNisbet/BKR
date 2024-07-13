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
        
        public Registration(
        string transactionCode, string date, string participantNo, string participantNo2, string customer,
        string kredietnemernaam, string voorletters, string voornaam, string prefix, string geboortedatum,
        string geslacht, string straat, string huisnummer, string alfanumeriek1, string woonplaats,
        string postcode, string alfanumeriek2, string landCode, string geboortedatumNieuw, string contractsoort,
        string contract, string contractNieuw, string limietContractBedrag, string opnamebedrag,
        string datumEersteAflossing, string datumTLaatstAflossing, string datumPLaatstAflossing,
        string specialCode, string regRegistrDate, string jointContract, string newName,
        string codeRemovalReason, string bestandCode)
        {
            TransactionCode = transactionCode;
            Date = date;
            ParticipantNo = participantNo;
            ParticipantNo2 = participantNo2;
            Customer = customer;
            Kredietnemernaam = kredietnemernaam;
            Voorletters = voorletters;
            Voornaam = voornaam;
            Prefix = prefix;
            Geboortedatum = geboortedatum;
            Geslacht = geslacht;
            Straat = straat;
            Huisnummer = huisnummer;
            Alfanumeriek1 = alfanumeriek1;
            Woonplaats = woonplaats;
            Postcode = postcode;
            Alfanumeriek2 = alfanumeriek2;
            LandCode = landCode;
            GeboortedatumNieuw = geboortedatumNieuw;
            Contractsoort = contractsoort;
            Contract = contract;
            ContractNieuw = contractNieuw;
            LimietContractBedrag = limietContractBedrag;
            Opnamebedrag = opnamebedrag;
            DatumEersteAflossing = datumEersteAflossing;
            DatumTLaatstAflossing = datumTLaatstAflossing;
            DatumPLaatstAflossing = datumPLaatstAflossing;
            SpecialCode = specialCode;
            RegRegistrDate = regRegistrDate;
            JointContract = jointContract;
            NewName = newName;
            CodeRemovalReason = codeRemovalReason;
            BestandCode = bestandCode;
        }
    
        public Registration(BKRData data, BKRTransaction bKRTransaction)
        {
            TransactionCode = bKRTransaction.TransactionCode;
            Date = "";
            ParticipantNo = data.Deelnemernummer;
            ParticipantNo2 = "";
            Customer = "";
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
            ContractNieuw = "";
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
        public static List<Registration> GetAllRegistrations(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM tblRegistration";
                var registrations = connection.Query<Registration>(sql).AsList();
                return registrations;
            }
        }
    }
}
