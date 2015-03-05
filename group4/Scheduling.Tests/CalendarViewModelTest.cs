using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using System.Linq;
using Repository;
using System.Collections.Generic;

namespace Scheduling.Test
{
    [TestClass]
    public class CalendarViewModelTest
    {       
        Schedule schedule;
        CsvParser csvParser;
        CalendarViewModel cvm;
        Filehandler fileHandler;
        string localURL = "TimeEdit_22756.csv";

        [TestInitialize]
        public void TestInitialize()
        {
            cvm = new CalendarViewModel();            
            schedule = new Schedule();
            csvParser = new CsvParser();
            fileHandler = new Filehandler();
        }

        [TestMethod]
        public void DaysTest()
        {
            // Assign
            schedule.Build(csvParser.Parse(fileHandler.GetFileFromUrl(localURL)), new Application(22756));
            
            // Act
            cvm.lectures = schedule.Lectures;
            
            // Assert
            Assert.AreEqual(49, cvm.Days());
        }
        [TestMethod]
        public void GetFirstLectureDateTest()
        {
            // Assign
            schedule.Build(csvParser.Parse(fileHandler.GetFileFromUrl(localURL)), new Application(22756));

            // Act
            cvm.lectures = schedule.Lectures;

            // Assert
            Assert.AreEqual(schedule.Lectures.First<Lecture>().startTime, cvm.GetFirstLectureDate());
        }
        [TestMethod]
        public void GetColorTest()
        {
            // Assign
            cvm.lectures = new List<Lecture>();
            Application app = new Application(22756);
            Lecture lecture = new Lecture()
            {
                application = app,
                classroom = "Classroom",
                info = "Info",
                startTime = DateTime.Now,
                endTime = DateTime.Now,
                course = "DVGC22",
                teacher = "Martin Blom"
            };

            // Act
            string result = cvm.GetColor(lecture);

            // Assert
            Assert.AreEqual("color1", result);
        }

        [TestMethod]
        public void GetUniqueListTest()
        {
            
        }

        [TestMethod]
        public void TestHoleTwoLecturesNoBreaks()
        {
            Application app = new Application(22756);
            DateTime start = DateTime.Parse("2013-10-16 08:15");
            Lecture lecture1 = new Lecture() { application = app, classroom = "Classroom", info = "Info", startTime = start, endTime = start.AddHours(5), course = "DVGC22", teacher = "Martin Blom" };
            Lecture lecture2 = new Lecture() { application = app, classroom = "Classroom", info = "Info", startTime = start.AddMinutes(329), endTime = start.AddMinutes(540), course = "DVGC22", teacher = "Martin Blom" };
            List<Lecture> lectures = new List<Lecture>();
            lectures.Add(lecture1);
            lectures.Add(lecture2);
            cvm.lectures = lectures;
            cvm.AddBreaksBetweenLectures();

            Assert.AreEqual(2, cvm.lectures.Count);  
        }

        [TestMethod]
        public void TestHoleTwoLecturesOneBreak()
        {
            Application app = new Application(22756);
            DateTime start = DateTime.Parse("2013-10-16 08:15");
            Lecture lecture1 = new Lecture() { application = app, classroom = "Classroom", info = "Info", startTime = start, endTime = start.AddHours(5), course = "DVGC22", teacher = "Martin Blom" };
            Lecture lecture2 = new Lecture() { application = app, classroom = "Classroom", info = "Info", startTime = start.AddMinutes(330), endTime = start.AddMinutes(540), course = "DVGC22", teacher = "Martin Blom" };
            List<Lecture> lectures = new List<Lecture>();
            lectures.Add(lecture1);
            lectures.Add(lecture2);
            cvm.lectures = lectures;
            cvm.AddBreaksBetweenLectures();


            Assert.AreEqual(3, cvm.lectures.Count);

        }

        [TestMethod]
        public void TestHoleTwoLecturesBreakOutOfBounds()
        {
            Application app = new Application(22756);
            DateTime start = DateTime.Parse("2013-10-16 14:15");
            Lecture lecture1 = new Lecture() { application = app, classroom = "Classroom", info = "Info", startTime = start, endTime = start.AddHours(4), course = "DVGC22", teacher = "Martin Blom" };
            Lecture lecture2 = new Lecture() { application = app, classroom = "Classroom", info = "Info", startTime = start.AddHours(18), endTime = start.AddHours(20), course = "DVGC22", teacher = "Martin Blom" };
            List<Lecture> lectures = new List<Lecture>();
            lectures.Add(lecture1);
            lectures.Add(lecture2);
            cvm.lectures = lectures;
            cvm.AddBreaksBetweenLectures();

            Assert.AreEqual(1, lectures[0].application.Code);
            Assert.AreEqual(4, cvm.lectures.Count);
        }

        [TestMethod]
        public void TestHoleAfterLastLecture()
        {
            Application app = new Application(22756);
            DateTime start = DateTime.Parse("2013-10-16 14:15");
            Lecture lecture1 = new Lecture() { application = app, classroom = "Classroom", info = "Info", startTime = start, endTime = start.AddHours(4), course = "DVGC22", teacher = "Martin Blom" };
            Lecture lecture2 = new Lecture() { application = app, classroom = "Classroom", info = "Info", startTime = start.AddHours(18), endTime = start.AddHours(20), course = "DVGC22", teacher = "Martin Blom" };
            List<Lecture> lectures = new List<Lecture>();
            lectures.Add(lecture1);
            lectures.Add(lecture2);
            cvm.lectures = lectures;
            cvm.AddBreaksBetweenLectures();

            Assert.AreEqual(1, lectures[3].application.Code);
            Assert.AreEqual(4, cvm.lectures.Count);
            
        }

        [TestMethod]
        public void TestMergeOneExamWithLectures()
        {
            //Assign
            List<Lecture> exams = new List<Lecture>();

            DateTime examstart = new DateTime(2014, 8, 30, 8, 15, 00);
            Lecture test1 = new Lecture() { application = new Application(24400), classroom = "Transformum", info = "hej hej", startTime = examstart, endTime = examstart.AddHours(5), course = "DVGC22", teacher = "" };
            exams.Add(test1);

            Application app = new Application(22756);
            DateTime start = DateTime.Parse("2015-10-16 14:15");
            Lecture lecture1 = new Lecture() { application = app, classroom = "Classroom", info = "Info", startTime = start, endTime = start.AddHours(5), course = "DVGC22", teacher = "Martin Blom" };

            List<Lecture> lectures = new List<Lecture>();
            lectures.Add(lecture1);

            cvm.lectures = lectures;

            //Act

            cvm.AddExams(exams);

            //Assert

            Assert.AreEqual(0, cvm.lectures.IndexOf(test1));

        }

        [TestMethod]
        public void TestCheckIfExamsBelongToLectures()
        {
            //Assign
            List<Lecture> exams = new List<Lecture>();

            DateTime examstart = new DateTime(2014, 8, 30, 8, 15, 00);
            Lecture test1 = new Lecture() { application = new Application(24400), classroom = "Transformum", info = "hej hej", startTime = examstart, endTime = examstart.AddHours(5), course = "DVGC19", teacher = "" };
            exams.Add(test1);

            Application app = new Application(22756);
            DateTime start = DateTime.Parse("2015-10-16 14:15");
            Lecture lecture1 = new Lecture() { application = app, classroom = "Classroom", info = "Info", startTime = start, endTime = start.AddHours(5), course = "DVGC22", teacher = "Martin Blom" };
           
            List<Lecture> lectures = new List<Lecture>();
            lectures.Add(lecture1);

            cvm.lectures = lectures;

            //Act
            cvm.AddExams(exams);

            //Assert
            Assert.AreEqual(1, cvm.lectures.Count());
        }

        [TestMethod]
        public void TestExamProperty()
        {
            // Assign
            List<Lecture> exams = new List<Lecture>();

            DateTime examstart = new DateTime(2014, 8, 30, 8, 15, 00);
            Lecture test1 = new Lecture() { application = new Application(24400), classroom = "Transformum", info = "hej hej", startTime = examstart, endTime = examstart.AddHours(5), course = "DVGC22", teacher = "" };
            exams.Add(test1);

            Application app = new Application(22756);
            DateTime start = DateTime.Parse("2015-10-16 14:15");
            Lecture lecture1 = new Lecture() { application = app, classroom = "Classroom", info = "Info", startTime = start, endTime = start.AddHours(5), course = "DVGC22", teacher = "Martin Blom" };

            List<Lecture> lectures = new List<Lecture>();
            lectures.Add(lecture1);

            cvm.lectures = lectures;
            // Act
            cvm.AddExams(exams);

            // Assert

            Assert.AreEqual(true, cvm.lectures.Find(element => element == test1).isExam);
            Assert.AreEqual(false, cvm.lectures.Find(element => element == lecture1).isExam);
        }

        [TestMethod]
        public void TestNewAddExamMethod()
        {
             cvm.AddExams_New(XMLParser.FileParser("https://www3.kau.se/tentamenslista/rss.xml"), "");
        }

    }
}
