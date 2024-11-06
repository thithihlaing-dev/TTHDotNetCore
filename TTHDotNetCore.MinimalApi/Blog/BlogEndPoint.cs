using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TTHDotNetCore.Database.Models;

namespace TTHDotNetCore.MinimalApi.Blog
{
    public static class BlogEndPoint
    {
        //public static string Test(this string i)
        //{
        //    return i.ToString();
        //}

        public static void UseBlogEndpoint(this IEndpointRouteBuilder app )
        {
            app.MapGet("/blogs", () =>
            {
                AppDbContext db = new AppDbContext();
                var model = db.TblBlogs.AsNoTracking().ToList();
                return Results.Ok(model);
            })
.WithName("GetBlogs")
.WithOpenApi();


            app.MapPost("/blog", (TblBlog blog) =>
            {
                AppDbContext db = new AppDbContext();
                db.TblBlogs.Add(blog);
                db.SaveChanges();
                return Results.Ok(blog);
            })
            .WithName("CreateBlog")
            .WithOpenApi();

            app.MapPut("/blogs/{id}", (int id, TblBlog blog) =>
            {
                AppDbContext db = new AppDbContext();
                var item = db.TblBlogs.FirstOrDefault(x => x.BlogId == id);

                if (item is null)
                {
                    return Results.BadRequest("No Data Found");
                }

                item.BlogTitle = blog.BlogTitle;
                item.BlogAuthor = blog.BlogAuthor;
                item.BlogContent = blog.BlogContent;

                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Results.Ok(blog);
            })
            .WithName("UpdateBlog")
            .WithOpenApi();

            app.MapDelete("/blogs/{id}", (int id) =>
            {
                AppDbContext db = new AppDbContext();
                var item = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
                if (item is null)
                {
                    return Results.BadRequest("No Data Found");
                }

                db.Entry(item).State = EntityState.Deleted;
                db.SaveChanges();
                return Results.Ok(item);

            })
            .WithName("DeleteBlog")
            .WithOpenApi();
           
        }
    }
}
