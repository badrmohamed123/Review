using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Review.model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
var Connectionstring = builder.Configuration.GetConnectionString("Defaultstring");
builder.Services.AddDbContext<ApplicationDbContext>(Options=>Options.UseSqlServer(Connectionstring));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option=>
option.SwaggerDoc("v1",new Microsoft.OpenApi.Models.OpenApiInfo
{
    Title = "Review",
    Version = "v1",
})
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
