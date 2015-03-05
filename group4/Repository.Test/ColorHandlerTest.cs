using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace Repository.Test
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
    }
}
