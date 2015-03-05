using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Repository;
using Domain;

namespace Scheduling.Test
{
    [TestClass]
    public class FilehandlerTest
    {
        private Filehandler fh;
        private string url;

        [TestInitialize]
        public void TestInitialize() 
        {
            fh = new Filehandler();
            url = "TimeEdit_22756.csv";
        }
        [TestMethod]
        public void readFileTest()
        {
            Stream stream = fh.GetFileFromUrl(url);
            string s = fh.ReadFile(stream);
            Assert.IsNotNull(s);
        }
        [TestMethod]
        public void getFileFromUrlTest()
        {
            Stream stream = fh.GetFileFromUrl(url);
            Assert.IsNotNull(stream);
        }
        [TestMethod]
        public void readFile()
        {
            
        }
    }
}
