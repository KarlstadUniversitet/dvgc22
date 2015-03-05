using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using System.Linq;


namespace Repository.Test
{
    [TestClass]
    public class CalendarViewModelTest
    {       
        Schedule schedule;
        CsvParser csvParser;
        CalendarViewModel cvm;
        Filehandler fileHandler;
        string localURL = "~/../../../TimeEdit_22756.csv";

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
    }
}
