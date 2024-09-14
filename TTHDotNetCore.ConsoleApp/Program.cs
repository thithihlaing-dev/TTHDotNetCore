// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;

//Console.WriteLine("Hello, World!");
//Console.ReadLine();
//Console.ReadKey();

// md => markdown

// C# <=> Database

// ADO.NNET
// Dapper (ORM)
// EFCore / Entity Framework (ORM)

// C# => sql query =>

// nuget

// Ctrl + . (press for sql connection)
// f5 (debugging)
// f9 (break point)
// f10 (one line enter)

// max connection = 100
// 100 = 99


string connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;";//DataSource=sever name;Initial Catalog=Database Name;User=Login User Name;Password= Ligin Password;
Console.WriteLine("Connection String :" + connectionString);
SqlConnection connection = new SqlConnection(connectionString);

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

SqlCommand cmd = new SqlCommand(query,connection); // sql cmd create
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

Console.ReadKey();
