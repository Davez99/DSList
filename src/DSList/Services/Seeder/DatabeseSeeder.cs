using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSList.Services.Seeder
{
    public class DatabeseSeeder
    {
        private readonly string _connectionString;

        public DatabaseSeeder(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void SeedDatabase(string scriptFilePath)
        {
            if (!File.Exists(scriptFilePath))
                throw new FileNotFoundException($"Script file not found: {scriptFilePath}");

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
        }
    }
}