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

        public List<BKRTransaction> DetermineBKRTransactions(BKRData delta)
        {
            Contract contract = BKR.Classes.Contract.GetContract(Constants.SQL_CONNECTION_STRING, this.Contractnummer);

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
            if (contract.IndicatieBKRAfgelost == "Y" && this.IndicatieBKRAfgelost == "N")
            {
                this.IndicatieBKRAfgelost = "Y";
                bKRTransactions.Add(new BKRTransaction("02", ""));
            }
            return bKRTransactions;
        }

        public bool IsEqual(BKRData delta)
        {
            return this.Kredietnemernaam == delta.Kredietnemernaam &&
                   this.Voorletters == delta.Voorletters &&
                   this.Prefix == delta.Prefix &&
                   this.Straat == delta.Straat &&
                   this.Huisnummer == delta.Huisnummer &&
                   this.Alfanumeriek1 == delta.Alfanumeriek1 &&
                   this.Postcode == delta.Postcode &&
                   this.Alfanumeriek2 == delta.Alfanumeriek2 &&
                   this.Woonplaats == delta.Woonplaats &&
                   this.Contractnummer == delta.Contractnummer &&
                   this.Contractsoort == delta.Contractsoort &&
                   this.Deelnemernummer == delta.Deelnemernummer &&
                   this.Registratiedatum == delta.Registratiedatum &&
                   this.DatumLaatsteMutatie == delta.DatumLaatsteMutatie &&
                   this.LimietContractBedrag == delta.LimietContractBedrag &&
                   this.Opnamebedrag == delta.Opnamebedrag &&
                   this.DatumEersteAflossing == delta.DatumEersteAflossing &&
                   this.DatumTLaatsteAflossing == delta.DatumTLaatsteAflossing &&
                   this.DatumPLaatsteAflossing == delta.DatumPLaatsteAflossing &&
                   this.IndicatieBKRAfgelost == delta.IndicatieBKRAfgelost &&
                   this.AchterstCode1 == delta.AchterstCode1 &&
                   this.DatumAchterstCode1 == delta.DatumAchterstCode1 &&
                   this.AchterstCode2 == delta.AchterstCode2 &&
                   this.DatumAchterstCode2 == delta.DatumAchterstCode2 &&
                   this.AchterstCode3 == delta.AchterstCode3 &&
                   this.DatumAchterstCode3 == delta.DatumAchterstCode3 &&
                   this.AchterstCode4 == delta.AchterstCode4 &&
                   this.DatumAchterstCode4 == delta.DatumAchterstCode4 &&
                   this.AchterstCode5 == delta.AchterstCode5 &&
                   this.DatumAchterstCode5 == delta.DatumAchterstCode5 &&
                   this.AchterstCode6 == delta.AchterstCode6 &&
                   this.DatumAchterstCode6 == delta.DatumAchterstCode6 &&
                   this.AchterstCode7 == delta.AchterstCode7 &&
                   this.DatumAchterstCode7 == delta.DatumAchterstCode7 &&
                   this.Geslacht == delta.Geslacht &&
                   this.LandCode == delta.LandCode;
        }
    }
}
