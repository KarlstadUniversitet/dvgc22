using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Diagnostics;
using Domain;
using Repository;

namespace Scheduling.Tests
{
    [TestClass]
    public class DataBaseTest
    {

        private static SqlConnection SetupConnection()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True");
            connection.Close();
            connection.Open();
            return connection;
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True");
        [TestInitialize]
        public void TestInitialize()
        {
            /*con.Open();
            SqlCommand derp = new SqlCommand("SELECT * FROM SCHEDULE", con);
            SqlDataReader reader=derp.ExecuteReader();
            if (reader.HasRows) { return; }
            reader.Close();
            SqlCommand sql = new SqlCommand("INSERT INTO SCHEDULE DEFAULT VALUES", con);
            sql.ExecuteNonQuery();
            sql = new SqlCommand("INSERT INTO CATEGORY(Name,Schedule_id) VALUES('derp',1)", con);
            sql.ExecuteNonQuery();
            sql = new SqlCommand("INSERT INTO CATEGORY(Name,Schedule_id) VALUES('herp',1)", con);
            sql.ExecuteNonQuery();
            sql = new SqlCommand("INSERT INTO COURSE(Appcode,Category_id) VALUES(24400,1)", con);
            sql.ExecuteNonQuery();
            sql = new SqlCommand("INSERT INTO COURSE(Appcode,Category_id) VALUES(24391,1)", con);
            sql.ExecuteNonQuery();
            sql = new SqlCommand("INSERT INTO COURSE(Appcode,Category_id) VALUES(24402,2)", con);
            sql.ExecuteNonQuery();
            Debug.WriteLine("RAN INTEIELIAZALIEZE ONCE");
            reader.Close();*/
        }
        [TestMethod]
        public void Test()
        {

            /*SqlCommand cmd=new SqlCommand("SELECT * FROM COURSE",con);
            SqlDataReader reader=cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Debug.WriteLine(reader.GetInt32(1));
                }
            }
            else
            {
                Debug.WriteLine("Database is empty");
            }*/
        }
        [TestMethod]
        public void TestSaveCategory()
        {
            // Category category=new Category("test");
            // category.AddSorted(new Application(24400,"DVGC19","Dastasaker"));
            //DatabaseHandler.SaveCategory(category);

            //Assert.AreEqual("derp", DatabaseHandler.FetchCategories(1)[0].Name.Trim());

        }

        [TestMethod]
        public void TestSaveCategoryHandler()
        {
            /* CategoryHandler catHandler = new CategoryHandler();
             Category kategori1 = new Category("foo");
             catHandler.Add(kategori1);
             Application course = new Application(24400,"DVGC19","Datasaker");
             kategori1.AddSorted(course);
            // Int32 id = DatabaseHandler.SaveCategoryHandler(catHandler);
             Assert.AreEqual(24400,DatabaseHandler.FetchCategories(id)[0].Applications[0].Code);*/
        }

        [TestMethod]
        public void TestFetchCategoryHandler()
        {
            //Assert.AreEqual(24400,DatabaseHandler.FetchCategoryHandler(1).Categories[0].Applications[0].Code);
        }

        /*[TestMethod]
        public void TestInsertCategoryCurrTime()
        {
            SqlConnection connection = SetupConnection();
            SqlCommand sql = new SqlCommand("INSERT INTO SCHEDULE (TimeStamp) OUTPUT INSERTED.ID VALUES (GETDATE()) ", connection);
            Int32 insertId = (Int32)sql.ExecuteScalar();
            sql = new SqlCommand("SELECT * FROM SCHEDULE WHERE ID=@insertId", connection);
            sql.Parameters.Add(new SqlParameter("insertId", insertId));
            SqlDataReader reader = sql.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    System.Diagnostics.Debug.WriteLine(reader.GetInt32(0));
                    System.Diagnostics.Debug.WriteLine(reader.GetDateTime(2));
                }
            }

            connection.Close();
        }

    }*/
    }
}