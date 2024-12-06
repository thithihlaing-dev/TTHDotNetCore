using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TTHDotNetCore.Database.Models;
using TTHDotNetCore.Domain.Features.Blog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// UI
// BL
// DA

builder.Services.AddDbContext<AppDbContext>( opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
}
    );
//builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IBlogService, BlogV2Service>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
