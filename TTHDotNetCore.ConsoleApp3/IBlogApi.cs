using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTHDotNetCore.ConsoleApp3
{
    public interface IBlogApi
    {
        [Get("/api/blogs")]
        Task<List<BlogModel>> GetBlogs();


        [Get("/api/blogs/{id}")]
        Task<BlogModel> GetBlog(int v);

        [Post("/api/blogs")]
        Task<BlogModel> CreateBlog(BlogModel blogModel);
    }

    public class BlogModel
    {
        public int BlogId { get; set; }

        public string? BlogTitle { get; set; }

        public string? BlogAuthor { get; set; }

        public string? BlogContent { get; set; }

        public bool DeleteFlag { get; set; }



    }
}
