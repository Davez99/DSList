using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSList.Interfaces;
using Microsoft.Data.Sqlite;

namespace DSList.Services.Seeder
{
    public class DatabaseSeeder
    {
        private readonly ISeedDataBase _SeedDatabase;
        private readonly IGameRepository _gameRepository;

        public DatabaseSeeder(
        ISeedDataBase seedDatabase, 
        IGameRepository gameRepository)
        {
            _SeedDatabase = seedDatabase;
            _gameRepository = gameRepository;
        }

        public void SeedDatabase()
        {
            var scriptFilePath = Path.Combine(AppContext.BaseDirectory, "Scripts", "Imports.sql");

            if (!File.Exists(scriptFilePath))
            {
                throw new FileNotFoundException($"Script file not found: {scriptFilePath}");
            }

            var sqlScript = File.ReadAllText(scriptFilePath);

            //Se já tiver realizado o Seeder uma vez, não executa novamente.
            if(!_SeedDatabase.VerificationSeed()){
                _SeedDatabase.ExecuteSeed(sqlScript);
            }

            Console.WriteLine("Database seeded successfully!");
        }

    }
}