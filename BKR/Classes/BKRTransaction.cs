using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKR.Classes
{
    public class BKRTransaction
    {
        public string TransactionCode { get; set; }
        public string SpecialCode { get; set; }

        // Constructor
        public BKRTransaction(string transactionCode, string specialCode)
        {
            TransactionCode = transactionCode;
            SpecialCode = specialCode;
        }
    }
}
