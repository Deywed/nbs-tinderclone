using Backend.Services;
using Backend.Services.Interfaces;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// --- SERVISI ---

// 1. Dodaj kontrolere
builder.Services.AddControllers();

// 2. Swagger/OpenAPI - KORISTI SAMO OVO ZA SWAGGER UI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

// 3. MongoDB
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDb");
builder.Services.AddSingleton<IMongoClient>(new MongoClient(mongoConnectionString));
builder.Services.AddScoped<IUserRepository, MongoUserRepository>();


// 4. Repozitorijumi

var app = builder.Build();

// --- MIDDLEWARE ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Ovo pravi onaj plavi UI
}

app.UseAuthorization();
app.MapControllers();

app.Run();