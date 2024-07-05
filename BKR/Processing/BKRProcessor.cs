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
        public static List<BKRData> CombineData(List<Customer> customers, List<Contract> contracts)
        {
            var bkrList = new List<BKRData>();

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

        private static BKRData CreateBKRClass(Customer customer, Contract contract)
        {
            return new BKRData
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
                LimietContractBedrag = contract.LimietContractBedrag,
                Opnamebedrag = contract.Opnamebedrag,
                DatumEersteAflossing = contract.DatumEersteAflossing,
                DatumTLaatsteAflossing = contract.DatumTLaatsteAflossing,
                DatumPLaatsteAflossing = contract.DatumPLaatsteAflossing,
                IndicatieBKRAfgelost = contract.IndicatieBKRAfgelost,
                Geslacht = customer.Geslacht,
                LandCode = customer.LandCode
            };
        }
    }
}