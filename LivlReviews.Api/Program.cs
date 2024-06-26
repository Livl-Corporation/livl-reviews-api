using System.Text;
using System.Text.Json.Serialization;
using LivlReviews.Api.Services;
using LivlReviews.Domain;
using LivlReviews.Domain.Domain_interfaces_input;
using LivlReviews.Domain.Domain_interfaces_output;
using LivlReviews.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using LivlReviews.Email;
using LivlReviews.Infra;
using LivlReviews.Infra.Data;
using LivlReviews.Infra.Repositories;
using LivlReviews.Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using User = LivlReviews.Infra.Data.User;

var builder = WebApplication.CreateBuilder(args);

// Add services

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Livl Reviews API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] { }
        }
    });
});

builder.Services.AddProblemDetails();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add DB Contexts
// Move the connection string to user secrets for release
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnection")));

builder.Services.AddScoped<TokenService, TokenService>();
builder.Services.AddScoped<IPaginatedRepository<Product>, PaginatedEntityRepository<Product>>();
builder.Services.AddScoped<IRepository<Category>, EntityRepository<Category>>();
builder.Services.AddScoped<IRepository<InvitationToken>, EntityRepository<InvitationToken>>();
builder.Services.AddScoped<IPaginatedRepository<Request>, PaginatedEntityRepository<Request>>();
builder.Services.AddScoped<IRepository<ProductStock>, EntityRepository<ProductStock>>();
builder.Services.AddScoped<IRepository<Review>, EntityRepository<Review>>();
builder.Services.AddScoped<IRequestInventory, RequestInventory>();
builder.Services.AddScoped<INotificationManager, EmailManager>();
builder.Services.AddScoped<INotificationContent, EmailContent>();
builder.Services.AddScoped<IRepository<User>, EntityRepository<User>>();
builder.Services.AddScoped<IImportManager, ImportManager>();
builder.Services.AddScoped<IStockManager, StockManager>();
builder.Services.AddScoped<IImportInventory, ImportInventory>();
builder.Services.AddScoped<ICategoryInventory, CategoryInventory>();
builder.Services.AddScoped<IProductInventory, ProductInventory>();
builder.Services.AddScoped<IStockInventory, StockInventory>();
builder.Services.AddScoped<IImportInventory, ImportInventory>();
builder.Services.AddScoped<IRepository<Import>, EntityRepository<Import>>();
builder.Services.AddScoped<IRepository<Category>, EntityRepository<Category>>();
builder.Services.AddScoped<IRepository<Product>, EntityRepository<Product>>();
builder.Services.AddScoped<IReviewManager, ReviewManager>();
builder.Services.AddScoped<IReviewInventory, ReviewInventory>();

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Specify identity requirements
// Must be added before .AddAuthentication otherwise a 404 is thrown on authorized endpoints
builder.Services
    .AddIdentity<User, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = true;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// These will eventually be moved to a secrets file, but for alpha development appsettings is fine
var validIssuer = builder.Configuration.GetValue<string>("JwtTokenSettings:ValidIssuer");
var validAudience = builder.Configuration.GetValue<string>("JwtTokenSettings:ValidAudience");
var symmetricSecurityKey = builder.Configuration.GetValue<string>("JwtTokenSettings:SymmetricSecurityKey");

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.IncludeErrorDetails = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = validIssuer,
            ValidAudience = validAudience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(symmetricSecurityKey)
            ),
        };
    });


var smtpSettings = builder.Configuration.GetSection("SmtpSettings");
var smtpPassword = builder.Configuration["Smtp:Password"];

builder.Services.Configure<SmtpSettings>(smtpSettings);
builder.Services.AddTransient<INotificationSender, SmtpEmailSender>(provider => new SmtpEmailSender(provider.GetRequiredService<IOptions<SmtpSettings>>(), smtpPassword));
builder.Services.AddTransient<INotificationContent, EmailContent>();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStatusCodePages();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
