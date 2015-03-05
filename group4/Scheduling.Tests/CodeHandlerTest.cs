using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using System.Linq;
using Repository;

namespace Scheduling.Test
{
    [TestClass]
    public class CodeHandlerTest
    {
        CodeHandler codeHandler;
    
        [TestInitialize]
        public void TestInitialize()
        {
            codeHandler = new CodeHandler();
        }


        /*[TestMethod]
        public void Find_ApplicationCode()
        {
            List<Application> result = codeHandler.GetApplicationCodeList("22756");

            //Assert.AreEqual(22756, result.ElementAt(0).Code);
        }*/
        [TestMethod]
        public void Find_Nothing()
        {
            List<Application> result = codeHandler.GetApplicationCodeList("");

            Assert.AreEqual(0, result.Count);
        }
        [TestMethod]
        public void Create_ApplicationCodeList()
        {
            List<Application> result = codeHandler.ConvertStringsToApplications("22222,33333");

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(22222, result.ElementAt(0).Code);
            Assert.AreEqual(33333, result.ElementAt(1).Code);

        }
    }
}

