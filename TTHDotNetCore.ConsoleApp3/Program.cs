// See https://aka.ms/new-console-template for more information
using TTHDotNetCore.ConsoleApp3;

Console.WriteLine("Hello, World!");


//HttpClientExample httpClientExample = new HttpClientExample();
//await httpClientExample.ReadAsync();
//await httpClientExample.Edit(1);
//await httpClientExample.Edit(110);
//await httpClientExample.Create(1, "title", "body");

//await httpClientExample.Update(100, "title", "body",10);

//await httpClientExample.Delete(1);


//RestClientExample restClientExample = new RestClientExample();
//await restClientExample.Read();
//await restClientExample.Edit(1);
//await restClientExample.Edit(110);
//await restClientExample.Create("title", "body", 1);

//await restClientExample.Update(1, "title", "body", 1);
//await restClientExample.Delete(1);


Console.WriteLine("Waiting for api....");
Console.ReadLine();

RefitExample refitExample = new RefitExample();
await refitExample.Run();

Console.WriteLine("Hello");



