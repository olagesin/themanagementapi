using Microsoft.AspNetCore.Mvc;
using MovieManagementApi.Presentation;
using MovieManagementAPI;
using MovieManagementAPI.Configurations;
using Services.AutomapperConfig;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigurePostgresContext(builder.Configuration);

builder.Services.ConfigureSwagger();
builder.Services.ConfigureAutoMapper();

builder.Services.AddApiVersioningExtension();

builder.Services.AddApiVersionedExplorerExtension();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(AssemblyReference)
    .Assembly);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseCors(option =>
option.AllowAnyOrigin()
.AllowAnyHeader()
.AllowAnyMethod()
.WithExposedHeaders("X-Pagination"));

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
