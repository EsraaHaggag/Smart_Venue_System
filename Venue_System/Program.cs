//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;
//using Venue_System.API.RealTime;
//using Venue_System.API.Web.Hubs;
//using Venue_System.Application.Interfaces.Services;
//using Venue_System.Infrastructure;
//using Venue_System.Infrastructure.Identity;
//using Venue_System.Infrastructure.Services;
//using Venue_System.Infrastructure.Services.Email;
//using Venue_System.Infrustrucure.DBContext;
//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<ApplictionDBContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("dbcontext")));
//builder.Services.AddDependencyInjection();

//// Validate Password
//builder.Services.Configure<IdentityOptions>(options =>
//{
//    options.Password.RequireDigit = true;
//    options.Password.RequiredLength = 8;
//    options.Password.RequireUppercase = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireNonAlphanumeric = true;
//});

//builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
//    .AddEntityFrameworkStores<ApplictionDBContext>()
//    .AddDefaultTokenProviders();

//builder.Services.Configure<EmailSettings>(
//    builder.Configuration.GetSection("EmailSettings"));
//builder.Services.AddScoped<IVenueNotifierService, VenueNotifier>();

//builder.Services.AddAuthentication("Bearer")
//    .AddJwtBearer("Bearer", options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateIssuerSigningKey = true,
//            ValidateLifetime = true,

//            ValidIssuer = builder.Configuration["Jwt:Issuer"],
//            ValidAudience = builder.Configuration["Jwt:Audience"],

//            IssuerSigningKey = new SymmetricSecurityKey(
//                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//        };
//    });

//builder.Services.AddSwaggerGen(options =>
//{
//    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
//        Scheme = "bearer",
//        BearerFormat = "JWT",
//        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
//        Description = "Enter: Bearer YOUR_TOKEN"
//    });

//    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
//    {
//        {
//            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
//            {
//                Reference = new Microsoft.OpenApi.Models.OpenApiReference
//                {
//                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            new string[] {}
//        }
//    });
//});

//builder.Services.AddSignalR();
//builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

//var app = builder.Build();

//app.MapHub<VenueHub>("/venueHub");

//using (var scope = app.Services.CreateScope())
//{
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

//    string[] roles = { "Customer", "VenueOwner", "Admin" };

//    foreach (var role in roles)
//    {
//        if (!await roleManager.RoleExistsAsync(role))
//        {
//            await roleManager.CreateAsync(new IdentityRole<Guid>(role));
//        }
//    }
//}

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt; // ????? ?????? ?? ??? Claims
using System.Text;
using Venue_System.API.RealTime;
using Venue_System.API.Web.Hubs;
using Venue_System.Application.Interfaces.Services;
using Venue_System.Infrastructure;
using Venue_System.Infrastructure.Identity;
using Venue_System.Infrastructure.Services;
using Venue_System.Infrastructure.Services.Email;
using Venue_System.Infrustrucure.DBContext;

// 1. ?? ????? ??? Claims Mapping (??? ?? ???? ??? ??? ?? ?????)
// ??? ????? ???? ??? ?? ?? ????? ????? ??? Claims ???? ??????
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

var builder = WebApplication.CreateBuilder(args);

// --- ????? ??????? (Services) ---

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ????? ????? ????????
builder.Services.AddDbContext<ApplictionDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbcontext")));

// ????? ??? Dependency Injection ????? ??
builder.Services.AddDependencyInjection();

// ??????? ???? ?????? ?? Identity
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
});

// ????? ???? ??? Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplictionDBContext>()
    .AddDefaultTokenProviders();

// ??????? ?????? ?????????? ???????? ??????
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IVenueNotifierService, VenueNotifier>();

// --- ??????? ??? JWT Authentication ---
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
})
.AddJwtBearer("Bearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),

        // ????? ????? ??? Claims ??????? ?? ???? ??? Identity
        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
        NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
    };
});

// --- ??????? Swagger ?? ??? Bearer Token ---
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter: Bearer YOUR_TOKEN"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddSignalR();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

var app = builder.Build();

// --- ????? ??? Roles ???????? ??? ??????? ---
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    string[] roles = { "Customer", "VenueOwner", "Admin" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(role));
        }
    }
}

// --- ??????? ??? Middleware Pipeline ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ??????? ??? "???": Authentication ??? Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<VenueHub>("/venueHub");

app.Run();
