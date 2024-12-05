using Microsoft.AspNetCore.Mvc;
using TTHDotNetCore.Database.Models;

namespace TTHDotNetCore.MinimalApi.Blog;

public static class BlogEndPoint
{
    //public static string Test(this string i)
    //{
    //    return i.ToString();
    //}

    public static void UseBlogEndpoint(this IEndpointRouteBuilder app )
    {
        app.MapGet("/blogs", ([FromServices] AppDbContext db) =>
        {
            var model = db.TblBlogs.AsNoTracking().ToList();
            return Results.Ok(model);
        })
.WithName("GetBlogs")
        .WithOpenApi();


        app.MapPost("/blog", ([FromServices] AppDbContext db,TblBlog blog) =>
        {
            db.TblBlogs.Add(blog);
            db.SaveChanges();
            return Results.Ok(blog);
        })
        .WithName("CreateBlog")
        .WithOpenApi();

        app.MapPut("/blogs/{id}", ([FromServices] AppDbContext db,int id, TblBlog blog) =>
        {
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

        app.MapDelete("/blogs/{id}", ([FromServices] AppDbContext db,int id) =>
        {
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
