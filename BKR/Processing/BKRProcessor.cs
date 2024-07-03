using BKR.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKR.Processing
{
    public class BKRProcessor
    {
        public List<BKRClass> CombineData(List<Customer> customers, List<Contract> contracts)
        {
            var bkrList = new List<BKRClass>();

            foreach (var contract in contracts)
            {
                var customer1 = customers.FirstOrDefault(c => c.Customernummer == contract.Customer1);
                var customer2 = customers.FirstOrDefault(c => c.Customernummer == contract.Customer2);

                if (customer1 != null)
                {
                    var bkr1 = CreateBKRClass(customer1, contract);
                    bkrList.Add(bkr1);
                }

                if (customer2 != null && !string.IsNullOrEmpty(contract.Customer2))
                {
                    var bkr2 = CreateBKRClass(customer2, contract);
                    bkrList.Add(bkr2);
                }
            }

            return bkrList;
        }

        private BKRClass CreateBKRClass(Customer customer, Contract contract)
        {
            return new BKRClass
            {
                Contract = contract.Contractnummer,
                Kredietnemernaam = customer.Kredietnemernaam,
                Voorletters = customer.Voorletters,
                Prefix = customer.Prefix,
                Geboortedatum = customer.Geboortedatum,
                Straat = customer.Straat,
                Huisnummer = customer.Huisnummer,
                Alfanumeriek1 = customer.Alfanumeriek1,
                Postcode = customer.Postcode,
                Alfanumeriek2 = customer.Alfanumeriek2,
                Woonplaats = customer.Woonplaats,
                Contractnummer = contract.Contractnummer,
                Contractsoort = contract.Contractsoort,
                Deelnemernummer = contract.Deelnemernummer,
                Registratiedatum = contract.Registratiedatum,
                DatumLaatsteMutatie = DateTime.Now, // Assuming current date for mutation date
                LimietContractBedrag = contract.LimietContractBedrag,
                Opnamebedrag = contract.Opnamebedrag,
                DatumEersteAflossing = contract.DatumEersteAflossing,
                DatumTLaatsteAflossing = contract.DatumTLaatsteAflossing,
                DatumPLaatsteAflossing = contract.DatumPLaatsteAflossing,
                IndicatieBKRAfgelost = contract.IndicatieBKRAfgelost,
                AchterstCode1 = null, // Assuming null for Achterst codes
                DatumAchterstCode1 = null,
                AchterstCode2 = null,
                DatumAchterstCode2 = null,
                AchterstCode3 = null,
                DatumAchterstCode3 = null,
                AchterstCode4 = null,
                DatumAchterstCode4 = null,
                AchterstCode5 = null,
                DatumAchterstCode5 = null,
                AchterstCode6 = null,
                DatumAchterstCode6 = null,
                AchterstCode7 = null,
                DatumAchterstCode7 = null,
                Geslacht = customer.Geslacht,
                LandCode = customer.LandCode
            };
        }
    }
}