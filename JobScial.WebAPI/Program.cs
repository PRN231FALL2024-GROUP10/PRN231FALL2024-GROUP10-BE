using JobScial.BAL.DTOs.JWT;
using JobScial.DAL.DAOs.Implements;
using JobScial.DAL.DAOs.Interfaces;
using JobScial.DAL.Infrastructures;
using JobScial.DAL.Models;
using JobScial.DAL.Repositorys.Implementations;
using JobScial.DAL.Repositorys.Interfaces;
using JobScial.WebAPI;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System.Reflection.Emit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<Account>("Accounts");
builder.Services.AddControllers()
.AddOData(opt => opt
               .AddRouteComponents(routePrefix: "odata", model: modelBuilder.GetEdmModel())
               .Select()
               .Expand()
               .OrderBy()
               .Filter()
               .Count()
               .SetMaxTop(100));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    /*c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());*/
    c.OperationFilter<ODataQueryOptionsOperationFilter>();
});
builder.Services.AddDbContext<JobSocialContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//Dependency Injections
builder.Services.Configure<JwtAuth>(builder.Configuration.GetSection("JwtAuth"));
builder.Services.AddScoped<IDbFactory, DbFactory>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();


builder.Services.AddScoped(typeof(IDao<>), typeof(Dao<>));
builder.Services.AddScoped<IAccountDao, AccountDao>();
builder.Services.AddScoped<IAccountCertificateDao, AccountCertificateDao>();
builder.Services.AddScoped<IAccountEducationDao, AccountEducationDao>();
builder.Services.AddScoped<IAccountExperienceDao, AccountExperienceDao>();
builder.Services.AddScoped<IAccountSkillDao, AccountSkillDao>();
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
