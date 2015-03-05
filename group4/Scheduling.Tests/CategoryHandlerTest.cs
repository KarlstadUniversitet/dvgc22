using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Domain;

namespace Scheduling.Tests
{
    [TestClass]
    public class CategoryHandlerTest
    {
        CategoryHandler CH;
        [TestInitialize]
        public void TestInitialize()
        {
            CH = new CategoryHandler();
        }
        [TestMethod]
        public void TestInitializing()
        {
            Assert.IsNotNull(CH);
        }
        [TestMethod]
        public void TestCategoryList()
        {
            Assert.AreEqual(0, CH.Categories.Count);
        }
        [TestMethod]
        public void TestCategoryListAdd()
        {
            Category cat = new Category();
            CH.Add(cat);
            Assert.AreEqual(1, CH.Categories.Count);
        }
        [TestMethod]
        public void TestCategoryListAddDuplicate()
        {
            Category cat1 = new Category("name");
            Category cat2 = new Category("name");
            CH.Add(cat1);
            CH.Add(cat2);
            Assert.AreEqual(1, CH.Categories.Count);
        }
        [TestMethod]
        public void TestCategoryListAddDuplicate2()
        {
            Category cat1 = new Category("name");
            Category cat2 = new Category("Name");
            CH.Add(cat1);
            CH.Add(cat2);
            Assert.AreEqual(1, CH.Categories.Count);
        }
        [TestMethod]
        public void TestCategoryListAddMultiple()
        {
            Category cat1 = new Category("name");
            Category cat2 = new Category("name2");
            Category cat3 = new Category("name3");
            Category cat4 = new Category("name4");
            Category cat5 = new Category("name5");
            Category cat6 = new Category("name6");
            Category cat7 = new Category("name7");
            Category cat8 = new Category("name8");
            Category cat9 = new Category("name9");
            Category cat10 = new Category("name10");
            CH.Add(cat1);
            CH.Add(cat2);
            CH.Add(cat3);
            CH.Add(cat4);
            CH.Add(cat5);
            CH.Add(cat6);
            CH.Add(cat7);
            CH.Add(cat8);
            CH.Add(cat9);
            CH.Add(cat10);
            Assert.AreEqual(10, CH.Categories.Count);
        }
        [TestMethod]
        public void TestCategoryHandlerGetByName()
        {
            Category cat = new Category("name10");
            CH.Add(cat);
            Assert.ReferenceEquals(cat, CH.GetByName("name10"));
        }

        [TestMethod]
        public void TestGetByNameNonExistant()
        {
            Assert.IsNull(CH.GetByName("nonexistingcat"));
        }

        [TestMethod]
        public void TestCategorySerialize()
        {
            Assert.IsTrue(CH.Serialize().Contains(@"{""Categories"":[],""IsLocallyLoaded"":false,""DontLoad"":false"));
        }
        [TestMethod]
        public void TestCategoryDeserialize()
        {
            CategoryHandler target = CategoryHandler.Deserialize(@"{""Categories"":[]}");
            Assert.IsNotNull(target);         
        }
        [TestMethod]
        public void TestCategoryDeserializeEmpty()
        {
            CategoryHandler target = CategoryHandler.Deserialize(@"{""Categories"":[]}");
            Assert.AreEqual(0, target.Categories.Count); 
        }

        [TestMethod]
        public void TestCategoryDeserializeBadFormat()
        {
            CategoryHandler target = CategoryHandler.Deserialize(@"[]}");
            Assert.IsNotNull(target);
            
        }

        [TestMethod]
        public void TestCategoryRemoveCourseByCategoryName()
        {
            Category cat = new Category("Category");
            Application app = new Application(1234);
            cat.Applications.Add(app);
            CH.Add(cat);
            Assert.AreEqual(1, CH.Categories.Count);
            Assert.AreEqual(1, CH.Categories[0].Applications.Count);
            CH.RemoveCourseByCategoryName("Category", 1234);
            Assert.AreEqual(0, CH.Categories[0].Applications.Count);   
        }

        [TestMethod]
        public void TestAddCategoriesSorted()
        {
            Category cat1 = new Category("DAI2");
            Category cat2 = new Category("DAI1");
            CH.Add(cat1);
            CH.Add(cat2);
            Assert.AreEqual(2, CH.Categories.Count);
            Assert.AreEqual("DAI1", CH.Categories[0].Name);
            
        }
    }
}
