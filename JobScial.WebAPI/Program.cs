using JobScial.BAL.DTOs.JWT;
using JobScial.DAL.DAOs.Implements;
using JobScial.DAL.DAOs.Interfaces;
using JobScial.DAL.Infrastructures;
using JobScial.DAL.Models;
using JobScial.BAL.Repositorys.Implementations;
using JobScial.BAL.Repositorys.Interfaces;
using JobScial.WebAPI;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

using System.Reflection.Emit;
using JobScial.BAL.DTOs.FireBase;
using JobScial.BAL.Models;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var emailConfig = configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
if (emailConfig != null)
{
    builder.Services.AddSingleton<EmailConfiguration>(emailConfig);
}

builder.Services.Configure<FireBaseImage>(builder.Configuration.GetSection("FireBaseImage"));
builder.Services.Configure<JwtAuth>(builder.Configuration.GetSection("JwtAuth"));
// Add services to the container.
builder.Services.Configure<IdentityOptions>(
opts => opts.SignIn.RequireConfirmedEmail = true
    );
builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<JobSocialContext>()
                .AddDefaultTokenProviders();

var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<Account>("Accounts");
modelBuilder.EntitySet<AccountCertificate>("AccountCertificate");
modelBuilder.EntitySet<AccountEducation>("AccountEducation");
modelBuilder.EntitySet<AccountExperience>("AccountExperience");
modelBuilder.EntitySet<AccountSkill>("AccountSkill");
modelBuilder.EntitySet<School>("School");
modelBuilder.EntitySet<JobTitle>("JobTitle");
modelBuilder.EntitySet<SkillCategory>("SkillCategory");
modelBuilder.EntitySet<TimespanUnit>("TimespanUnit");
modelBuilder.EntitySet<Company>("Company");
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
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionStringDB")));


//Dependency Injections

builder.Services.AddScoped<IDbFactory, DbFactory>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
//builder.Services.AddScoped<IlikeRepository, LikeRepository>();
builder.Services.AddScoped<IPostCategoryRepository, PostCategoryRepository>();
builder.Services.AddScoped<ISharedRepository, SharedRepository>();
builder.Services.AddScoped<IAccountCertRepository, AccountCertRepository>();
builder.Services.AddScoped<IAccountEduRepository, AccountEduRepository>();
builder.Services.AddScoped<IAccountExpRepository, AccountExpRepository>();
builder.Services.AddScoped<UserManager<IdentityUser>>();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddScoped<IAccountSkillRepository, AccountSkillRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.Configure<FireBaseImage>(builder.Configuration.GetSection("FireBaseImage"));

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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()  // Cho phép tất cả các origin
               .AllowAnyMethod()  // Cho phép tất cả các phương thức (GET, POST, PUT, DELETE, ...)
               .AllowAnyHeader(); // Cho phép tất cả các header
    });
});



var app = builder.Build();



app.UseCors("AllowAllOrigins");

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
