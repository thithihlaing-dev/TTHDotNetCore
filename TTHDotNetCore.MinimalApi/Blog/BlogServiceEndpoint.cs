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
        app.MapGet("/blogs", () =>
        {
            BlogService service = new BlogService();
            var lst = service.GetBlogs();
            
            return Results.Ok(lst);
        })
        .WithName("GetBlogs")
        .WithOpenApi();

        app.MapGet("/blogs/{id}" , (int id) =>
        {
            BlogService service = new BlogService();
            var item = service.GetBlog(id);
            if ( item is null)
            {
                return Results.BadRequest("No Data Found");
            }
            return Results.Ok(item);
        })
        .WithName("GetBlog")
        .WithOpenApi();

        app.MapPost("/blog", (TblBlog blog) =>
        {
            BlogService service = new BlogService();
            var model = service.CreateBlog(blog);            
            return Results.Ok(model);
        })
        .WithName("CreateBlog")
        .WithOpenApi();

        app.MapPut("/blogs/{id}", (int id, TblBlog blog) =>
        {
            BlogService service = new BlogService();
            
            var item = service.UpdateBlog(id,blog);
            if (item is null)
            {
                return Results.BadRequest("No Data Found");
            }            
            return Results.Ok(item);
        })
        .WithName("UpdateBlog")
        .WithOpenApi();

        app.MapDelete("/blogs/{id}", (int id) =>
        {
            BlogService service = new BlogService();
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
