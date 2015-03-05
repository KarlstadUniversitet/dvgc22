using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
namespace Repository
{
    public class DatabaseHandler
    {
        //private static SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True");

        public static void SetDescription(Category cat, Int32 ScheduleID)
        {
            SqlConnection connection = SetupConnection();
            SqlCommand sql = new SqlCommand("UPDATE CATEGORY SET Description = @Description WHERE Id=@insertedId", connection);
            sql.Parameters.Add(new SqlParameter("Description", cat.Description));
            sql.Parameters.Add(new SqlParameter("insertId", ScheduleID));
            sql.ExecuteNonQuery();
            connection.Close();

        }

        public static String GetDescription(Int32 ScheduleID)
        {
            SqlConnection connection = SetupConnection();
            SqlCommand sql = new SqlCommand("SELECT Description FROM SCHEDULE WHERE Id=@insertedId", connection);
            sql.Parameters.Add(new SqlParameter("Id",ScheduleID));
            String Description = sql.ExecuteScalar().ToString();
            connection.Close();

            return Description;

        }

        public static void SetDate(String HashId)
        {
            SqlConnection connection = SetupConnection();
            SqlCommand sql = new SqlCommand("UPDATE SCHEDULE SET TimeStamp = GETDATE() WHERE Hash = @HashId", connection);
            sql.Parameters.Add(new SqlParameter("HashId", HashId));
            sql.ExecuteNonQuery();
            connection.Close();
        }

        public static DateTime GetDate(String HashId)
        {
            if (HashId == null) { return DateTime.MinValue; }
            DateTime Date = new DateTime();
            SqlConnection connection = SetupConnection();
            SqlCommand sql = new SqlCommand("SELECT * FROM SCHEDULE WHERE Hash = @HashId", connection);
            sql.Parameters.Add(new SqlParameter("HashId", HashId));

            SqlDataReader reader = sql.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Date = (reader.GetDateTime(2));
                }
            }
            connection.Close();
            return Date;
        }

        private static String GetHashId(Int32 Id)
        {
            String hashId=null;
            SqlConnection connection = SetupConnection();
            SqlCommand sql = new SqlCommand("SELECT * FROM SCHEDULE WHERE Id=@Id", connection);
            sql.Parameters.Add(new SqlParameter("Id", Id));

            SqlDataReader reader = sql.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                hashId = reader.GetString(1);
            }
            reader.Close();
            connection.Close();
            return hashId;

        }

        public static int GetId(String HashId)
        {
            int id=0;
            SqlConnection connection = SetupConnection();
            SqlCommand sql = new SqlCommand("SELECT * FROM SCHEDULE WHERE Hash=@hash", connection);
            sql.Parameters.Add(new SqlParameter("hash", HashId));

            SqlDataReader reader = sql.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                id = reader.GetInt32(0);
            }
            reader.Close();
            connection.Close();
            return id;
        }

        private static void SaveCategory(Category cat, Int32 ScheduleID)
        {
            SqlConnection connection = SetupConnection();
            SqlCommand sql = new SqlCommand("INSERT INTO CATEGORY(Name,Schedule_id,Description) OUTPUT INSERTED.ID VALUES(@name,@insertId,@description)", connection);
           
            sql.Parameters.Add(new SqlParameter("name", cat.Name));
            sql.Parameters.Add(new SqlParameter("insertId", ScheduleID));
            sql.Parameters.Add(new SqlParameter("description", cat.Description));

            Int32 catId = (Int32)sql.ExecuteScalar();
            SaveSubCategories(cat.Categories, catId, ScheduleID);
            connection.Close();
            SaveCourses(catId, cat.Applications);

        }

        public static string SaveCategoryHandler(CategoryHandler ch)
        {
            
            SqlConnection connection = SetupConnection();
            SqlCommand sql = new SqlCommand("DECLARE @T TABLE(Id int);INSERT INTO SCHEDULE OUTPUT INSERTED.ID INTO @T DEFAULT VALUES;SELECT * FROM @T", connection);
            Int32 insertId = (Int32)sql.ExecuteScalar();
            connection.Close();
            foreach (Category cat in ch.Categories)
            {
                SaveCategory(cat, insertId);
            }
            string hash = GetHashId(insertId);
            return hash;
        }

        private static void SaveSubCategories(List<Category> categories,Int32 parent,Int32 scheduleId)
        {
            SqlCommand sql;
            SqlConnection connection;
            foreach (Category category in categories)
            {
                connection = SetupConnection();
                sql = new SqlCommand("INSERT INTO CATEGORY(Name,Schedule_id,Parent,Description) OUTPUT INSERTED.ID VALUES(@name,@scheduleId,@parent,@description)", connection);
                 sql.Parameters.Add(new SqlParameter("name", category.Name));
                sql.Parameters.Add(new SqlParameter("scheduleId", scheduleId));
                sql.Parameters.Add(new SqlParameter("parent", parent));
                sql.Parameters.Add(new SqlParameter("description", category.Description));
                SaveCourses((Int32)sql.ExecuteScalar(), category.Applications);
                connection.Close();
            }
            
        }

        public static DateTime UpdateCategoryHandler(CategoryHandler ch)
        {
            SqlConnection connection = SetupConnection();
            SqlCommand sql = new SqlCommand("DELETE FROM CATEGORY WHERE Schedule_id = @id", connection);
            Int32 id = GetId(ch.shareId);
            sql.Parameters.Add(new SqlParameter("id", id));
            sql.ExecuteNonQuery();
            connection.Close();
            SetDate(ch.shareId);
            foreach (Category cat in ch.Categories)
            {
                SaveCategory(cat, id);
            }
            return GetDate(ch.shareId);
        }

        public static CategoryHandler FetchCategoryHandler(string hash)
        {
            CategoryHandler categoryHandler = new CategoryHandler();
            SqlConnection connection = SetupConnection();
            SqlCommand sql=new SqlCommand("SELECT Id,Hash, TimeStamp FROM SCHEDULE WHERE Hash=@hash", connection);
            sql.Parameters.Add(new SqlParameter("hash", hash));
           
            SqlDataReader reader = sql.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                categoryHandler.Categories = FetchCategories(reader.GetInt32(0));
                categoryHandler.updateTime = reader.GetDateTime(2);
                categoryHandler.shareId = reader.GetString(1);
            }
            reader.Close();
            connection.Close();
            categoryHandler.DontLoad = true;
            return categoryHandler;
        }


        private static SqlConnection SetupConnection()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True");
            connection.Close();
            connection.Open();
            return connection;
        }

        private static void SaveCourses(Int32 categoryId, List<Application> courses)
        {
            SqlConnection connection;
            foreach (Application app in courses)
            {
                connection = SetupConnection();
                SqlCommand sql=new SqlCommand("INSERT INTO COURSE(appcode,Category_id,Name,CourseCode) VALUES(@appcode, @Category_id,@Name,@CourseCode)",connection);
                sql.Parameters.Add(new SqlParameter("Name",app.CourseName));
                sql.Parameters.Add(new SqlParameter("appcode", app.Code));
                sql.Parameters.Add(new SqlParameter("Category_id", categoryId));
                sql.Parameters.Add(new SqlParameter("CourseCode", app.CourseCode));
                sql.ExecuteNonQuery();
                connection.Close();
            }

        }

        private static List<Category> FetchCategories(Int32 scheduleId)
        {
            SqlConnection connection = SetupConnection();
            List<Category> result = new List<Category>();
            SqlCommand sql = new SqlCommand("SELECT * FROM CATEGORY WHERE Parent IS NULL AND Schedule_id=@scheduleId", connection);
            sql.Parameters.Add(new SqlParameter("scheduleId",scheduleId));
            SqlDataReader reader=sql.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Category newCategory = new Category(reader.GetString(1).Trim());
                    newCategory.Categories=FetchSubCategories(reader.GetInt32(0));//hämtar id
                    newCategory.Applications = FetchCourses(reader.GetInt32(0));
                    newCategory.Description = reader.GetString(4);
                    result.Add(newCategory);
                }
            }
            else
            {
                Debug.WriteLine("No categories for shceudendeleid");
            }
            reader.Close();
            connection.Close();
            return result;
        }
        private static List<Category> FetchSubCategories(Int32 parentId)
        {
            SqlConnection connection = SetupConnection();
            List<Category> result = new List<Category>();
            SqlCommand sql = new SqlCommand("SELECT * FROM CATEGORY WHERE Parent=@parentId", connection);
            sql.Parameters.Add(new SqlParameter("parentId", parentId));
            SqlDataReader reader = sql.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Category subCategory = new Category(reader.GetString(1));//tar andra attributet ur category (Name)

                    subCategory.Applications = FetchCourses(reader.GetInt32(0));
                    result.Add(subCategory);
                }
            }
            connection.Close();
            reader.Close();
            return result;
        }
        private static List<Application> FetchCourses(int categoryId)
        {
            SqlConnection connection = SetupConnection();
            List<Application> result = new List<Application>();
            SqlCommand sql = new SqlCommand("SELECT Appcode,CourseCode,Name FROM COURSE WHERE Category_id=@categoryId", connection);
            sql.Parameters.Add(new SqlParameter("categoryId",categoryId));
            SqlDataReader reader = sql.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Application app = new Application(reader.GetInt32(0),reader.GetString(1),reader.GetString(2));
                    result.Add(app);
                }
            }
            else
            {
                Debug.WriteLine("No categories for shceudendeleid");
            }
            reader.Close();
            connection.Close();
            return result;
        }


    }
}
