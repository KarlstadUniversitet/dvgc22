using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scheduling.Controllers;
using System.Web.Mvc;
using Repository;
using System.Web;
using System.Collections.Generic;

namespace Scheduling.Tests
{
    //class MockHttpSession : HttpSessionStateBase
    //{
    //    Dictionary<string, object> sessionObject = new Dictionary<string, object>();
    //    public override object this[string key]
    //    {
    //        get
    //        {
    //            return sessionObject[key];
    //        }
    //        set
    //        {
    //            base[key] = value;
    //        }
    //    }
    //}

    //[TestClass]
    //public class CategoryControllerTest
    //{
    //    CategoryController CC;
    //    [TestInitialize]
    //    public void TestInitialize()
    //    {

    //         CC = new CategoryController();
    //    }
    //    [TestMethod]
    //    public void TestInitializing()
    //    {
    //        Assert.IsNotNull(CC);
    //    }
    //    [TestMethod]
    //    public void TestGetCategoriesSerialized()
    //    {
    //        string result = CC.GetCategoriesSerialized();
    //        Assert.IsNotNull(result);
    //    }
    //    [TestMethod]
    //    public void TestSetCategoriesDeserialized()
    //    {
    //        PartialViewResult results = CC.SetCategoriesDeserialized(@"{""Categories"":[]}") as PartialViewResult;
    //        CategoryHandler ch = results.Model as CategoryHandler;
    //        Assert.AreEqual(0, ch.Categories.Count);           
    //    }
    //}
}
