using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Divisas.Models;

namespace Divisas.ViewsModels
{
    public class MonedaDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public MonedaDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Monedas>().Wait();
        }

        public Task<List<Monedas>> GetMonedasAsync()
        {
            return _database.Table<Monedas>().ToListAsync();
        }

        public Task<int> SaveMonedaAsync(Monedas moneda)
        {
            return _database.InsertAsync(moneda);
        }
    }
}
