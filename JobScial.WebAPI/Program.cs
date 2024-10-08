using JobScial.BAL.DTOs.JWT;
using JobScial.DAL.DAOs.Implements;
using JobScial.DAL.DAOs.Interfaces;
using JobScial.DAL.Infrastructures;
using JobScial.DAL.Models;
using JobScial.DAL.Repositorys.Implementations;
using JobScial.DAL.Repositorys.Interfaces;
using JobScial.WebAPI;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
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
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
#region JWT 
builder.Services.AddSwaggerGen(options =>
{
    // using System.Reflection;
    /*var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));*////chú ý chỗ này
    options.AddSecurityRequirement(
new OpenApiSecurityRequirement()
{
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
}
);
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "GenZStyle Store Application API",
        Description = "JWT Authentication API"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
});



builder.Services.AddScoped(typeof(IDao<>), typeof(Dao<>));
builder.Services.AddScoped<IAccountDao, AccountDao>();
builder.Services.AddScoped<IAccountCertificateDao, AccountCertificateDao>();
builder.Services.AddScoped<IAccountEducationDao, AccountEducationDao>();
builder.Services.AddScoped<IAccountExperienceDao, AccountExperienceDao>();
builder.Services.AddScoped<IAccountSkillDao, AccountSkillDao>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; //chinh cho nay
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtAuth:Key"])),
        ValidateLifetime = false,
        ClockSkew = TimeSpan.Zero
    };
});
#endregion
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
