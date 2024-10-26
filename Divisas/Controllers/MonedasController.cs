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

        public async Task<Monedas?> GetMonedaByNameAsync(string nombre)
        {
            return await _dbContext.Monedas.FirstOrDefaultAsync(m => m.Nombre == nombre);
        }

        public async Task SaveNewMonedaAsync(Monedas moneda)
        {
            await _dbContext.Monedas.AddAsync(moneda);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditMonedaAsync(Monedas moneda)
        {
            _dbContext.Monedas.Update(moneda);
            await _dbContext.SaveChangesAsync();
        }
    }
}
