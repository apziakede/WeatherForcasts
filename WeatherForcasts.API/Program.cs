using Application.Features.Forcasts.Queries;
using DbUp.Helpers;
using DbUp;
using Infrastructure.DataContext;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
  
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Configure EF with Sql services
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});


builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 4;
}).AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["AuthSettings:Audience"],
        ValidIssuer = builder.Configuration["AuthSettings:Issuer"],
        RequireExpirationTime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AuthSettings:Key"])),
        ValidateIssuerSigningKey = true
    };
});

Log.Logger=new LoggerConfiguration()
    .MinimumLevel.Information()
     .WriteTo.Console()
    .WriteTo.File("logs/log-.txt",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Error()
     .WriteTo.Console()
    .WriteTo.File("logs/errorLog-.txt",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddMediatR(typeof(GetAllForcastsQuery));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllForcastsQuery>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

void DBMigrationScript()
{
    string? conStr = builder.Configuration.GetConnectionString("Default");
    EnsureDatabase.For.SqlDatabase(conStr);
    var upgrader = DeployChanges.To.SqlDatabase(conStr)
        .WithScriptsFromFileSystem(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SqlScripts", "db"))
        .WithTransactionPerScript()
       .JournalTo(new NullJournal())
        .LogToConsole()
        .Build();
    upgrader.PerformUpgrade();
}

void TableMigrationScript()
{
    string? conStr = builder.Configuration.GetConnectionString("Default");
    EnsureDatabase.For.SqlDatabase(conStr);
    var upgrader = DeployChanges.To.SqlDatabase(conStr)
        .WithScriptsFromFileSystem(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SqlScripts", "SqlTables"))
        .WithTransactionPerScript()
       .JournalTo(new NullJournal())
        .LogToConsole()
        .Build();
    upgrader.PerformUpgrade();
}

void StoredProcMigrationScript()
{
    string? conStr = builder.Configuration.GetConnectionString("Default");
    EnsureDatabase.For.SqlDatabase(conStr);
    var upgrader = DeployChanges.To.SqlDatabase(conStr)
        .WithScriptsFromFileSystem(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SqlScripts", "SqlProcs"))
        .WithTransactionPerScript()
      .JournalTo(new NullJournal())
        .LogToConsole()
        .Build();
    upgrader.PerformUpgrade();
}

DBMigrationScript();
TableMigrationScript();
StoredProcMigrationScript();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
