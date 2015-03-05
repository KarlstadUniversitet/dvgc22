using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace Scheduling.Controllers
{

    public class CategoryController : Controller
    {
        private static string SessionKey="Categories";
        public CategoryHandler CH { get; set; }

        public CategoryController()
        {
            
        }

        public ActionResult Index()
        {
            CH = GetSesssionObject();
            return PartialView(CH);
        }
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            CH = GetSesssionObject();
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(category.Name))
                    CH.Add(new Category(category.Name, category.Description));
                if (Request.IsAjaxRequest())
                    return Json(CH.Serialize());
                return RedirectToAction("Index", "Home");
            }
            return View(category);
        }
        
        public string AddSubCategory(String p, String sub)
        {
            int catID;
            
            CodeHandler codeHandler = new CodeHandler();
            CH = GetSesssionObject();

            if (int.TryParse(p, out catID))
            {
                for(int i =0; i<CH.Categories.Count;i++)
                {
                    if (CH.Categories[i].id == catID)
                    {
                        CH.Categories[i].AddSubCategory(new Category(sub));
                        return CH.Serialize();
                    }
                    if (CH.Categories[i].id != catID)
                        CH.Categories[i] = recursiveAddSub(CH.Categories[i], catID , sub);
                }
            }
            return CH.Serialize();
        }

        private Category recursiveAddSub(Category recCat,int catID, String subcat)
        {
            if (recCat.Categories.Count == 0)
            {
                return recCat;
            }

            for (int i = 0; i < recCat.Categories.Count; i++)
            {
                if (recCat.Categories[i].id == catID)
                {
                    recCat.Categories[i].AddSubCategory(new Category(subcat));
                    return recCat;

                }
                else
                {
                    recCat.Categories[i] = recursiveAddSub(recCat.Categories[i], catID, subcat);
                }
            }
            return recCat;

        }         

        //OBS DETTA ÄR EN JÄTTE FULHACK EFTERSOM DET VERKAR VARA OMÖJLIGT ATT ÄNDRA PÅ ORGINALKODEN UTAN ATT SKRIVA OM ALLT
        //Det fungerar felfritt men koden borde skrivas om så att funktionen tar emot en "int cat_id_to_remove" än en nyskapad categori
        //med namnet hackat till id för den som ska bort (detta eftersom det magsikt skapas en ny cat med ett nytt id istället för att
        //skicka med rätt objekt till fuktion, som sagt detta sker magsikt)
        public ActionResult RemoveCategory(Category categoryToRemove)     
        {
            int cat_id_to_remove;
            
            if(int.TryParse(categoryToRemove.Name,out cat_id_to_remove)) //<---
            { 
                CH = GetSesssionObject();
                if (ModelState.IsValid)
                {     
                    for (int i = 0; i < CH.Categories.Count; i++) 
                    {
                        if (cat_id_to_remove == CH.Categories[i].id) 
                        {
                            CH.Categories.RemoveAt(i);
                            break;
                        }
                        else if (CH.Categories[i].Categories.Count > 0)
                        {
                            CH.Categories[i] = recursiceRemoveCategory(CH.Categories[i], cat_id_to_remove);
                        }
                    }
                Session[SessionKey] = CH;             
                if (Request.IsAjaxRequest())
                    return Json(CH.Serialize(), JsonRequestBehavior.AllowGet);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        private Category recursiceRemoveCategory(Category subcat, int check)
        {
            for (int i = 0; i < subcat.Categories.Count; i++)
            {
                if (check == subcat.Categories[i].id)
                {
                    subcat.Categories.RemoveAt(i);
                    return subcat;
                }
                else if (subcat.Categories[i].Categories.Count > 0)
                {
                    subcat.Categories[i] = recursiceRemoveCategory(subcat.Categories[i], check);
                }
                
            }
            return subcat;
        }

        [HttpPost]
        public string GetCategoriesSerialized()
        {
            CH = GetSesssionObject();
            return CH.Serialize();
        }

        public string SetCategoriesDeserialized(string jsonNotation)
        {
            CH = GetSesssionObject();
            if (!CH.DontLoad&&!string.IsNullOrEmpty(jsonNotation))
            {
                CH = CategoryHandler.Deserialize(jsonNotation);
                CH.IsLocallyLoaded = true;
                Session[SessionKey] = CH;
            }
            else if (CH.DontLoad)
            {
                CH.IsLocallyLoaded = true;
            }
            return CH.Serialize();
        }


        public ActionResult GetCategories()
        {
            CH = GetSesssionObject();
            return PartialView(CH);
        }

        public ActionResult GetCategoriesForCourses()
        {
            CH = GetSesssionObject();
            return PartialView(CH);
        }

        private CategoryHandler GetSesssionObject()
        {
            CategoryHandler result = new CategoryHandler();
            if (Session != null && Session[SessionKey] != null)
                result = Session[SessionKey] as CategoryHandler;
            return result;
        
        }
        public string AddCourseToCategory(string categoryId, string applicationCode) 
        {
            int catID; 
            
            CodeHandler codeHandler = new CodeHandler();
            CH = GetSesssionObject();
            List<Application> courseInfo = codeHandler.GetApplicationCodeList(applicationCode);
            
            if(int.TryParse(categoryId,out catID))    
                if (courseInfo.Count > 0)
                {
                    for(int i=0;i<CH.Categories.Count;i++) 
                    {
                        if(CH.Categories[i].id == catID)
                        {
                            CH.Categories[i].AddSorted(courseInfo[0]);
                            return CH.Serialize();
                        }
                        else if(CH.Categories[i].Categories.Count > 0)
                        {
                             CH.Categories[i] = RecursivAddCourseToCategory(CH.Categories[i],catID, courseInfo[0]);
                        }              
                    }
                }
            return CH.Serialize();
        }

        private Category RecursivAddCourseToCategory(Category category, int catID , Application application)
        {
            if (category.id == catID)
            {
                category.AddSorted(application);
                return category;
            }
            else if(category.Categories.Count>0)
            {
                for(int i=0;i<category.Categories.Count;i++)
                {
                    category.Categories[i] = RecursivAddCourseToCategory(category.Categories[i], catID, application);
                }
            }
            return category;
        }

        [HttpPost]
        public string RemoveCourseFromCategory(string categoryName, string applicationCode)
        {
            CH = GetSesssionObject();
            int appCode;
            if (!String.IsNullOrEmpty(categoryName) && !String.IsNullOrEmpty(applicationCode))
            {
                if (int.TryParse(applicationCode, out appCode))
                {
                    CH.RemoveCourseByCategoryName(categoryName, appCode);
                }
            }
            
            return CH.Serialize();
        }

        [HttpPost]
        public string RemoveCourseFromCategoryWithID(string categoryID, string applicationCode)
        {
            CH = GetSesssionObject();
            int appCode;
            int catID;
            if (!String.IsNullOrEmpty(categoryID) && !String.IsNullOrEmpty(applicationCode))
            {
                if (int.TryParse(applicationCode, out appCode)&&(int.TryParse(categoryID,out catID)))
                {
                    CH.RemoveCourseByCategoryID(catID, appCode);
                }
            }

            return CH.Serialize();
        }

    }
}
