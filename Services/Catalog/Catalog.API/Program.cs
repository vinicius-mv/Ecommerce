using Catalog.Application.Queries;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Data.MongoMappings;
using Catalog.Infrastructure.Repositories;
using Catalog.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Register custom serializers for MongoClient
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateOnlySerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => 
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())); // Serialize/deserialize enums as their string names (e.g., "NameAsc") so Swagger shows readable values and query binding accepts those strings instead of numeric values.

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add Swagger Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllBrandsQuery).Assembly));

// Build strongly typed settings
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection(nameof(DatabaseSettings)));

// Configure MongoDb Entities
MongoDbMappingConfiguration.Configure();

// Register MongoClient as a singleton
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var databaseSettings = sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
    var client = new MongoClient(databaseSettings.ConnectionString);
    return client.GetDatabase(databaseSettings.DatabaseName);
});

// Custom Services
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<ITypeRepository, TypeRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Seed MongoDB on startup
using (var scope = app.Services.CreateScope())
{
    var databaseOptions = app.Services.GetRequiredService<IOptions<DatabaseSettings>>();
    await DatabaseSeeder.Seed(databaseOptions);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Enable Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
