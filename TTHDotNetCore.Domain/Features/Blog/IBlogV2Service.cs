using TTHDotNetCore.Database.Models;

namespace TTHDotNetCore.Domain.Features.Blog
{
    public interface IBlogV2Service
    {
        TblBlog CreateBlog(TblBlog blog);
        bool? DeleteBlog(int id);
        TblBlog GetBlog(int id);
        List<TblBlog> GetBlogs();
        TblBlog PatchBlog(int id, TblBlog blog);
        TblBlog UpdateBlog(int id, TblBlog blog);
    }
}