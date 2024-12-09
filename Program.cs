
using System.Reflection;
using sirius.Repositories;
using sirius.Repositories.Implementations;
using sirius.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<SiriusDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Default");
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 27)),
        mySqlOptions => { mySqlOptions.EnableRetryOnFailure(); });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IObjectifsRepository, ObjectifsRepository>();
builder.Services.AddScoped<ILivrableRepository, LivrableRepository>();
builder.Services.AddScoped<IHypothesisCategoryRepository, HypothesisCategoryRepository>();
builder.Services.AddScoped<IOperationalPrioritizationRepository, OperationalPrioritizationRepository>();
builder.Services.AddScoped<IHypothesisRepository, HypothesisRepository>();
builder.Services.AddScoped<IHypothesisHistoryRepository, HypothesisHistoryRepository>();

builder.Services.AddLogging();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", build =>
    {
        build.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
        build.WithOrigins("http://localhost:3001")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

//builder.Services.AddControllersWithViews();
//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//    });

builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

    // Show enums as strings instead of numbers
    options.DescribeAllParametersInCamelCase();
});

var app = builder.Build();

// Enable Swagger UI in all environments
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    c.RoutePrefix = string.Empty; // Make Swagger the default route
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowOrigin");
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
app.Run();
