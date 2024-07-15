using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BKR.Classes;
using BKR.Processing;

namespace BKRTest
{
    public class TestData
    {
        public Customer customer1 = new Customer("CUST001", "Joosten", "V", "", new DateTime(1980, 1, 1), "Zandweg", "", "31", "3233", "ET", "OOSTVORNE", "M", "NLD");
        public Customer customer1_amended = new Customer("CUST001", "Joosten", "V", "", new DateTime(1980, 1, 1), "Schapenlaan", "", "111", "2512", "HT", "DEN HAAG", "M", "NLD");
        public Customer customer2 = new Customer("CUST002", "Mulder", "J", "van", new DateTime(1990, 2, 2), "De Matestraat", "", "293", "7447", "BA", "OVERIJSSEL", "V", "NLD");
        public Customer customer3 = new Customer("CUST003", "Wel", "A", "de", new DateTime(2000, 3, 3), "Sint Michaelstraat", "A", "26", "5861", "BV", "LIMBURG", "V", "NLD");
        public Customer customer4 = new Customer("CUST004", "Klaasen", "H", "", new DateTime(1990, 4, 4), "Koedikk", "", "40", "3224", "LD", "HELLEVOETSLUIS", "V", "NLD");

        public Contract contract1 = new Contract("CUST001", "CUST002", "CNTR001", "AK", "9000100", 50000m, 20000m, new DateTime(2023, 2, 1), new DateTime(2024, 1, 1), new DateTime(2024, 6, 1), "N", 0,null);
        public Contract contract2 = new Contract("CUST003", "", "CNTR002", "RK", "9000100", 75000m, 30000m, new DateTime(2023, 3, 15), new DateTime(2024, 2, 15), new DateTime(2024, 7, 15), "N", 0, null);
        public Contract contract3 = new Contract("CUST001", "", "CNTR003", "AK", "9000100", 100000m, 50000m, new DateTime(2023, 4, 20), new DateTime(2024, 3, 20), new DateTime(2024, 8, 20), "N", 0, null);
        public Contract contract4 = new Contract("CUST003", "CUST004", "CNTR004", "RK", "9000100", 5000m, 5000m, new DateTime(2023, 4, 1), new DateTime(2024, 1, 1), new DateTime(2024, 6, 1), "N", 0, null);

        public Contract contract1_amended = new Contract("CUST001", "CUST002", "CNTR001", "AK", "9000100", 50000m, 20000m, new DateTime(2023, 2, 1), new DateTime(2024, 1, 1), new DateTime(2024, 6, 1), "Y", 0, null);
        public Contract contract2_amended = new Contract("CUST003", "", "CNTR002", "RK", "9000100", 75000m, 30000m, new DateTime(2023, 3, 28), new DateTime(2024, 3, 15), new DateTime(2024, 7, 15), "N", 1, null);
        public Contract contract3_amended = new Contract("CUST001", "", "CNTR003", "AK", "9000100", 100000m, 50000m, new DateTime(2023, 4, 20), new DateTime(2024, 3, 20), new DateTime(2024, 8, 20), "N", 0, null);

        public BKRData bkrData1 = new BKRData("CNTR001", "Joosten", "V", "", new DateTime(1980, 1, 1), "Zandweg", "31", "", "3233", "ET", "OOSTVORNE", "CNTR001", "AK", "9000100", null, null, 50000m, 20000m, 
            new DateTime(2023, 2, 1), new DateTime(2024, 1, 1), new DateTime(2024, 6, 1), "N", null, "M", "NLD");

        public BKRData bkrData1_2 = new BKRData("CNTR001", "Mulder", "J", "van", new DateTime(1990, 2, 2), "De Matestraat", "293", "", "7447", "BA", "OVERIJSSEL", "CNTR001", "AK", "9000100", null, null, 50000m, 20000m, 
            new DateTime(2023, 2, 1), new DateTime(2024, 1, 1), new DateTime(2024, 6, 1), "N", null, "V", "NLD");

        public BKRData bkrData2 = new BKRData("CNTR002", "Wel", "A", "de", new DateTime(2000, 3, 3), "Sint Michaelstraat", "26", "A", "5861", "BV", "LIMBURG", "CNTR002", "RK", "9000100", null, null, 75000m, 30000m, 
            new DateTime(2023, 3, 15), new DateTime(2024, 2, 15), new DateTime(2024, 7, 15), "N", null, "V", "NLD");

        public BKRData bkrData3 = new BKRData("CNTR003", "Joosten", "V", "", new DateTime(1980, 1, 1), "Zandweg", "31", "", "3233", "ET", "OOSTVORNE", "CNTR003", "AK", "9000100", null, null, 100000m, 50000m, 
            new DateTime(2023, 4, 20), new DateTime(2024, 3, 20), new DateTime(2024, 8, 20), "N", null, "M", "NLD");
        
        public BKRData bkrData4 = new BKRData("CNTR004", "Wel", "A", "de", new DateTime(2000, 3, 3), "Sint Michaelstraat", "26", "A", "5861", "BV", "LIMBURG", "CNTR004", "RK", "9000100", null, null, 5000m, 5000m,
            new DateTime(2023, 4, 1), new DateTime(2024, 1, 1), new DateTime(2024, 6, 1), "N", null, "V", "NLD");

        public BKRData bkrData4_2 = new BKRData("CNTR004", "Klaasen", "H", "", new DateTime(1990, 4, 4), "Koedikk", "40", "", "3224", "LD", "HELLEVOETSLUIS", "CNTR004", "RK", "9000100", null, null, 5000m, 5000m,
            new DateTime(2023, 4, 1), new DateTime(2024, 1, 1), new DateTime(2024, 6, 1), "N", null, "V", "NLD");


        public Registration registration1 = new Registration("01", "", "9000100", "", "", "Joosten", "V", "", "", "19800101", "M", "Zandweg", "31", "", "OOSTVORNE", "3233", "ET", "NLD", 
            "", "AK", "CNTR001", "", "50000", "20000", "20230201", "20240101", "20240601", "", "", "", "", "", "DE");
        public Registration registration2 = new Registration("01", "", "9000100", "", "", "Mulder",  "J", "", "van", "19900202", "V", "De Matestraat", "293", "", "OVERIJSSEL", "7447", "BA", "NLD", 
            "", "AK", "CNTR001", "", "50000", "20000", "20230201", "20240101", "20240601", "", "", "", "", "", "DE");
        public Registration registration3 = new Registration("01", "", "9000100", "", "",  "Wel", "A", "", "de", "20000303", "V", "Sint Michaelstraat", "26", "A", "LIMBURG", "5861", "BV", "NLD", 
            "", "RK", "CNTR002", "", "75000", "30000", "20230315", "20240215", "20240715", "", "", "", "", "", "DE");
        public Registration registration4 = new Registration("01", "", "9000100", "", "",  "Joosten", "V", "", "", "19800101", "M", "Zandweg", "31", "", "OOSTVORNE", "3233", "ET", "NLD", 
            "", "AK", "CNTR003", "", "100000", "50000", "20230420", "20240320", "20240820", "", "", "", "", "","DE");
        public Registration registration5 = new Registration("05", "", "9000100", "", "", "Joosten", "V", "", "", "19800101", "M", "Schapenlaan", "111", "", "DEN HAAG", "2512", "HT", "NLD",
            "", "AK", "CNTR001", "", "50000", "20000", "20230201", "20240101", "20240601", "", "", "", "", "", "DE");

        public Registration registration6 = new Registration("02", "", "9000100", "", "", "Joosten", "V", "", "", "19800101", "M", "Schapenlaan", "111", "", "DEN HAAG", "2512", "HT", "NLD",
            "", "AK", "CNTR001", "", "50000", "20000", "20230201", "20240101", "20240601", "", "", "", "", "", "DE");

        public Registration registration7 = new Registration("02", "", "9000100", "", "", "Mulder", "J", "", "van", "19900202", "V", "De Matestraat", "293", "", "OVERIJSSEL", "7447", "BA", "NLD",
            "", "AK", "CNTR001", "", "50000", "20000", "20230201", "20240101", "20240601", "", "", "", "", "", "DE");

        public Registration registration8 = new Registration("09", "", "9000100", "", "", "Wel", "A", "", "de", "20000303", "V", "Sint Michaelstraat", "26", "A", "LIMBURG", "5861", "BV", "NLD",
            "", "RK", "CNTR002", "", "75000", "30000", "20230328", "20240315", "20240715", "", "", "", "", "", "DE");

        public Registration registration9 = new Registration("12", "", "9000100", "", "", "Wel", "A", "", "de", "20000303", "V", "Sint Michaelstraat", "26", "A", "LIMBURG", "5861", "BV", "NLD",
            "", "RK", "CNTR002", "", "75000", "30000", "20230328", "20240315", "20240715", "", "", "", "", "", "DE");

        public Registration registration10 = new Registration("05", "", "9000100", "", "", "Joosten", "V", "", "", "19800101", "M", "Schapenlaan", "111", "", "DEN HAAG", "2512", "HT", "NLD",
            "", "AK", "CNTR003", "", "100000", "50000", "20230420", "20240320", "20240820", "", "", "", "", "", "DE");

        public Registration registration11 = new Registration("03", "", "9000100", "", "", "Joosten", "V", "", "", "19800101", "M", "Schapenlaan", "111", "", "DEN HAAG", "2512", "HT", "NLD",
            "", "AK", "CNTR003", "", "100000", "50000", "20230420", "20240320", "20240820", "A", "", "", "", "", "DE");

        public Registration registration12 = new Registration("01", "", "9000100", "", "", "Wel", "A", "", "de", "20000303", "V", "Sint Michaelstraat", "26", "A", "LIMBURG", "5861", "BV", "NLD",
            "", "RK", "CNTR004", "", "5000", "5000", "20230401", "20240101", "20240601", "", "", "", "", "", "DE");

        public Registration registration13 = new Registration("01", "", "9000100", "", "", "Klaasen", "H", "", "", "19900404", "V", "Koedikk", "40", "", "HELLEVOETSLUIS", "3224", "LD", "NLD",
            "", "RK", "CNTR004", "", "5000", "5000", "20230401", "20240101", "20240601", "", "", "", "", "", "DE");

        public Registration registration14 = new Registration("03", "", "9000100", "", "", "Joosten", "V", "", "", "19800101", "M", "Schapenlaan", "111", "", "DEN HAAG", "2512", "HT", "NLD",
    "", "AK", "CNTR003", "", "100000", "50000", "20230420", "20240320", "20240820", "H", "", "", "", "", "DE");

        public Registration registration15 = new Registration("03", "", "9000100", "", "", "Wel", "A", "", "de", "20000303", "V", "Sint Michaelstraat", "26", "A", "LIMBURG", "5861", "BV", "NLD",
            "", "RK", "CNTR004", "", "5000", "5000", "20230401", "20240101", "20240601", "A", "", "", "", "", "DE");

        public Registration registration16 = new Registration("03", "", "9000100", "", "", "Klaasen", "H", "", "", "19900404", "V", "Koedikk", "40", "", "HELLEVOETSLUIS", "3224", "LD", "NLD",
            "", "RK", "CNTR004", "", "5000", "5000", "20230401", "20240101", "20240601", "A", "", "", "", "", "DE");



    }
}
