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
                    bkrList.Add(new BKRData(customer1, contract));
                }

                if (customer2 != null && !string.IsNullOrEmpty(contract.Customer2))
                {
                    bkrList.Add(new BKRData(customer2, contract));
                }
            }
            return bkrList;
        }
    }
}