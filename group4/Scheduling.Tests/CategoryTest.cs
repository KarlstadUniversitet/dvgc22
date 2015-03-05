using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace Scheduling.Tests
{
    [TestClass]
    public class CategoryTest
    {
        Category category;
        [TestInitialize]
        public void TestInitialize()
        {
            category = new Category();
        }
        [TestMethod]
        public void TestInitializing()
        {
            Assert.IsNotNull(category);
        }
        [TestMethod]
        public void TestName()
        {
            category.Name = "name";
            Assert.AreEqual("name", category.Name);
        }
        [TestMethod]
        public void TestList()
        {
            Assert.IsNotNull(category.Applications);
            Assert.AreEqual(0, category.Applications.Count);
        }
        [TestMethod]
        public void TestListAddApplication()
        {
            Application app = new Application();
            category.Applications.Add(app);
            Assert.AreEqual(1, category.Applications.Count);
        }
        [TestMethod]
        public void TestApplicationCodesCommaSeparated()
        {
            Assert.AreEqual("",category.ApplicationCodesCommaSeparated());
        }
        [TestMethod]
        public void TestApplicationCodesCommaSeparatedSingle()
        {
            Application app = new Application(1234);
            category.Applications.Add(app);
            Assert.AreEqual("1234", category.ApplicationCodesCommaSeparated()); 
        }
        [TestMethod]
        public void TestApplicationCodesCommaSeparatedMultiple()
        {
            Application app1 = new Application(1234);
            category.Applications.Add(app1);
            Application app2 = new Application(5678);
            category.Applications.Add(app2);
            Assert.AreEqual("1234,5678", category.ApplicationCodesCommaSeparated());
        }

        [TestMethod]
        public void TestRemoveByApplicationCode()
        {
            Application app = new Application(1234);
            category.Applications.Add(app);
            Assert.AreEqual(1, category.Applications.Count);
            category.RemoveCourseByApplicationCode(1234);
            Assert.AreEqual(0, category.Applications.Count); 
        }

        [TestMethod]
        public void TestRemoveByApplicationCodeWithMultiples()
        {
            Application app1 = new Application(1234);
            Application app2 = new Application(1234);
            Application app3 = new Application(123455);
            category.Applications.Add(app1);
            category.Applications.Add(app2);
            category.Applications.Add(app3);
            Assert.AreEqual(3, category.Applications.Count);
            category.RemoveCourseByApplicationCode(1234);
            Assert.AreEqual(2, category.Applications.Count);   
        }

        [TestMethod]
        public void TestAddSorted()
        {
            Application app1 = new Application(1232, "DVGC22");
            Application app2 = new Application(1234, "DVGC19");
            Application app3 = new Application(123455, "DVGA03");
            category.AddSorted(app1);
            category.AddSorted(app2);
            category.AddSorted(app3);
            Assert.AreEqual(3, category.Applications.Count);
            Assert.AreEqual("DVGA03", category.Applications[0].CourseCode);
        }

        [TestMethod]
        public void TestCategoryDescription()
        {
            Category c = new Category("test", "this is a description");

            Assert.AreEqual(c.Description, "this is a description");
        }

        [TestMethod]
        public void TestEmptyDescription()
        {
            Category c = new Category("test");

            Assert.AreEqual(c.Description, "");
        }
    }
}
