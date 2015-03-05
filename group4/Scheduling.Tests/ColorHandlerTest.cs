using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using Repository;
using System.Collections.Generic;

namespace Scheduling.Test
{
    [TestClass]
    public class ColorHandlerTest
    {
        [TestMethod]
        public void ColorHandler()
        {
            ColorHandler colorHandeler = new ColorHandler();
            Assert.IsNotNull(colorHandeler);
        }
        [TestMethod]
        public void AddAndGetColorTest()
        {
            ColorHandler colorHandler = new ColorHandler();
            Lecture lecture = new Lecture();
            lecture.application = new Application(324234);
            colorHandler.AddColor(lecture);
            Assert.IsNotNull(colorHandler.GetSavedColor(lecture));
        }
        [TestMethod]
        public void AddTwoColorsTest()
        {
            ColorHandler colorHandler = new ColorHandler();
            Lecture lecture1 = new Lecture() { course = "DVGC22", application = new Application(22756) };
            Lecture lecture2 = new Lecture() { course = "DVGC19", application = new Application(22754) };

            colorHandler.AddColor(lecture1);
            colorHandler.AddColor(lecture2);

            Assert.AreNotEqual(colorHandler.GetSavedColor(lecture1), colorHandler.GetSavedColor(lecture2));
        }
        [TestMethod]
        public void ElevenColorTest()
        {
            ColorHandler colorHandler = new ColorHandler();
            for (int i = 1; i < 60; i++) 
                colorHandler.AddColor(new Lecture() { course = i.ToString(), application = new Application(i) });
            Lecture lecture11 = new Lecture() { course = 11.ToString(), application = new Application(11) };
            colorHandler.AddColor(lecture11);

            Assert.AreEqual("color1", colorHandler.GetSavedColor(lecture11));
        }

    }
}
