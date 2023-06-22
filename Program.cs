using flightdocs_system.common;
using flightdocs_system.configs;
using flightdocs_system.repositories.Account;
using flightdocs_system.repositories.Document;
using flightdocs_system.repositories.Group;
using flightdocs_system.repositories.Permission;
using flightdocs_system.services.AccountServices;
using flightdocs_system.services.DocumentServices;
using flightdocs_system.services.GroupServices;
using flightdocs_system.services.PermissionServices;
using flightdocs_system.Utils.S3;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IS3Utils, S3Utils>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add common & helper services to the container
builder.Services.AddScoped<StringSupport>();
builder.Services.AddScoped<AccountHelper>();
builder.Services.AddScoped<GroupHelper>();
builder.Services.AddScoped<PermissionHelper>();
builder.Services.AddScoped<EmailValidatorService>();
builder.Services.AddScoped<S3ClientFactory>();
builder.Services.AddScoped<DocumentSevices>();
builder.Services.AddScoped<SQLcommon>();

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
