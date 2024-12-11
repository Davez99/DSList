using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace DSList.Services.Seeder
{
    public class DatabaseSeeder
    {
        private readonly string _connectionString;

        public DatabaseSeeder(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void SeedDatabase()
        {
            var scriptFilePath = Path.Combine(AppContext.BaseDirectory, "Scripts", "Imports.sql");

            if (!File.Exists(scriptFilePath))
            {
                throw new FileNotFoundException($"Script file not found: {scriptFilePath}");
            }

            var sqlScript = File.ReadAllText(scriptFilePath);

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sqlScript;
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Database seeded successfully!");
        }

    }
}