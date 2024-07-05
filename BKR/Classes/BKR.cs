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
        public string AchterstCode1 { get; set; }
        public DateTime? DatumAchterstCode1 { get; set; }
        public string AchterstCode2 { get; set; }
        public DateTime? DatumAchterstCode2 { get; set; }
        public string AchterstCode3 { get; set; }
        public DateTime? DatumAchterstCode3 { get; set; }
        public string AchterstCode4 { get; set; }
        public DateTime? DatumAchterstCode4 { get; set; }
        public string AchterstCode5 { get; set; }
        public DateTime? DatumAchterstCode5 { get; set; }
        public string AchterstCode6 { get; set; }
        public DateTime? DatumAchterstCode6 { get; set; }
        public string AchterstCode7 { get; set; }
        public DateTime? DatumAchterstCode7 { get; set; }
        public string Geslacht { get; set; }
        public string LandCode { get; set; }
    }
}
