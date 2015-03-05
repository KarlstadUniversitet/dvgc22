using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Policy;
using Repository;
using Domain;
using System.Collections.Generic;

namespace Scheduling.Test
{
    [TestClass]
    public class ParserTests
    {
        private CsvParser sp;
        private List<String[]> parsedCsv = new List<String[]>();
        private Filehandler fh;
        private String localURL;
        
        [TestInitialize]
        public void TestInitialize()
        {
            localURL = "TimeEdit_22756.csv";
            fh = new Filehandler();
        }

        [TestMethod]
        public void TestParse()
        {
            sp = new CsvParser();
            List<String[]> parsedCsv = new List<String[]>();
            parsedCsv = sp.Parse(fh.GetFileFromUrl(localURL));
            parsedCsv.RemoveRange(0, 4);
           
            String TestParse = " ";
            foreach (String[] Post in parsedCsv) {
                TestParse = Post[0];
                break;
            }
            Assert.AreEqual(TestParse, "2013-09-12");
        }
    }
}
