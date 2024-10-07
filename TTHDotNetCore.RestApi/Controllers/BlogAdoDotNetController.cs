using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TTHDotNetCore.RestApi.DataModels;
using TTHDotNetCore.RestApi.ViewModels;

namespace TTHDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";

        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogViewModel> lst = new List<BlogViewModel>();
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                //var item = new BlogViewModel
                //{
                //    Id = Convert.ToInt32(reader["BlogId"]),
                //    Title = Convert.ToString(reader["BlogTitle"]),
                //    Author = Convert.ToString(reader["BlogAuthor"]),
                //    Content = Convert.ToString(reader["BlogContent"]),
                //    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"]),
                //};
                //lst.Add(item);

                lst.Add(new BlogViewModel
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"])

                });


            }
            connection.Close();

            return Ok(lst);
        }
    }
}
