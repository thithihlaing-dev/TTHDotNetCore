using Microsoft.AspNetCore.Mvc;
using System.Net;
using TTHDotNetCore.Domain.Features.Blog;

namespace TTHDotNetCore.MinimalApi.Blog;

public static class BlogServiceEndpoint
{
    //public static string Test(this string i)
    //{
    //    return i.ToString();
    //}

    // Presentation Layer
    public static void UseBlogServiceEndpoint(this IEndpointRouteBuilder app )
    {
        app.MapGet("/blogs", ([FromServices] BlogService service ) =>
        {
            var lst = service.GetBlogs();
            
            return Results.Ok(lst);
        })
        .WithName("GetBlogs")
        .WithOpenApi();

        app.MapGet("/blogs/{id}" , ([FromServices] BlogService service,int id) =>
        {
            var item = service.GetBlog(id);
            if ( item is null)
            {
                return Results.BadRequest("No Data Found");
            }
            return Results.Ok(item);
        })
        .WithName("GetBlog")
        .WithOpenApi();

        app.MapPost("/blog", ([FromServices] BlogService service,TblBlog blog) =>
        {
            var model = service.CreateBlog(blog);            
            return Results.Ok(model);
        })
        .WithName("CreateBlog")
        .WithOpenApi();

        app.MapPut("/blogs/{id}", ([FromServices] BlogService service,int id, TblBlog blog) =>
        {
           
            var item = service.UpdateBlog(id,blog);
            if (item is null)
            {
                return Results.BadRequest("No Data Found");
            }            
            return Results.Ok(item);
        })
        .WithName("UpdateBlog")
        .WithOpenApi();

        app.MapDelete("/blogs/{id}", ([FromServices] BlogService service,int id) =>
        {
            var item = service.DeleteBlog(id);
            if (item is false)
            {
                return Results.BadRequest("No Data Found");
            }
            return Results.Ok("Deleting Success");

        })
        .WithName("DeleteBlog")
        .WithOpenApi();
       
    }
}
