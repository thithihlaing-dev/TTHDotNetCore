using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TTHDotNetCore.ConsoleApp3
{
    public class HttpClientExample
    {
        private readonly HttpClient _client;
        private readonly string _postEndPoint = "https://jsonplaceholder.typicode.com/posts";

        public HttpClientExample()
        {
            _client = new HttpClient();
        }


        public async Task ReadAsync()
        {
            var response = await _client.GetAsync(_postEndPoint);

            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();

                Console.WriteLine(jsonStr);
            }



        }

        public async Task Edit(int id)
        {
            var response = await _client.GetAsync($"{_postEndPoint}/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found.");
                return;
            }

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonStr);


            }


        }

        public async Task  Create(int userId,string title,string body)
        {
            PostModel requestModel = new PostModel()
            {
                title = title,
                body = body,
                userId = userId
            };// C# object

            var jsonRequest = JsonConvert.SerializeObject(requestModel);//change json code
            var content = new StringContent(jsonRequest,Encoding.UTF8,Application.Json);//(json,language,json string) Change httpContent 
            var response = await _client.PostAsync(_postEndPoint, content);//(endpoint , httpContent)
            if (response.IsSuccessStatusCode) 
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }

        }

        public async Task Update(int id, string title,string body,int userId)
        {
            PostModel requestModel = new PostModel()
            {
                id = id,
                title = title,
                body = body,
                userId = userId

            };// C# object

            var jsonRequest = JsonConvert.SerializeObject(requestModel);//change to json code
            var content = new StringContent(jsonRequest, Encoding.UTF8, Application.Json);//(json,language,json string) Change httpContent  string
            var response = await _client.PutAsync($"{_postEndPoint}/{id}", content); ;//(endpoint , httpContent)
            if (response.IsSuccessStatusCode) 
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task Delete(int id)
        {
            //var response = await _client.GetAsync($"{_postEndPoint}/{id}");
            var response = await _client.DeleteAsync($"{_postEndPoint}/{id}");


            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found.");
                return;
            }
            //var responseDelete = await _client.DeleteAsync($"{_postEndPoint}/{id}");

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonStr);
            }

        }

    }


    
    public class PostModel
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }

}
