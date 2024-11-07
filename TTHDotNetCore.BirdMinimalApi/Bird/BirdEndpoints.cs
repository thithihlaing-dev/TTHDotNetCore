
namespace TTHDotNetCore.BirdMinimalApi.Bird;

public static class  BirdEndpoints
{
    public static void UseBirdEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/birds", () =>
        {
            string folderPath = "Data/Birds.json";
            var jsonStr = File.ReadAllText(folderPath);
            var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;
            return Results.Ok(result.Tbl_Bird);
        })
.WithName("GetBirds")
.WithOpenApi();


        app.MapGet("/birds/{id}", (int id) =>
        {
            string folderPath = "Data/Birds.json";
            var jsonStr = File.ReadAllText(folderPath);
            var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

            var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);

            if (item is null) return Results.BadRequest("No data Found.");

            return Results.Ok(item);

        })
        .WithName("GetBird")
        .WithOpenApi();


        app.MapPost("/birds", (BirdModel bird) =>
        {
            string folderPath = "Data/Birds.json";
            var jsonStr = File.ReadAllText(folderPath);
            var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;
            var birdList = result.Tbl_Bird?.ToList() ?? new List<BirdModel>();

            if (bird.Id == 0)
            {
                bird.Id = birdList.Any() ? birdList.Max(bird => bird.Id) + 1 : 1;
            }
            birdList.Add(bird);

            if (birdList is null) return Results.BadRequest("No data Found.");

            return Results.Ok(birdList);
        })
        .WithName("CreateBird")
        .WithOpenApi();

        app.MapPut("/birds/{id}", (int id, BirdModel bird) =>
        {
            string folderPath = "Data/Birds.json";
            var jsonStr = File.ReadAllText(folderPath);
            var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr);

            var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
            if (item is null) return Results.BadRequest("No data Found.");

            item.Id = id;
            item.BirdEnglishName = bird.BirdEnglishName;
            item.BirdMyanmarName = bird.BirdMyanmarName;
            item.Description = bird.Description;
            item.ImagePath = bird.ImagePath;



            return Results.Ok(result);


        })
        .WithName("UpdateBird")
        .WithOpenApi();


        app.MapDelete("/birds/{id}", (int id) =>
        {
            string folderPath = "Data/Birds.json";
            var jsonStr = File.ReadAllText(folderPath);
            var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr);

            var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
            if (item is null) return Results.BadRequest("No data Found.");

            //result.Tbl_Bird = result.Tbl_Bird.Where((bird, i) => i != id+1 ).ToArray();

            var index = Array.FindIndex(result.Tbl_Bird, x => x.Id == id);

            result.Tbl_Bird = result.Tbl_Bird.Where((bird, i) => i != index).ToArray();

            return Results.Ok(result);


        })
        .WithName("DeleteBird")
        .WithOpenApi();







}

    public class BirdResponseModel
    {
        public BirdModel[] Tbl_Bird { get; set; }
    }

    public class BirdModel
    {
        public int Id { get; set; }
        public string BirdMyanmarName { get; set; }
        public string BirdEnglishName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
}
