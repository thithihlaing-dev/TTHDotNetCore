// See https://aka.ms/new-console-template for more information
using TTHDotNetCore.ConsoleApp3;

Console.WriteLine("Hello, World!");


HttpClientExample example = new HttpClientExample();
//await example.ReadAsync();
await example.Edit(1);
await example.Edit(110);

