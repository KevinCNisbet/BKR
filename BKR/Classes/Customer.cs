using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKR.Classes
{
    public class Customer
    {
        public string Customernummer { get; set; }
        public string Kredietnemernaam { get; set; }
        public string Voorletters { get; set; }
        public string Prefix { get; set; }
        public DateTime Geboortedatum { get; set; }
        public string Straat { get; set; }
        public string Alfanumeriek1 { get; set; }
        public string Huisnummer { get; set; }
        public string Postcode { get; set; }
        public string Alfanumeriek2 { get; set; }
        public string Woonplaats { get; set; }
        public string Geslacht { get; set; }
        public string LandCode { get; set; }
    }
}
