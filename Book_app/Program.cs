using Book_app.Data;
using Book_app.Data.Models;
using Book_app.Data.Services;
using Book_app.Exceptions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//configure DbContext with Sql

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});
builder.Services.AddScoped<BooksService>();
builder.Services.AddScoped<PublishersService>();
builder.Services.AddScoped<AuthorsService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureBuildInExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
