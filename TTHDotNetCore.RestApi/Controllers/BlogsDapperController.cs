using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using TTHDotNetCore.RestApi.ViewModels;

namespace TTHDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsDapperController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";
        
        [HttpGet]
        public IActionResult GetBlogs()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"SELECT BlogId AS Id, 
                                        BlogTitle AS Title, 
                                        BlogAuthor AS Author, 
                                        BlogContent AS Content, 
                                        DeleteFlag 
                                FROM tbl_blog 
                                WHERE DeleteFlag = 0;";
                List<BlogViewModel> lst = db.Query<BlogViewModel>(query).ToList();
                return Ok(lst);
            }
           
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            using(IDbConnection db= new SqlConnection(_connectionString))
            {
                string query = @"SELECT BlogId AS Id, 
                                        BlogTitle AS Title, 
                                        BlogAuthor AS Author, 
                                        BlogContent AS Content, 
                                        DeleteFlag 
                                FROM tbl_blog 
                                WHERE DeleteFlag = 0  
                                AND BlogId=@Id";
                var item = db.Query<BlogViewModel>(query, new BlogViewModel
                {
                    Id = id
                }).FirstOrDefault();

                if (item is null)
                {
                    return NotFound();
                }
                return Ok(item);

            }
        }
    }
}
