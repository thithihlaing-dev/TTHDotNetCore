
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

            if (bird.Id == 0)
            {
                bird.Id = result.Tbl_Bird.Count != 0 ? result.Tbl_Bird.Max(bird => bird.Id) + 1 : 1;
            }
            result.Tbl_Bird.Add(bird);

            var jsonStrToWrite = JsonConvert.SerializeObject(result);
            File.WriteAllText(folderPath, jsonStrToWrite);

            return Results.Ok(result);
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
          

            var index = result.Tbl_Bird.FindIndex(x => x.Id == id);
            if (index != -1)
            {
                result.Tbl_Bird[index] = new BirdModel
                {
                    Id = id,
                    BirdEnglishName = bird.BirdEnglishName,
                    BirdMyanmarName = bird.BirdMyanmarName,
                    Description = bird.Description,
                    ImagePath = bird.ImagePath
                };
            }
            var jsonStrToWrite = JsonConvert.SerializeObject(result);
            File.WriteAllText(folderPath, jsonStrToWrite);


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
            
            result.Tbl_Bird.Remove(item);

            var jsonStrToWrite = JsonConvert.SerializeObject(result);
            File.WriteAllText(folderPath, jsonStrToWrite);

            return Results.Ok(result);

        })
        .WithName("DeleteBird")
        .WithOpenApi();







    }

    public class BirdResponseModel
    {
        public List<BirdModel> Tbl_Bird { get; set; }
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
