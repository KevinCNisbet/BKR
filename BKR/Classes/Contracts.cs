﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKR.Classes
{
    public class Contract
    {
        public string Customer1 { get; set; }
        public string Customer2 { get; set; }
        public string Contractnummer { get; set; }
        public string Contractsoort { get; set; }
        public string Deelnemernummer { get; set; }
        public DateTime Registratiedatum { get; set; }
        public decimal LimietContractBedrag { get; set; }
        public decimal Opnamebedrag { get; set; }
        public DateTime DatumEersteAflossing { get; set; }
        public DateTime DatumTLaatsteAflossing { get; set; }
        public DateTime DatumPLaatsteAflossing { get; set; }
        public string IndicatieBKRAfgelost { get; set; }
        public decimal NumberOfPaymentsMissed { get; set; }
    }

}