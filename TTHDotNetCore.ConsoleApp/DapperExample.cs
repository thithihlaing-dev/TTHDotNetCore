using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TTHDotNetCore.ConsoleApp.Models;

namespace TTHDotNetCore.ConsoleApp
{
    public class DapperExample
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123";

        public void Read()
        {
            //using (IDbConnection db = new SqlConnection(_connectionString))
            //{
            //    string query = "select * from tbl_blog where DeleteFlag = 0;";
            //    var lst = db.Query(query).ToList();
            //    foreach (var item in lst) {
            //        Console.WriteLine(item.BlogId);
            //        Console.WriteLine(item.BlogTitle);
            //        Console.WriteLine(item.BlogAuthor);
            //        Console.WriteLine(item.BlogContent);
            //    }
            //}

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from tbl_blog where DeleteFlag = 0;";
                var lst = db.Query<BlogDataModel>(query).ToList();
                foreach (var item in lst)
                {
                    Console.WriteLine(item.BlogId);
                    Console.WriteLine(item.BlogTitle);
                    Console.WriteLine(item.BlogAuthor);
                    Console.WriteLine(item.BlogContent);
                }
            }
        }

        public void Create(string title,string author, string content)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
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
                int result = db.Execute(query, new BlogDataModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content,
                });
                Console.WriteLine(result==1 ? "Saving Successful" : "Saving Fail");
            }

        }

        public void Update()
        {
            Console.WriteLine("BlogId :");
            string id = Console.ReadLine();
            int blogId = int.Parse(id);

            Console.WriteLine("Blog Title");
            string title = Console.ReadLine();

            Console.WriteLine("Blog author");
            string author = Console.ReadLine();

            Console.WriteLine("Blog Content");
            string content = Console.ReadLine();

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $@"UPDATE [dbo].[Tbl_Blog]
                    SET [BlogTitle] = @BlogTitle
                    ,[BlogAuthor] = @BlogAuthor
                    ,[BlogContent] = @BlogContent
                    ,[DeleteFlag] = 0
                    WHERE BlogId= @blogId";

                int result = db.Execute(query, new BlogDataModel
                {
                    BlogId = blogId,
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content,
                } );
                Console.WriteLine(result == 1 ? "Updating Successful." : "Updating Fail");                
            }

        }

        public void LogicalDelete()
        {
            Console.WriteLine("BlogId :");
            string id = Console.ReadLine();
            int blogId = int.Parse(id);            

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $@"UPDATE [dbo].[Tbl_Blog]
                                    SET [DeleteFlag] = 1
                                    WHERE BlogId= @BlogId";

                int result = db.Execute(query, new BlogDataModel
                {
                    BlogId = blogId,                    
                });
                Console.WriteLine(result == 1 ? "Deleting Successful." : "Deleting Fail");
            }

        }

        public void ManualDelete()
        {
            Console.WriteLine("BlogId :");
            string id = Console.ReadLine();
            int blogId = int.Parse(id);



            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $@"DELETE FROM [dbo].[Tbl_Blog]
                                         WHERE BlogId = @BlogId";

                int result = db.Execute(query, new BlogDataModel
                {
                    BlogId = blogId,
                });
                Console.WriteLine(result == 1 ? "Deleting Successful." : "Deleting Fail");
            }

        }


    }
}
