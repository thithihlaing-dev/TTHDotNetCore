using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata;
using TTHDotNetCore.RestApi.ViewModels;

namespace TTHDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskCategorysDapperController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";
        [HttpGet]
        public ActionResult Get()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"SELECT [CategoryID] AS Id
                                        ,[CategoryName] As Name
                                        ,[DeteteFlag]
                                 FROM [dbo].[TaskCategory] 
                                WHERE DeteteFlag = 0";
                List<TaskCategoryViewModel> lst = new List<TaskCategoryViewModel>();
                return Ok(lst);
            }

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"SELECT [CategoryID] AS Id
                                        ,[CategoryName] As Name
                                        ,[DeteteFlag]
                                 FROM [dbo].[TaskCategory] 
                                WHERE DeteteFlag = 0 
                                  AND CategoryID = @Id";
                var item = db.Query<TaskCategoryViewModel>(query, new TaskCategoryViewModel
                {
                    Id = id
                }).FirstOrDefault();

                return (item is null ? NotFound() : Ok(item));

            }
        }

        [HttpPost]
        public IActionResult CreateTaskCategory(TaskCategoryViewModel taskCategory)
        {
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO [dbo].[TaskCategory]
                                                    ([CategoryName]
                                                    ,[DeteteFlag])
                                      VALUES
                                            (@Name
                                             ,0)";
                var result = db.Execute(query, new TaskCategoryViewModel
                {
                    Name = taskCategory.Name
                });
                return Ok(result == 1 ? "Saving Successful" : "Saving Fail");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTaskCategory(int id , TaskCategoryViewModel taskCategory)
        {
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $@"UPDATE [dbo].[TaskCategory]
                                     SET [CategoryName] = @Name
                                        ,[DeteteFlag] = 0
                                   WHERE CategoryId = @Id";
               

                int result = db.Execute(query, new TaskCategoryViewModel
                {
                    Id = id,
                    Name = taskCategory.Name,
                });
                return Ok(result == 1 ? "Updating Successful" : "Updating Fail");

            }
        }

        [HttpPatch("{id}")]
        public IActionResult PatchTaskCategory(int id, TaskCategoryViewModel taskCategory)
        {
            string conditions = "";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                if (!string.IsNullOrEmpty(taskCategory.Name))
                {
                    conditions += " [CategoryName] = @Name, ";
                }
                if (conditions.Length == 0)
                {
                    BadRequest("Invalid Parameter");
                }

                conditions = conditions.Substring(0, conditions.Length - 2);

                string query = $@"UPDATE [dbo].[TaskCategory]
                                     SET {conditions}
                                        ,[DeteteFlag] = 0
                                   WHERE CategoryId = @Id";


                int result = db.Execute(query, new TaskCategoryViewModel
                {
                    Id = id,
                    Name = taskCategory.Name,
                });
                return Ok(result == 1 ? "Updating Successful" : "Updating Fail");

            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTaskCategory(int id, TaskCategoryViewModel taskCategory)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $@"UPDATE [dbo].[TaskCategory]
                                     SET [DeteteFlag] = 1
                                   WHERE CategoryId = @Id";


                int result = db.Execute(query, new TaskCategoryViewModel
                {
                    Id = id,
                   
                });
                return Ok(result == 1 ? "Deleting Successful" : "Deleting Fail");

            }
        }
    }
}
