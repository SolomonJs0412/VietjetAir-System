using System.Text;
using flightdocs_system.common;
using flightdocs_system.configs;
using flightdocs_system.repositories.Account;
using flightdocs_system.repositories.Document;
using flightdocs_system.repositories.Group;
using flightdocs_system.repositories.Type;
using flightdocs_system.services.AccountServices;
using flightdocs_system.services.DocumentServices;
using flightdocs_system.services.GroupServices;
using flightdocs_system.services.TypeServices;
using flightdocs_system.Utils.S3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

//add configuration
builder.Services.AddDbContext<SystemDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultCons"));
});

// Add services to the container.
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IS3Utils, S3Utils>();
builder.Services.AddScoped<ITypeRepository, TypeRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add common & helper services to the container
builder.Services.AddScoped<StringSupport>();
builder.Services.AddScoped<AccountHelper>();
builder.Services.AddScoped<GroupHelper>();
builder.Services.AddScoped<TypeServices>();
builder.Services.AddScoped<EmailValidatorService>();
builder.Services.AddScoped<S3ClientFactory>();
builder.Services.AddScoped<DocumentSevices>();
builder.Services.AddScoped<SQLcommon>();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Nothings",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abc-egc-24-23he0-323-q232q")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.DictionaryKeyPolicy = null;
    });

builder.Services.AddDirectoryBrowser();

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
