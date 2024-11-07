using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace TTHDotNetCore.SnakeMinimalApi.Snake
{
    public static class SnakeEndpoints
    {
        public static void UseSnakeEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/snakes", () =>
            {
                var folderPath = "Data/Snakes.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<List<SnakeModel>>(jsonStr);

                return Results.Ok(result);
            })
                .WithName("GetSnakes")
                .WithOpenApi();

            app.MapGet("/snakes/{id}", (int id) =>
            {
                var folderPath = "Data/Snakes.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<List<SnakeModel>>(jsonStr);
                var item = result.FirstOrDefault(x => x.Id == id);

                if (item is null) return Results.BadRequest("No Data Found");

                return Results.Ok(item);
            })
                .WithName("GetSnake")
                .WithOpenApi();

            app.MapPost("/snakes", (SnakeModel snake) =>
            {
                var folderPath = "Data/Snakes.json";
                var jsonStr = File.ReadAllText(folderPath);
                var result = JsonConvert.DeserializeObject<List<SnakeModel>>(jsonStr);

                if(snake.Id == 0)
                {
                    snake.Id = result.Any() ? result.Max(snake => snake.Id) + 1 : 1;
                }

                result.Add(snake);

                return Results.Ok(result);
            })
                .WithName("CreateSnake")
                .WithOpenApi();

            app.MapPut("/snakes/{id}" , (int id, SnakeModel snake) =>
            {
                var filePath = "Data/Snakes.json";
                var jsonStr = File.ReadAllText(filePath);
                var result = JsonConvert.DeserializeObject<List<SnakeModel>>(jsonStr);

                var item = result.FirstOrDefault(x => x.Id == id);
                if (item is null) return Results.BadRequest("No Data Found");

                item.Id = id;
                item.MMName = snake.MMName;
                item.EngName = snake.EngName;
                item.Detail = snake.Detail;
                item.IsPoison = snake.IsPoison;
                item.IsDanger = snake.IsPoison;

                return Results.Ok(result);

            })
                .WithName("UpdateSnake")
                .WithOpenApi();

            app.MapDelete("/snakes/{id}", (int id) =>
            {
                var filePath = "Data/Snakes.json";
                var jsonStr = File.ReadAllText(filePath);
                var result = JsonConvert.DeserializeObject<List<SnakeModel>>(jsonStr);

                var item = result.FirstOrDefault(x => x.Id == id);
                if (item is null) return Results.BadRequest("No Data Found");

                result.Remove(item);

                return Results.Ok(result);

            })
                .WithName("DeleteSnake")
                .WithOpenApi();
        }
    }


    //public class SnakeResponseModel
    //{
    //    public SnakeModel[] Property1 { get; set; }
    //}

    public class SnakeModel
    {
        public int Id { get; set; }
        public string MMName { get; set; }
        public string EngName { get; set; }
        public string Detail { get; set; }
        public string IsPoison { get; set; }
        public string IsDanger { get; set; }
    }


}

