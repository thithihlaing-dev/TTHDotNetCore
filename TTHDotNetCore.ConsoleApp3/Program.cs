// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

HttpClient client = new HttpClient();
var response = await client.GetAsync("https://jsonplaceholder.typicode.com/posts");

if (response.IsSuccessStatusCode) 
{
    var jsonStr = await response.Content.ReadAsStringAsync();
    Console.WriteLine("hello1");
    Console.WriteLine(jsonStr);
    Console.WriteLine("hello2");

}
