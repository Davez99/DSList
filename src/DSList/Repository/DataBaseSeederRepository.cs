using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DSList.Data;
using DSList.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DSList.Repository
{
    public class DataBaseSeederRepository: ISeedDataBase
    {
        private readonly GameContext _context;
        private readonly ILogger<DataBaseSeederRepository> _logger;

        public DataBaseSeederRepository(GameContext context, ILogger<DataBaseSeederRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void ExecuteSeed(string query)
        {
            try
            {
                _context.Database.GetDbConnection().Query(query);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao executar o Seeder");
                throw;
            }
        }

        public bool VerificationSeed()
        {
            return _context.Games.ToList().Count() > 0 ? true : false;
        }
    }
}