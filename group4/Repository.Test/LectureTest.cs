using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Repository;
using Domain;

namespace Repository.Test
{
    [TestClass]
    public class LectureTest
    {
        private CsvParser sp;
        private List<String[]> parsedCsv = new List<String[]>();
        private Filehandler fh;
        private String url;
        private Lecture testLecture;
        private String[] postInfo;

        [TestInitialize]
        public void TestInitialize()
        {
            url = "~/../../../TimeEdit_22756.csv";
            sp = new CsvParser();
            fh = new Filehandler();
            parsedCsv = sp.Parse(fh.GetFileFromUrl(url));
            parsedCsv.RemoveRange(0, 4);
            testLecture = new Lecture();
            testLecture.teacher = "Martin Blom";
            testLecture.classroom = "21E404 DV";
            testLecture.startTime = new DateTime(2013, 9, 12, 8, 15, 0);
            testLecture.endTime = new DateTime(2013, 9, 12, 16, 0, 0);
            foreach (String[] post in parsedCsv)
            {
                postInfo = post;
                break;
            }

        }
        [TestMethod]
        public void testBuildLecture()
        {
            Lecture lecture = new Lecture();
            lecture = Lecture.buildLecture(postInfo, new Application(22756));
            Assert.AreEqual(testLecture.classroom, lecture.classroom);
            Assert.AreEqual(testLecture.endTime, lecture.endTime);
            Assert.AreEqual(testLecture.startTime, lecture.startTime);
            Assert.AreEqual(testLecture.teacher, lecture.teacher);
        }
    }
}
