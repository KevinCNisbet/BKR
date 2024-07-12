using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BKR.Classes;

namespace BKRTest
{
    public class TestData
    {
        public Customer customer1 = new Customer("CUST001", "Joosten", "V", "", new DateTime(1980, 1, 1), "Zandweg", "", "31", "3233", "ET", "OOSTVORNE", "M", "NLD");
        public Customer customer2 = new Customer("CUST002", "Mulder", "J", "van", new DateTime(1990, 2, 2), "De Matestraat", "", "293", "7447", "BA", "OVERIJSSEL", "V", "NLD");
        public Customer customer3 = new Customer("CUST003", "Wel", "A", "de", new DateTime(2000, 3, 3), "Sint Michaelstraat", "A", "26", "5861", "BV", "LIMBURG", "V", "NLD");

        public Contract contract1 = new Contract("CUST001", "CUST002", "CNTR001", "AK", "9000100", 50000m, 20000m, new DateTime(2023, 2, 1), new DateTime(2024, 1, 1), new DateTime(2024, 6, 1), "N", 0,null);
        public Contract contract2 = new Contract("CUST003", "", "CNTR002", "RK", "9000100", 75000m, 30000m, new DateTime(2023, 3, 15), new DateTime(2024, 2, 15), new DateTime(2024, 7, 15), "N", 0, null);
        public Contract contract3 = new Contract("CUST001", "", "CNTR003", "AK", "9000100", 100000m, 50000m, new DateTime(2023, 4, 20), new DateTime(2024, 3, 20), new DateTime(2024, 8, 20), "N", 0, null);
        public Contract contract4 = new Contract("CUST003", "CUST004", "CNTR004", "RK", "9000100", 5000m, 5000m, new DateTime(2023, 4, 1), new DateTime(2024, 1, 1), new DateTime(2024, 6, 1), "N", 0, null);

        public BKRData bkrData1 = new BKRData("CNTR001", "Joosten", "V", "", new DateTime(1980, 1, 1), "Zandweg", "31", "", "3233", "ET", "OOSTVORNE", "CNTR001", "AK", "9000100", null, null, 50000m, 20000m, 
            new DateTime(2023, 2, 1), new DateTime(2024, 1, 1), new DateTime(2024, 6, 1), "N", null, "M", "NLD");

        public BKRData bkrData1_2 = new BKRData("CNTR001", "Mulder", "J", "van", new DateTime(1990, 2, 2), "De Matestraat", "293", "", "7447", "BA", "OVERIJSSEL", "CNTR001", "AK", "9000100", null, null, 50000m, 20000m, 
            new DateTime(2023, 2, 1), new DateTime(2024, 1, 1), new DateTime(2024, 6, 1), "N", null, "V", "NLD");

        public BKRData bkrData2 = new BKRData("CNTR002", "Wel", "A", "de", new DateTime(2000, 3, 3), "Sint Michaelstraat", "26", "A", "5861", "BV", "LIMBURG", "CNTR002", "RK", "9000100", null, null, 75000m, 30000m, 
            new DateTime(2023, 3, 15), new DateTime(2024, 2, 15), new DateTime(2024, 7, 15), "N", null, "V", "NLD");

        public BKRData bkrData3 = new BKRData("CNTR003", "Joosten", "V", "", new DateTime(1980, 1, 1), "Zandweg", "31", "", "3233", "ET", "OOSTVORNE", "CNTR003", "AK", "9000100", null, null, 100000m, 50000m, 
            new DateTime(2023, 4, 20), new DateTime(2024, 3, 20), new DateTime(2024, 8, 20), "N", null, "M", "NLD");
    }
}
