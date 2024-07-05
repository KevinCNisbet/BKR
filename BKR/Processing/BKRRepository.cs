using BKR.Classes;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;

namespace BKR.Processing
{
    public class BKRRepository
    {
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
                    IndicatieBKRAfgelost, AchterstCode1, DatumAchterstCode1, AchterstCode2, DatumAchterstCode2, 
                    AchterstCode3, DatumAchterstCode3, AchterstCode4, DatumAchterstCode4, AchterstCode5, 
                    DatumAchterstCode5, AchterstCode6, DatumAchterstCode6, AchterstCode7, DatumAchterstCode7, 
                    Geslacht, LandCode)
                VALUES (
                    @Contract, @Kredietnemernaam, @Voorletters, @Prefix, @Geboortedatum, @Straat, @Huisnummer, 
                    @Alfanumeriek1, @Postcode, @Alfanumeriek2, @Woonplaats, @Contractnummer, @Contractsoort, 
                    @Deelnemernummer, @Registratiedatum, @DatumLaatsteMutatie, @LimietContractBedrag, 
                    @Opnamebedrag, @DatumEersteAflossing, @DatumTLaatsteAflossing, @DatumPLaatsteAflossing, 
                    @IndicatieBKRAfgelost, @AchterstCode1, @DatumAchterstCode1, @AchterstCode2, @DatumAchterstCode2, 
                    @AchterstCode3, @DatumAchterstCode3, @AchterstCode4, @DatumAchterstCode4, @AchterstCode5, 
                    @DatumAchterstCode5, @AchterstCode6, @DatumAchterstCode6, @AchterstCode7, @DatumAchterstCode7, 
                    @Geslacht, @LandCode);";

                connection.Execute(sql, bkrList);
            }
        }
    }

}
