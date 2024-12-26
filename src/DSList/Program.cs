using DSList.Data;
using DSList.Interfaces;
using DSList.Repository;
using DSList.Services;
using DSList.Services.Seeder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Contexto de banco
builder.Services.AddDbContext<GameContext>(option => 
    option.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// // Configuração do SQLite
// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


//Adição do escopo de Service Scoped

builder.Services.AddScoped<IGameListRepository, GameListRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<ISeedDataBase, DataBaseSeederRepository>();
builder.Services.AddScoped<DatabaseSeeder>();
builder.Services.AddScoped<GameListService>();
builder.Services.AddScoped<GameService>();

//Adição da Leitura das controllers
builder.Services.AddControllers();



var app = builder.Build();


// Executa as migrações e o seeder durante a inicialização
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<GameContext>();
    dbContext.Database.Migrate(); // Aplica as migrações

    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    seeder.SeedDatabase(); // Executa o seeder
}

//fazer um scopo diferente 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();
app.UseHttpsRedirection();
app.Run();

