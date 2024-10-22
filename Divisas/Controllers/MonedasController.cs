using Divisas.Database;
using Divisas.Models;
using Microsoft.EntityFrameworkCore;

namespace Divisas.Controllers
{
    public class MonedasController
    {
        private readonly DivisasDbContext _dbContext;

        public MonedasController()
        {
            _dbContext = new DivisasDbContext();
        }

        public async Task<List<Monedas>> GetMonedasAsync()
        {
            return await _dbContext.Monedas.ToListAsync();
        }
    }
}
