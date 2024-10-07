namespace TTHDotNetCore.RestApi.DataModels
{
    public class BlogDataModel
    {
        public int BlogId { get; set; }

        public string? BlogTitle { get; set; }

        public string? blogAuthor { get; set; }

        public string? BlogContent { get; set; }

        public bool  DeleteFlag { get; set; }



    }
}
