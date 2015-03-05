using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Repository
{
    public class CategoryHandler
    {
        public List<Category> Categories { get; set; }
        public Boolean IsLocallyLoaded { get; set; }
        public Boolean DontLoad { get; set; }
        public string shareId { get; set; }
        public DateTime updateTime { get; set; }
        public String Description { get; set; }

        
        public CategoryHandler() 
        {
            Categories = new List<Category>();
            IsLocallyLoaded = false;
            DontLoad = false;
        }

        public void Add(Category newCat)
        {
            foreach (Category cat in Categories)
            { 
                if (cat.Name.Equals(newCat.Name,StringComparison.CurrentCultureIgnoreCase))
                {
                    return;
                }
            }
            Categories.Add(newCat);
            this.Sort();
        }

        

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static CategoryHandler Deserialize(string jsonNotation)
        {
            try
            {
                return JsonConvert.DeserializeObject<CategoryHandler>(jsonNotation);
            }
            catch
            {
                return new CategoryHandler() { IsLocallyLoaded=true};
            }
        }

        public Category GetByName(string categoryName)
        {
            Category result;
            foreach (Category cat in Categories)
            {
                if (cat.Name.Equals(categoryName, StringComparison.CurrentCultureIgnoreCase))
                {
                    return cat;
                }
                else if(cat.Categories.Count>0) 
                {
                    result = recursiveGetbyname(cat, categoryName);
                    if (result!=null && result.Name.Equals(categoryName, StringComparison.CurrentCultureIgnoreCase) )
                        return result;
                }

            }
            return null;
        }

        private Category recursiveGetbyname(Category categ, string categoryName)
        {
            Category result;
            foreach (Category cat in categ.Categories)
            {
                if (cat.Name.Equals(categoryName, StringComparison.CurrentCultureIgnoreCase))
                {
                    return cat;
                }
                else if (cat.Categories.Count > 0)
                {
                    result = recursiveGetbyname(cat, categoryName);
                    if (result!=null && result.Name.Equals(categoryName, StringComparison.CurrentCultureIgnoreCase))
                        return result;
                }

            }
            return null;
        }

        public void RemoveCourseByCategoryName(string categoryName, int applicationCode)
        {
            if (GetByName(categoryName) != null)
                GetByName(categoryName).RemoveCourseByApplicationCode(applicationCode);
        }

        public void RemoveCourseByCategoryID(int catID, int applicationcode)
        {
            for (int i = 0; i < Categories.Count; i++)
            {
                if (Categories[i].id == catID) 
                {
                    Categories[i].RemoveCourseByApplicationCode(applicationcode);
                }
                else if (Categories[i].Categories.Count > 0)
                {
                    Categories[i] = recursiveRemoveCourseByCategoryID(Categories[i], catID, applicationcode);
                }
            }
        }

        private Category recursiveRemoveCourseByCategoryID(Category category, int catID, int applicationcode)
        {
            for (int i = 0; i < category.Categories.Count; i++)
            {
                if (category.Categories[i].id == catID)
                {
                    category.Categories[i].RemoveCourseByApplicationCode(applicationcode);
                    return category;
                }
                else if (category.Categories[i].Categories.Count > 0)
                {
                    category.Categories[i] = recursiveRemoveCourseByCategoryID(category.Categories[i], catID, applicationcode);
                }
                
            }
            return category;
        }

        public void Sort()
        {
            Categories = Categories.OrderBy(x => x.Name).ToList();
        }

        private String GetAppString(Category cat)
        {
            String result = "";

            foreach (Application app in cat.Applications)
            {
                result += app.Code;
            }

            return result;
        }

        private String GetComparisonStringAux(Category cat)
        {
            String res="";
            foreach (Category cate in cat.Categories)
            {
                res += GetAppString(cate);
                res += cat.Name;
                res += cat.Description;
            }
            return res;
        }

        public String GetComparisonString()
        {
            String result = "";
            foreach (Category cat in Categories)
            {
                result += GetAppString(cat);
                result += cat.Name;
                result += cat.Description;
                result += GetComparisonStringAux(cat);
            }
            return result.Replace(" ",string.Empty);
        }
    }
}
