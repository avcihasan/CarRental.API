using Autofac;
using Autofac.Extensions.DependencyInjection;
using CarRental.API.Modules;
using CarRental.Caching;
using CarRental.Core.Repositories;
using CarRental.Core.Services;
using CarRental.Core.UnitOfWorks;
using CarRental.Repository;
using CarRental.Repository.Repositories;
using CarRental.Repository.UnitOfWorks;
using CarRental.Service.Mapping;
using CarRental.Service.Services;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddSingleton<RedisService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();


builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")));

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

builder.Services.AddAutoMapper(typeof(MapProfile));


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*").AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                      });
});





builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(MyAllowSpecificOrigins);


app.MapControllers();

app.Run();
