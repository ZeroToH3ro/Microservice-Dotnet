using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebPlatform;
using WebPlatform.SyncDataServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option => option.SwaggerDoc("v1", new OpenApiInfo {Title = "Platform Service", Version = "v1"}));
builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseInMemoryDatabase("InMem")
);
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddHttpClient<ICommandServices, HttpCommandDataClient>();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();
// app.UseHttpsRedirection();

PrepDb.PrepPopulation(app);

app.Run();
