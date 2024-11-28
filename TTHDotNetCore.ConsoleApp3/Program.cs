// See https://aka.ms/new-console-template for more information
using TTHDotNetCore.ConsoleApp3;

Console.WriteLine("Hello, World!");


HttpClientExample httpClientExample = new HttpClientExample();
//await httpClientExample.ReadAsync();
//await httpClientExample.Edit(1);
//await httpClientExample.Edit(110);
//await httpClientExample.Create(1, "title", "body");

//await httpClientExample.Update(100, "title", "body",10);

await httpClientExample.Delete(1);


