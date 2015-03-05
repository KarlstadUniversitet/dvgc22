using System;
using Repository;
using Domain;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Scheduling.Test
{

    [TestClass]
    public class ScheduleTest
    {
        private Schedule s;
        private CsvParser sp;
        private List<String[]> parsedCsv = new List<String[]>();
        private Filehandler fh;
        private String localURL = "TimeEdit_22756.csv";

        [TestInitialize]
        public void TestInitialize()
        {
            s = new Schedule();
            
            fh = new Filehandler();
            sp = new CsvParser();
            parsedCsv = sp.Parse(fh.GetFileFromUrl(localURL));
        }

        [TestMethod]
        public void scheduleExistanceTest()
        {
            Assert.IsNotNull(s);
        }

        [TestMethod]
        public void scheduleContainsLectureTest()
        {
            Lecture l = new Lecture();
            s.Lectures.Add(l);
            CollectionAssert.Contains(s.Lectures, l);
        }
        
        //[TestMethod]
        //public void buildScheduleTest()
        //{
        //    Schedule schedule = new Schedule();
        //    schedule = schedule.CreateFromUrl(localURL);
        //    String TestString = " ";
        //    foreach (Lecture lecture in schedule.Lectures)
        //    {
        //        TestString = lecture.startTime.ToString();
        //        break;
        //    }
        //    Assert.AreEqual(TestString, "2013-09-12 08:15:00");
        //}

        //[TestMethod]
        //public void CountLecturesTest()
        //{
        //    Merger merger = new Merger();

        //    List<String[]> parsedCsv = new List<String[]>();
        //    parsedCsv = sp.Parse(fh.GetFileFromUrl(localURL));

        //    Schedule schedule = new Schedule();
        //    schedule = schedule.CreateFromUrl(localURL);

        //    Assert.AreEqual(19, schedule.Lectures.Count);
        //}

        //[TestMethod]
        //public void CreateScheduleTest()
        //{
        //    String url = "~/../../../TimeEdit_22756.csv";
        //    Schedule schedule = new Schedule();
        //    schedule = schedule.CreateFromUrl(url);
        //    Lecture lecture = new Lecture();
        //    foreach (Lecture l in schedule.Lectures)
        //    {
        //        lecture = l;
        //        break;
        //    }
        //    Assert.AreEqual(lecture.startTime.ToString(), "2013-09-12 08:15:00");
        //}
       
    }
}
