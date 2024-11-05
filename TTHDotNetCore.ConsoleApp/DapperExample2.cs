using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTHDotNetCore.ConsoleApp.Models;
using TTHDotNetCore.Shared;

namespace TTHDotNetCore.ConsoleApp
{
    internal class DapperExample2
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;";

        private readonly DapperService _dapperService;

        public DapperExample2()
        {
            _dapperService = new DapperService(_connectionString);  
        }

        public void Read()
        {
            string query = "select * from tbl_blog where DeleteFlag = 0;";
            var lst = _dapperService.Query<BlogDataModel>(query).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }

        public void Create(string title , string author, string content)
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

            int result = _dapperService.Execute(query,
                new BlogDataModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });

            Console.WriteLine(result == 1 ? "Saving Successful" : "Saving Fail");
        }

       public void Edit(int id)
        {
            string query = @"SELECT * 
                            FROM tbl_blog 
                            WHERE DeleteFlag = 0 
                            AND BlogId = @BlogId;";

            var item = _dapperService.QueryFirstOrDefault<BlogDataModel>(query ,
                new BlogDataModel
                {
                    BlogId = id
                });

            if (item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
        }

        public void Update(int id , String title , String author , String content)
        {
            string query = $@"UPDATE [dbo].[Tbl_Blog]
                    SET [BlogTitle] = @BlogTitle
                    ,[BlogAuthor] = @BlogAuthor
                    ,[BlogContent] = @BlogContent
                    ,[DeleteFlag] = 0
                    WHERE BlogId= @blogId";

            var item = _dapperService.Execute(query,
                new BlogDataModel
                {
                    BlogId = id,
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });

            Console.WriteLine( item == 1 ? "Updating Successful" : "Updating Fail");
        }

        public void Delete(int id)
        {
            string query = $@"UPDATE [dbo].[Tbl_Blog]
                    SET [DeleteFlag] = 1
                    WHERE BlogId= @blogId";

            var item = _dapperService.Execute(query,
                new BlogDataModel
                {
                    BlogId = id                   
                });

            Console.WriteLine(item == 1 ? "Deleting Successful" : "Deleting Fail");
        }
    }
}

