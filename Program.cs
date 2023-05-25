using flightdocs_system.common;
using flightdocs_system.configs;
using flightdocs_system.repositories.Account;
using flightdocs_system.services.AccountServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//add configuration
builder.Services.AddDbContext<SystemDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultCons"));
});

// Add services to the container.
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add common & helper services to the container
builder.Services.AddScoped<StringSupport>();
builder.Services.AddScoped<AccountHelper>();

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
