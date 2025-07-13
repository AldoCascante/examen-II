using examen_II;
using examen_II.Domain;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:8081", "http://localhost:8080", "http://192.168.0.8:8080")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

DrinkInfoDataBase.drinksTable.Add(new DrinkInfoDTO
{
     name = "Coca Cola",
     available = 10,
     price = 800,
});

DrinkInfoDataBase.drinksTable.Add(new DrinkInfoDTO
{
    name = "Pepsi",
    available = 8,
    price = 750,
});

DrinkInfoDataBase.drinksTable.Add(new DrinkInfoDTO
{
    name = "Fanta",
    available = 10,
    price = 950,
});

DrinkInfoDataBase.drinksTable.Add(new DrinkInfoDTO
{
    name = "Sprite",
    available = 15,
    price = 975,
});

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
