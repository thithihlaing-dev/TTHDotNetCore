using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTHDotNetCore.ConsoleApp
{
    public class AdoDotNetExample
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;";//DataSource=sever name;Initial Catalog=Database Name;User=Login User Name;Password= Ligin Password;

        public void Read()
        {
             Console.WriteLine("Connection String :" + _connectionString);
            SqlConnection connection = new SqlConnection(_connectionString);

            Console.WriteLine("connection opening...");
            connection.Open(); // open connection
            Console.WriteLine("connection opened");

            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog] 
    WHERE DeleteFlag = 0"; // sql query

            SqlCommand cmd = new SqlCommand(query, connection); // sql cmd create
                                                                //SqlDataAdapter adapter = new SqlDataAdapter(cmd); // C# run sql get data 
                                                                //DataTable dt = new DataTable(); // create data table
                                                                //adapter.Fill(dt); // After C# run then get data insert into dataset (for execute)

            SqlDataReader reader = cmd.ExecuteReader(); // SqlDataAdapter + DataTable and instead of "adapter.Fill(dt)"
            while (reader.Read())
            {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);
                Console.WriteLine(reader["DeleteFlag"]);

            }


            Console.WriteLine("connecction closing");
            connection.Close(); // close connection
            Console.WriteLine("connection closed");



            // DataSet
            // DataTable
            // DataRow
            // DataColumn

            // foreach (DataRow dr in dt.Rows)
            // {
            //    Console.WriteLine(dr["BlogTitle"]);
            //    Console.WriteLine(dr["BlogAuthor"]);
            //    Console.WriteLine(dr["BlogContent"]);
            //    Console.WriteLine(dr["DeleteFlag"]);

            // }
        }


        public void Create()
        {
            //string connectionString2 = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;";//DataSource=sever name;Initial Catalog=Database Name;User=Login User Name;Password= Ligin Password;


            SqlConnection connection2 = new SqlConnection(_connectionString);
            connection2.Open();

            Console.WriteLine("Blog Title :");
            string title = Console.ReadLine();

            Console.WriteLine("Blog Author :");
            string author = Console.ReadLine();

            Console.WriteLine("Blog Content");
            string content = Console.ReadLine();

            //string queryInsert = $@"INSERT INTO [dbo].[Tbl_Blog]
            //           ([BlogTitle]
            //           ,[BlogAuthor]
            //           ,[BlogContent]
            //           ,[DeleteFlag])
            //     VALUES
            //           ('{title}'
            //           ,'{author}'
            //            ,'{content}'
            //           ,0)";

            //SqlCommand cmd2 = new SqlCommand(queryInsert,connection2);
            //SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
            //DataTable dt2 = new DataTable();
            //adapter2.Fill(dt2);


            string queryInsert = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent]
           ,[DeleteFlag])
     VALUES
           (@BlogTitle
            ,@BlogAuthor
            ,@BlogContent
           ,0)";

            SqlCommand cmd2 = new SqlCommand(queryInsert, connection2);
            cmd2.Parameters.AddWithValue("@BlogTitle", title);
            cmd2.Parameters.AddWithValue("@BlogAuthor", author);
            cmd2.Parameters.AddWithValue("@BlogContent", content);
            int result = cmd2.ExecuteNonQuery();
            //SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
            //DataTable dt2 = new DataTable();
            //adapter2.Fill(dt2);
            connection2.Close();

            //if (result == 1)
            //{
            //    Console.WriteLine("Saving Successful");
            //}else
            //    Console.WriteLine("Saving Fail");

            Console.WriteLine(result == 1 ? "Saving Successful." : "Saving Fail");


        }

        public void Edit()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            Console.WriteLine("BlogId :");
            string blogId = Console.ReadLine();

            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog] 
    WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query,connection);
            cmd.Parameters.AddWithValue("@blogId",blogId);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No data found");
                return;
            }

            DataRow dr = dt.Rows[0];
            Console.WriteLine(dr["BlogId"]);
            Console.WriteLine(dr["BlogTitle"]);
            Console.WriteLine(dr["BlogAuthor"]);
            Console.WriteLine(dr["BlogContent"]);
            
        }

        public void Update()
        {
            Console.WriteLine("Blog ID :");
            string blogId = Console.ReadLine();

            Console.WriteLine("Blog Title :");
            string title = Console.ReadLine();

            Console.WriteLine("Blog Author :");
            string author = Console.ReadLine();

            Console.WriteLine("Blog Content");
            string content = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
      ,[DeleteFlag] = 0
 WHERE BlogId= @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", blogId);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine(result == 1 ? "Updating Successful." : "Updating Fail");

        }

        public void LogicalDelete()
        {
            Console.WriteLine("Blog ID :");
            string blogId = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET [DeleteFlag] = 1
 WHERE BlogId= @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", blogId);           
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine(result == 1 ? "Logical Delete Successful." : "Logical Delete Fail");

        }

        public void ManualDelete()
        {
            Console.WriteLine("Blog ID :");
            string blogId = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = $@"DELETE FROM [dbo].[Tbl_Blog]
                WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", blogId);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine(result == 1 ? "Manual Delete Successful." : "Manual Delete Fail");

        }
    }
}
