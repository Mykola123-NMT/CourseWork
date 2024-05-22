using CourseWork.Server.Interfaces;
using CourseWork.Server.Models;
using CourseWork.Server.Repositories;
using CourseWork.Server.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CourseWorkContext>();
builder.Services.AddTransient<IAmazonCategoryRepository, AmazonCategoryRepository>();
builder.Services.AddTransient<IAmazonProductRepository, AmazonProductRepository>();
builder.Services.AddTransient<IAmazonCategoryService, AmazonCategoryService>();
builder.Services.AddTransient<IAmazonProductService, AmazonProductService>();
builder.Services.AddTransient<IAmazonBestSellerAnalyzerService, AmazonBestSellerAnalyzerService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
