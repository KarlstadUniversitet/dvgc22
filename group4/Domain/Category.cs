using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain
{
    public class Category
    {
        private static Random r = new Random();

        [StringLength(10, ErrorMessage="Maximum length for name is 10 characters"),Required,Display(Name="Category Name", Prompt="Example 'DAI'"), RegularExpression(@"^[\w\d\/å/ä/ö/Å/Ä/Ö]+$", ErrorMessage="Category name can only contain alphanumeric characters")]
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Application> Applications { get; set; }
        public List<Category> Categories { get; set; }

        public int id;

        public Category():this(string.Empty) {}
       
        public Category(string name)
        {
            Applications = new List<Application>();
            Categories   = new List<Category>();

            Name = name;
            Description = "";
            id = r.Next();
        }

        public Category(string name, string description)
        {
            Applications = new List<Application>();
            Categories = new List<Category>();

            Name = name;
            Description = description;
            id = r.Next();
        }
       
        public string ApplicationCodesCommaSeparated()
        {
            List<int> applicationCodes = new List<int>();
            foreach (Application app in Applications) 
            {
                applicationCodes.Add(app.Code);
            }
            return String.Join(",", applicationCodes);
        }

        public void RemoveCourseByApplicationCode(int applicationCode)
        {
            for (int i = 0; i < Applications.Count; i++)
            {
                if (Applications[i].Code == applicationCode)
                {
                    Applications.RemoveAt(i);
                    break;
                }
            }
        }

        public void AddSorted(Application app)
        {

            if (app != null && !Applications.Select(x => x.Code).Contains(app.Code))
            {
                Applications.Add(app);
                Applications = Applications.OrderBy(x => x.CourseCode).ToList();
            }
        }

        public void AddSubCategory(Category subcat)
        {
            Categories.Add(subcat);
            Categories = Categories.OrderBy(x => x.Name).ToList();
        }

    }
}
