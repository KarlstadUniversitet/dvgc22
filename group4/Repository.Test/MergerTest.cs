using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Repository;
using Domain;
using System.IO;

namespace Repository.Test
{
    [TestClass]
    public class MergerTest
    {
        private String localURL;
        private String localURL2;
        private Schedule scheduleOne, scheduleTwo;
        private Merger merger;
        private List<Schedule> schedules;

        [TestInitialize]
        public void TestInitialize()
        {
            localURL = "~/../../../TimeEdit_22756.csv";
            localURL2 = "~/../../../TimeEdit_22754.csv";
            scheduleOne = new Schedule();
            scheduleTwo = new Schedule();
            CsvParser csvParser = new CsvParser();
            Filehandler fileHandler = new Filehandler();
            scheduleOne.Build(csvParser.Parse(fileHandler.GetFileFromUrl(localURL)), new Application(22756));
            scheduleTwo.Build(csvParser.Parse(fileHandler.GetFileFromUrl(localURL2)), new Application(22756));
            merger = new Merger();
            schedules = new List<Schedule>();
            schedules.Add(scheduleOne);
            schedules.Add(scheduleTwo);

        }

        [TestMethod]
        public void MergerExistsTest()
        {
            Merger merger = new Merger();
            Assert.IsNotNull(merger);
        }

        [TestMethod]
        public void MergeSchedule()
        {
            Schedule result = merger.Merge(schedules);
            Assert.AreEqual(40, result.Lectures.Count);
        }

        [TestMethod]
        public void SortingTest()
        {
            Schedule schedule = merger.Merge(schedules);
            schedule.Sort();
            bool success = false;
            DateTime previous = new DateTime(2013, 09, 12, 8, 15, 0);
            foreach (Lecture lecture in schedule.Lectures)
            {
                if (lecture.startTime < previous)
                {
                    success = false;
                    break;
                }
                else
                    success = true;
                previous = lecture.startTime;
            }
            Assert.IsTrue(success);
        }
    }
}
