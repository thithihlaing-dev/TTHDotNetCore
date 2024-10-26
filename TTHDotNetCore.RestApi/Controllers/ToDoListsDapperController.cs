using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using TTHDotNetCore.RestApi.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TTHDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListsDapperController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";

        [HttpGet]
        public IActionResult GetToDoLists()
        {
            using(IDbConnection db = new SqlConnection())
            {
                string query = @"SELECT 
    t.[TaskID] AS Id,
    t.[TaskTitle] AS Title,
    t.[TaskDescription] As Description,
    t.[CategoryID],
    t.[PriorityLevel],
    t.[Status],
    t.[DueDate],
    t.[CreatedDate],
    t.[CompletedDate],
    c.[CategoryName],
    c.[DeleteFlag]
FROM 
    [dbo].[ToDoList] AS t
JOIN 
    [dbo].[TaskCategory] AS c
ON 
    t.[CategoryID] = c.[CategoryID];";
                List<ToDoListViewModel> lst = new List<ToDoListViewModel>();
                return Ok(lst);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetToDoLists(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"SELECT 
                    t.[TaskID] AS Id,
                    t.[TaskTitle] AS Title,
                    t.[TaskDescription] AS Description,
                    t.[CategoryID],
                    t.[PriorityLevel],
                    t.[Status],
                    t.[DueDate],
                    t.[CreatedDate],
                    t.[CompletedDate],
                    c.[CategoryName]
                FROM 
                    [dbo].[ToDoList] AS t
                JOIN 
                    [dbo].[TaskCategory] AS c
                ON 
                    t.[CategoryID] = c.[CategoryID]
                WHERE t.TaskID = @Id;";


                var item = db.Query<ToDoListViewModel>(query, new ToDoListViewModel{ Id = id }).FirstOrDefault();
                        return (item is null ? NotFound() : Ok(item));

            };
        }

        [HttpPost]
        public IActionResult CreateToDoList( ToDoListViewModel toDoListView)
        {
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO [dbo].[ToDoList]
           ([TaskTitle]
           ,[TaskDescription]
           ,[CategoryID]
           ,[PriorityLevel]
           ,[Status]
           ,[DueDate]
           ,[CreatedDate]
           ,[CompletedDate]
           ,[DeleteFlag])
     VALUES
           (@Title
           ,@Description
           ,@CategoryID
           ,@PriorityLevel
           ,@Status
           ,GETDATE()
           ,GETDATE()  
           ,NULL       
           ,0);"
                ;
                var result = db.Execute(query, new ToDoListViewModel
                {
                    Title = toDoListView.Title,
                    Description = toDoListView.Description,
                    CategoryID = toDoListView.CategoryID,
                    PriorityLevel = toDoListView.PriorityLevel,
                    Status = toDoListView.Status
                });
                return Ok(result == 1 ? "Saving Successful" : "Saving Fail");
            }           
        }



        [HttpPut("{id}")]
        public IActionResult UpdateToDoList(int id ,ToDoListViewModel toDoListView)
        {
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $@"UPDATE [dbo].[ToDoList]
   SET  
        [TaskTitle] = @Title,
       [TaskDescription] = @Description,
       [CategoryID] = @CategoryID,
       [PriorityLevel] = @PriorityLevel,
       [Status] = @Status,
       [DueDate] = GETDATE(),
       [CreatedDate] = GETDATE(),
       [CompletedDate] = GETDATE(),
       [DeleteFlag] = 0
 WHERE [TaskID] = @Id;  
";

                int result = db.Execute(query, new ToDoListViewModel
                {
                    Id = id,
                    Title = toDoListView.Title,
                    Description = toDoListView.Description,
                    CategoryID = toDoListView.CategoryID,
                    PriorityLevel = toDoListView.PriorityLevel,
                    Status = toDoListView.Status,
                   
                });
                return Ok(result == 1 ? "Updating Successful" : "Updating Fail");
            }
        }


        [HttpPatch("{id}")]
        public IActionResult PatchToDoList(int id, ToDoListViewModel toDoListView)
        {
            string conditions = "";

            
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                if (!string.IsNullOrEmpty(toDoListView.Title))
                {
                    conditions += " [TaskTitle] = @Title, ";
                }
                if (!string.IsNullOrEmpty(toDoListView.Description))
                {
                    conditions += " [TaskDescription] = @Description, ";
                }
                if (toDoListView.CategoryID.HasValue)
                {
                    conditions += " [CategoryID] = @CategoryID, ";
                }
                if (toDoListView.PriorityLevel.HasValue)
                {
                    conditions += " [PriorityLevel] = @PriorityLevel, ";
                }
                if (!string.IsNullOrEmpty(toDoListView.Status))
                {
                    conditions += " [Status] = @Status, ";
                }
                if (conditions.Length == 0)
                {
                    BadRequest("Invalid Parameter");
                }

                conditions = conditions.Substring(0, conditions.Length - 2);

                string query = $@"UPDATE [dbo].[ToDoList]
                                    SET 
                                        {conditions} 
                                        WHERE [TaskID] = @Id;  
                                        ";

                int result = db.Execute(query, new ToDoListViewModel
                {
                    Id = id,
                    Title = toDoListView.Title,
                    Description = toDoListView.Description,
                    CategoryID = toDoListView.CategoryID,
                    PriorityLevel = toDoListView.PriorityLevel,
                    Status = toDoListView.Status,

                });
                return Ok(result == 1 ? "Updating Successful" : "Updating Fail");
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteToDoList(int id, ToDoListViewModel toDoListView)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $@"UPDATE [dbo].[ToDoList]
   SET  
        
       [DeleteFlag] = 1
 WHERE [TaskID] = @Id;  
";

                int result = db.Execute(query, new ToDoListViewModel
                {
                    Id = id,                 

                });
                return Ok(result == 1 ? "Updating Successful" : "Updating Fail");
            }
        }
    }
}
