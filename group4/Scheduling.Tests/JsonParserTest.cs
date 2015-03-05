using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using Domain;
using Repository;

namespace Scheduling.Test
{
    [TestClass]
    public class JsonParserTest
    {
        string jsontext;
        [TestMethod]
        public void Parse_Correctly()
        {
            JsonParser json = new JsonParser();
            Filehandler fh = new Filehandler();
            jsontext = fh.ReadFile(fh.GetFileFromUrl("jsontest.json"));
            List<Application> result = json.ParseJson(jsontext);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(22756, result.ElementAt(1).Code);
        }
    }
}
