using CodePulse.API.Data;
using CodePulse1.Repository.Implementation;
using CodePulse1.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CodePulseConnectionString"));
});

//repository service
builder.Services.AddScoped<IcategoryRepository,CategoryRepository>();
builder.Services.AddScoped<IblogpostRepository, BlogPostRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => {
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
    }
    ) ;  

app.UseAuthorization();

app.MapControllers();

app.Run();
