using MagicVilla_VillAPI.Data;
using MagicVilla_VillAPI.Logging;
using Microsoft.EntityFrameworkCore;
using Serilog;
var builder = WebApplication.CreateBuilder(args);
//Log.Logger = (Serilog.ILogger)new LoggerConfiguration().MinimumLevel.Debug().
//    WriteTo.File("log/villaLogs.txt",rollingInterval: RollingInterval.Day).CreateLogger();
//builder.Host.UseSerilog();
// Add services to the container.builder.Services.AddControllers();//.AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers(option => { 
    //option.ReturnHttpNotAcceptable = true;
    }).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ILogging, LoggingV2>();
builder.Services.AddDbContext<ApplicationDbContext>
(option => 
{ 
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSqlConnection")); 
});

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
