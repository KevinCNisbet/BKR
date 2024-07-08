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
        public static void CompareAndRegisterChanges(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var masterData = connection.Query<BKRData>("SELECT * FROM tblBKR_Master").AsList();
                var deltaData = connection.Query<BKRData>("SELECT * FROM tblBKR_Delta").AsList();

                foreach (var delta in deltaData)
                {
                    var master = masterData.Find(m => m.Contract == delta.Contract && m.Geboortedatum == delta.Geboortedatum);

                    if (master == null)
                    {
                        // No matching row in tblBKRMaster, insert with TransactionCode 01
                        InsertRegistration(connection, CreateRegistration(delta, new BKRTransaction("01", "")));
                    }
                    else
                    {

                        // Matching row found, check for differences
                        if (!IsEqual(master, delta))
                        {
                            // There are differences, insert with TransactionCode 02
                            List<BKRTransaction> bKRTransactions = master.DetermineBKRTransactions(delta);
                            foreach (var bKRTransaction in bKRTransactions)
                            {
                                InsertRegistration(connection, CreateRegistration(delta, bKRTransaction)); ;
                            }
                        }
                    }
                }
                connection.Execute("drop table tblBKR_Master;");
                connection.Execute("EXEC sp_rename 'tblBKR_Delta', 'tblBKR_Master';");
                connection.Execute("Select Top 0 * into tblBKR_Delta from tblBKR_Master;");
            }
        }


        private static bool IsEqual(BKRData master, BKRData delta)
        {
            return master.Kredietnemernaam == delta.Kredietnemernaam &&
                   master.Voorletters == delta.Voorletters &&
                   master.Prefix == delta.Prefix &&
                   master.Straat == delta.Straat &&
                   master.Huisnummer == delta.Huisnummer &&
                   master.Alfanumeriek1 == delta.Alfanumeriek1 &&
                   master.Postcode == delta.Postcode &&
                   master.Alfanumeriek2 == delta.Alfanumeriek2 &&
                   master.Woonplaats == delta.Woonplaats &&
                   master.Contractnummer == delta.Contractnummer &&
                   master.Contractsoort == delta.Contractsoort &&
                   master.Deelnemernummer == delta.Deelnemernummer &&
                   master.Registratiedatum == delta.Registratiedatum &&
                   master.DatumLaatsteMutatie == delta.DatumLaatsteMutatie &&
                   master.LimietContractBedrag == delta.LimietContractBedrag &&
                   master.Opnamebedrag == delta.Opnamebedrag &&
                   master.DatumEersteAflossing == delta.DatumEersteAflossing &&
                   master.DatumTLaatsteAflossing == delta.DatumTLaatsteAflossing &&
                   master.DatumPLaatsteAflossing == delta.DatumPLaatsteAflossing &&
                   master.IndicatieBKRAfgelost == delta.IndicatieBKRAfgelost &&
                   master.AchterstCode1 == delta.AchterstCode1 &&
                   master.DatumAchterstCode1 == delta.DatumAchterstCode1 &&
                   master.AchterstCode2 == delta.AchterstCode2 &&
                   master.DatumAchterstCode2 == delta.DatumAchterstCode2 &&
                   master.AchterstCode3 == delta.AchterstCode3 &&
                   master.DatumAchterstCode3 == delta.DatumAchterstCode3 &&
                   master.AchterstCode4 == delta.AchterstCode4 &&
                   master.DatumAchterstCode4 == delta.DatumAchterstCode4 &&
                   master.AchterstCode5 == delta.AchterstCode5 &&
                   master.DatumAchterstCode5 == delta.DatumAchterstCode5 &&
                   master.AchterstCode6 == delta.AchterstCode6 &&
                   master.DatumAchterstCode6 == delta.DatumAchterstCode6 &&
                   master.AchterstCode7 == delta.AchterstCode7 &&
                   master.DatumAchterstCode7 == delta.DatumAchterstCode7 &&
                   master.Geslacht == delta.Geslacht &&
                   master.LandCode == delta.LandCode;
        }

        private static Registration CreateRegistration(BKRData data, BKRTransaction bKRTransaction)
        {
            return new Registration
            {
                TransactionCode = bKRTransaction.TransactionCode,
                Date = "",
                ParticipantNo = data.Deelnemernummer,
                ParticipantNo2 = "",
                Kredietnemernaam = data.Kredietnemernaam,
                Voorletters = data.Voorletters,
                Voornaam = "",
                Prefix = data.Prefix,
                Geboortedatum = data.Geboortedatum.ToString("yyyMMdd"),
                GeboortedatumNieuw = "",
                Geslacht = data.Geslacht,
                Straat = data.Straat,
                Huisnummer = data.Huisnummer,
                Alfanumeriek1 = data.Alfanumeriek1,
                Woonplaats = data.Woonplaats,
                Postcode = data.Postcode,
                Alfanumeriek2 = data.Alfanumeriek2,
                LandCode = data.LandCode,
                Contractsoort = data.Contractsoort,
                Contract = data.Contract,
                LimietContractBedrag = ((int)Math.Floor(data.LimietContractBedrag)).ToString(CultureInfo.InvariantCulture),
                Opnamebedrag = ((int)Math.Floor(data.Opnamebedrag)).ToString(CultureInfo.InvariantCulture),
                DatumEersteAflossing = data.DatumEersteAflossing?.ToString("yyyyMMdd") ?? "",
                DatumTLaatstAflossing = data.DatumTLaatsteAflossing?.ToString("yyyyMMdd") ?? "",
                DatumPLaatstAflossing = data.DatumPLaatsteAflossing?.ToString("yyyyMMdd") ?? "",
                SpecialCode = bKRTransaction.SpecialCode,
                RegRegistrDate = "",
                JointContract = "", // No direct mapping
                NewName = "", // No direct mapping
                CodeRemovalReason = "", // No direct mapping
                BestandCode = "DE" // No direct mapping
            };
        }

        private static void InsertRegistration(SqlConnection connection, Registration registration)
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

            connection.Execute(sql, registration);
        }

        public static List<Registration> ConvertBKRDataToRegistrations(List<BKRData> bkrDataList)
        {
            var registrationList = new List<Registration>();

            foreach (var bkrData in bkrDataList)
            {
                var registration = new Registration
                {
                    TransactionCode = "01", // No direct mapping
                    Date = bkrData.Registratiedatum?.ToString("yyyyMMdd") ?? "",
                    ParticipantNo = bkrData.Deelnemernummer,
                    ParticipantNo2 = "",
                    Customer = "", // No direct mapping
                    Kredietnemernaam = bkrData.Kredietnemernaam,
                    Voorletters = bkrData.Voorletters,
                    Voornaam = "", // No direct mapping
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
                    GeboortedatumNieuw = "", // No direct mapping
                    Contractsoort = bkrData.Contractsoort,
                    Contract = bkrData.Contract,
                    ContractNieuw = "",
                    LimietContractBedrag = ((int)Math.Floor(bkrData.LimietContractBedrag)).ToString(CultureInfo.InvariantCulture),
                    Opnamebedrag = ((int)Math.Floor(bkrData.Opnamebedrag)).ToString(CultureInfo.InvariantCulture),
                    DatumEersteAflossing = bkrData.DatumEersteAflossing?.ToString("yyyyMMdd") ?? "",
                    DatumTLaatstAflossing = bkrData.DatumTLaatsteAflossing?.ToString("yyyyMMdd") ?? "",
                    DatumPLaatstAflossing = bkrData.DatumPLaatsteAflossing?.ToString("yyyyMMdd") ?? "",
                    SpecialCode = "", // No direct mapping
                    RegRegistrDate = bkrData.Registratiedatum?.ToString("yyyyMMdd") ?? "",
                    JointContract = "", // No direct mapping
                    NewName = "", // No direct mapping
                    CodeRemovalReason = "", // No direct mapping
                    BestandCode = "DE" // No direct mapping
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
    ParticipantNo2,
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
    @ParticipantNo2,
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
