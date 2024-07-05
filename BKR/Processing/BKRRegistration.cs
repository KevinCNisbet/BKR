using BKR.Classes;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Globalization;

namespace BKR.Processing
{
    public class BKRRegistration
    {
        public static List<Registration> ConvertBKRDataToRegistrations(List<BKRData> bkrDataList)
        {
            var registrationList = new List<Registration>();

            foreach (var bkrData in bkrDataList)
            {
                var registration = new Registration
                {
                    TransactionCode = "TD", // No direct mapping
                    Date = bkrData.Registratiedatum?.ToString("yyyyMMdd") ?? "",
                    ParticipantNo = bkrData.Deelnemernummer,
                    Customer = "TODO", // No direct mapping
                    Kredietnemernaam = bkrData.Kredietnemernaam,
                    Voorletters = bkrData.Voorletters,
                    Voornaam = "TODO", // No direct mapping
                    Prefix = bkrData.Prefix,
                    Geboortedatum = bkrData.Geboortedatum.ToString("yyyyMMdd"),
                    Geslacht = bkrData.Geslacht,
                    Straat = bkrData.Straat,
                    Huisnummer = bkrData.Huisnummer,
                    Alfanumeriek1 = bkrData.Alfanumeriek1,
                    Woonplaats = bkrData.Woonplaats,
                    Postcode = bkrData.Postcode,
                    Alfanumeriek2 = bkrData.Alfanumeriek2,
                    LandCode = bkrData.LandCode,
                    GeboortedatumNieuw = "TODO", // No direct mapping
                    Contractsoort = bkrData.Contractsoort,
                    Contract = bkrData.Contract,
                    ContractNieuw = bkrData.Contractnummer,
                    LimietContractBedrag = ((int)Math.Floor(bkrData.LimietContractBedrag)).ToString(CultureInfo.InvariantCulture),
                    Opnamebedrag = ((int)Math.Floor(bkrData.Opnamebedrag)).ToString(CultureInfo.InvariantCulture),
                    DatumEersteAflossing = bkrData.DatumEersteAflossing?.ToString("yyyyMMdd") ?? "",
                    DatumTLaatstAflossing = bkrData.DatumTLaatsteAflossing?.ToString("yyyyMMdd") ?? "",
                    DatumPLaatstAflossing = bkrData.DatumPLaatsteAflossing?.ToString("yyyyMMdd") ?? "",
                    SpecialCode = "T", // No direct mapping
                    RegRegistrDate = bkrData.Registratiedatum?.ToString("yyyyMMdd") ?? "",
                    JointContract = "T", // No direct mapping
                    NewName = "TD", // No direct mapping
                    CodeRemovalReason = "TD", // No direct mapping
                    BestandCode = "TD" // No direct mapping
                };

                registrationList.Add(registration);
            }

            return registrationList;
        }

        public static void InsertRegistrations(List<Registration> registrations)
        {
            using (var connection = new SqlConnection(Constants.SQL_CONNECTION_STRING))
            {
                connection.Open();

                var sql = @"
INSERT INTO tblRegistration (
    TransactionCode,
    Date,
    ParticipantNo,
    Customer,
    Kredietnemernaam,
    Voorletters,
    Voornaam,
    Prefix,
    Geboortedatum,
    Geslacht,
    Straat,
    Huisnummer,
    Alfanumeriek1,
    Woonplaats,
    Postcode,
    Alfanumeriek2,
    LandCode,
    GeboortedatumNieuw,
    Contractsoort,
    Contract,
    ContractNieuw,
    LimietContractBedrag,
    Opnamebedrag,
    DatumEersteAflossing,
    DatumTLaatstAflossing,
    DatumPLaatstAflossing,
    SpecialCode,
    RegRegistrDate,
    JointContract,
    NewName,
    CodeRemovalReason,
    BestandCode
) VALUES (
    @TransactionCode,
    @Date,
    @ParticipantNo,
    @Customer,
    @Kredietnemernaam,
    @Voorletters,
    @Voornaam,
    @Prefix,
    @Geboortedatum,
    @Geslacht,
    @Straat,
    @Huisnummer,
    @Alfanumeriek1,
    @Woonplaats,
    @Postcode,
    @Alfanumeriek2,
    @LandCode,
    @GeboortedatumNieuw,
    @Contractsoort,
    @Contract,
    @ContractNieuw,
    @LimietContractBedrag,
    @Opnamebedrag,
    @DatumEersteAflossing,
    @DatumTLaatstAflossing,
    @DatumPLaatstAflossing,
    @SpecialCode,
    @RegRegistrDate,
    @JointContract,
    @NewName,
    @CodeRemovalReason,
    @BestandCode
)";


                connection.Execute(sql, registrations);
            }
        }
    }
}
