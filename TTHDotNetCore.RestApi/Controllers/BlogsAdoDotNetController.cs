using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using TTHDotNetCore.RestApi.DataModels;
using TTHDotNetCore.RestApi.ViewModels;

namespace TTHDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsAdoDotNetController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";

        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogViewModel> lst = new List<BlogViewModel>();
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = @"SELECT [BlogId]
                                    [BlogTitle]
                                    [BlogAuthor]
                                    [BlogContent]
                                    [DeleteFlag]
                              FROM [dbo].[Tbl_Blog]
                              WHERE DeleteFlag = 0";
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

        [HttpPost]
        public IActionResult CreateBlog(BlogViewModel blog)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
                                                ([BlogTitle]
                                                ,[BlogAuthor]
                                                ,[BlogContent]
                                                ,[DeleteFlag])
                                           VALUES
                                                 (@BlogTitle
                                                    ,@BlogAuthor
                                                    ,@BlogContent
                                                    ,0)";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle",blog.Title);
            cmd.Parameters.AddWithValue("@BlogAuthor",blog.Author);
            cmd.Parameters.AddWithValue("@BlogContent",blog.Content);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = (result == 1 ? "Saving Successful" : "Saving Fail");
        
            return Ok(message);
        }



        [HttpGet("{id}")]
        public IActionResult GetBlog(int id) { 

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = @"SELECT [BlogId]
                                ,[BlogTitle]
                                ,[BlogAuthor]
                                ,[BlogContent]
                                ,[DeleteFlag]
                            FROM [dbo].[Tbl_Blog] 
                            WHERE BlogId = @BlogId";
            
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adapter.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                return NotFound();

            }
            DataRow dr = dt.Rows[0];
            var item = new BlogViewModel
            {
                Id = Convert.ToInt32(dr["BlogId"]),
                Title = Convert.ToString(dr["BlogTitle"]),
                Author = Convert.ToString(dr["BlogAuthor"]),
                Content = Convert.ToString(dr["BlogContent"]),
                DeleteFlag = Convert.ToBoolean(dr["DeleteFlag"]),
            };
            connection.Close();

           
            return Ok(item);

        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogViewModel blog)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = $@"UPDATE [dbo].[Tbl_Blog]
                                    SET [BlogTitle] = @BlogTitle
                                        ,[BlogAuthor] = @BlogAuthor
                                        ,[BlogContent] = @BlogContent
                                        ,[DeleteFlag] = 0
                                    WHERE BlogId= @BlogId"; 
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            cmd.Parameters.AddWithValue("@BlogContent", blog.Content);

            int restult = cmd.ExecuteNonQuery();
            connection.Close();

            return Ok(restult == 1 ? "Updating Successful" : "Updating Fail");

        }


        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogViewModel blog)
        {
            string conditions = "";

            if (!string.IsNullOrEmpty(blog.Title))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";

            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                conditions += " [BlogContent] = @BlogContent, ";
            }

            if (conditions.Length == 0)
            {
                BadRequest("Invalid Parameter");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = $@"UPDATE [dbo].[Tbl_Blog] SET {conditions} WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
           if (!string.IsNullOrEmpty(blog.Title))
            {
                cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                cmd.Parameters.AddWithValue("@BlogContent", blog.Content);
            }

            int restult = cmd.ExecuteNonQuery();
            connection.Close();

            return Ok(restult == 1 ? "Updating Successful" : "Updating Fail");
        
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id, BlogViewModel blog)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            //string query = $@"UPDATE [dbo].[Tbl_Blog]
            //                    SET [DeleteFlag] = 1
            //                    WHERE BlogId= @BlogId";
            string query = $@"DELETE FROM [dbo].[Tbl_Blog]
                WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
           

            int restult = cmd.ExecuteNonQuery();
            connection.Close();

            return Ok(restult == 1 ? "Deleting Successful" : "Deleting Fail");

        }
    }
}
