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

        public void SeedDatabase(string scriptFilePath)
        {
            if (!File.Exists(scriptFilePath))
            {
                throw new FileNotFoundException($"Script file not found: {scriptFilePath}");
            }

            // Lê o conteúdo do arquivo SQL
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