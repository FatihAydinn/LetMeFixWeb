using LetMeFix.Persistence;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistanceServices();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDBSettings"));

builder.Services.AddSingleton<IMongoDatabase>(sp => {
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    var client = new MongoClient(settings.ConnectionString);
    return client.GetDatabase(settings.DatabaseName);
});

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
