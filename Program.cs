using Microsoft.EntityFrameworkCore;
using projetoWebApi.Context;
using projetoWebApi.Extensions;
using projetoWebApi.Filters;
using projetoWebApi.Logging;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => 
        options.JsonSerializerOptions
        //Ignora Referencia Ciclica
            .ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddScoped<ApiLoggingFilter>();
builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information,
}));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string mysqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(mysqlConnection, 
    ServerVersion.AutoDetect(mysqlConnection)));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHandler();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
