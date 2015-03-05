using Domain;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using System.Xml.Linq;
using System.Xml;
using System.Diagnostics;
using System.Collections.Generic;

namespace Scheduling.Tests
{
    [TestClass]
    public class XMLTest
    {

        String regexTest = "Benämning: Arkivkunskap I - Regelverket<br>Kurs: ARGA01&lt;br&gt;Sal: 11B 140&lt;br&gt;Tid: 0815-1315";
        Lecture tenta;
      //  String data;
     //   String link = "https://www3.kau.se/tentamenslista/rss.xml";
        List<Lecture> list;
        [TestInitialize]
        public void TestInitialize()
        {
            tenta = new Lecture();
            Filehandler fh = new Filehandler();
            list = XMLParser.FileParser("rss.xml");
            Assert.AreEqual(true,XMLParser.ParseDescription(tenta, regexTest, new DateTime(),new DateTime()));
        //    data=fh.ReadFile(fh.GetFileFromUrl(link));
            
        }
        [TestMethod]
        public void TestReadFile()
        {
            Assert.AreNotEqual(0,list.Count);
        }

        [TestMethod]
        public void TestRegexCourse()
        {
            Assert.AreEqual("ARGA01", tenta.course);
            Assert.AreEqual("ARGA01", tenta.application.CourseCode);
        }

        [TestMethod]
        public void TestRegexRoom()
        {
            Assert.AreEqual("11B 140",tenta.classroom);
        }

        [TestMethod]
        public void TestRegexInfo()
        {
            Assert.AreEqual("Tentamen: Regelverket", tenta.info);
        }

        [TestMethod]
        public void TestRegexTime()
        {
            Assert.AreEqual(8, tenta.startTime.Hour);
            Assert.AreEqual(15, tenta.startTime.Minute);
            Assert.AreEqual(13, tenta.endTime.Hour);
            Assert.AreEqual(15, tenta.endTime.Minute);
        }

        [TestMethod]
        public void TestRegexCourseName()
        {
            Assert.AreEqual("Arkivkunskap I", tenta.application.CourseName);
        }

        [TestMethod]
        public void TestDate()
        {
            Assert.AreEqual("2014-10-03 08:15:00", list[0].startTime.ToString());
            Assert.AreEqual("2014-10-03 12:15:00", list[0].endTime.ToString());
        }
    }
}