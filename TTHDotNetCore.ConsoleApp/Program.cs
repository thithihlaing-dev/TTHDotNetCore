// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;
using TTHDotNetCore.ConsoleApp;

Console.WriteLine("Hello, World!");
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
// enter Ctrl+a Ctrl+d ( format )

// max connection = 100
// 100 = 99

//AdoDotNetExample adoDotNetExample= new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Edit();
//adoDotNetExample.Update();
//adoDotNetExample.LogicalDelete();
//adoDotNetExample.ManualDelete();


//DapperExample dapperExample = new DapperExample();
//dapperExample.Create("Title", "Author", "Content");
//dapperExample.Edit(1);
//dapperExample.Edit(10);
//dapperExample.Update();
//dapperExample.LogicalDelete();
//dapperExample.ManualDelete();

EFCoreExample eFCoreExample =new EFCoreExample();
//eFCoreExample.Read();
eFCoreExample.Create("EFcore Title", "EFcore Author", "EFcore Content");

Console.ReadKey();
