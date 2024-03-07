using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingConsoleApp.AdoDotNetExamples
{
    public class AdoDotNetExample
    {
        public void Read()
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            sqlConnectionStringBuilder.DataSource = "THANZAWMYINT\\SQLEXPRESS"; // server name
            sqlConnectionStringBuilder.InitialCatalog = "TestDB"; // db name
            sqlConnectionStringBuilder.UserID = "sa";
            sqlConnectionStringBuilder.Password = "sasa@123";

            Console.WriteLine("Connection String => " + sqlConnectionStringBuilder.ConnectionString);

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

            Console.WriteLine("connection opening...");
            connection.Open();
            Console.WriteLine("connection opened...");

            string query = @"SELECT [BlogId]
                          ,[BlogTitle]
                          ,[BlogAuthor]
                          ,[BlogContent]
                      FROM [dbo].[Tbl_Blogs]";

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            Console.WriteLine("connection closing...");
            connection.Close();
            Console.WriteLine("connection closed...");

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("Title..." + dr["BlogTitle"]);
                Console.WriteLine("Author..." + dr["BlogAuthor"]);
                Console.WriteLine("Content..." + dr["BlogContent"]);
            }
        }

        public void Edit(int id)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            sqlConnectionStringBuilder.DataSource = "THANZAWMYINT\\SQLEXPRESS"; // server name
            sqlConnectionStringBuilder.InitialCatalog = "TestDB"; // db name
            sqlConnectionStringBuilder.UserID = "sa";
            sqlConnectionStringBuilder.Password = "sasa@123";

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

            connection.Open();

            string query = $@"SELECT [BlogId]
                          ,[BlogTitle]
                          ,[BlogAuthor]
                          ,[BlogContent]
                      FROM [dbo].[Tbl_Blogs] Where BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No data found!");
                return; // not to do following tasks
            }

            DataRow dr = dt.Rows[0];
            Console.WriteLine("Title..." + dr["BlogTitle"]);
            Console.WriteLine("Author..." + dr["BlogAuthor"]);
            Console.WriteLine("Content..." + dr["BlogContent"]);
        }

        public void Create(string title, string author, string content)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            sqlConnectionStringBuilder.DataSource = "THANZAWMYINT\\SQLEXPRESS"; // server name
            sqlConnectionStringBuilder.InitialCatalog = "TestDB"; // db name
            sqlConnectionStringBuilder.UserID = "sa";
            sqlConnectionStringBuilder.Password = "sasa@123";

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

            connection.Open();

            string query = @"INSERT INTO [dbo].[Tbl_Blogs]
                   ([BlogTitle]
                   ,[BlogAuthor]
                   ,[BlogContent])
             VALUES
                   (@BlogTitle
                   ,@BlogAuthor
                   ,@BlogContent)";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue(parameterName: "@BlogTitle", title);
            cmd.Parameters.AddWithValue(parameterName: "@BlogAuthor", author);
            cmd.Parameters.AddWithValue(parameterName: "@BlogContent", content);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";

            Console.WriteLine(message);

        }

        public void Update(int id, string title, string author, string content)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            sqlConnectionStringBuilder.DataSource = "THANZAWMYINT\\SQLEXPRESS"; // server name
            sqlConnectionStringBuilder.InitialCatalog = "TestDB"; // db name
            sqlConnectionStringBuilder.UserID = "sa";
            sqlConnectionStringBuilder.Password = "sasa@123";

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

            connection.Open();

            string query = $@"UPDATE [dbo].[Tbl_Blogs]
               SET [BlogTitle] = @BlogTitle
                  ,[BlogAuthor] = @BlogAuthor
                  ,[BlogContent] = @BlogContent
             WHERE [BlogId] = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue(parameterName: "@BlogId", id);
            cmd.Parameters.AddWithValue(parameterName: "@BlogTitle", title);
            cmd.Parameters.AddWithValue(parameterName: "@BlogAuthor", author);
            cmd.Parameters.AddWithValue(parameterName: "@BlogContent", content);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";

            Console.WriteLine(message);

        }

        public void Delete(int id)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            sqlConnectionStringBuilder.DataSource = "THANZAWMYINT\\SQLEXPRESS"; // server name
            sqlConnectionStringBuilder.InitialCatalog = "TestDB"; // db name
            sqlConnectionStringBuilder.UserID = "sa";
            sqlConnectionStringBuilder.Password = "sasa@123";

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

            connection.Open();

            string query = $@"Delete FROM [dbo].[Tbl_Blogs] Where [BlogId] = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue(parameterName: "@BlogId", id);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";

            Console.WriteLine(message);

        }
    }
}
