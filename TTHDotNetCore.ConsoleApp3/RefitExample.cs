using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTHDotNetCore.ConsoleApp3
{
    internal class RefitExample
    {
        public async Task Run()
        {
            var blogApi = RestService.For<IBlogApi>("https://localhost:7219");
            var lst = await blogApi.GetBlogs();
            foreach (var item in lst) 
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
            }

            //Get Blog found
            var item2 = await blogApi.GetBlog(1);

            //Get Blog Not Found
            try
            {
                var item3 = await blogApi.GetBlog(100);
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("No data found.");
                }
            }

            //Create Blog
            var item4 = await blogApi.CreateBlog(new BlogModel
            {
                BlogTitle = "test",
                BlogAuthor = "test",
                BlogContent = "test",
            });
        }
    }
}
