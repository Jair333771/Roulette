using RouletteApp.Core.Interfaces;
using RouletteApp.Data.Context;
using RouletteApp.Data.Emtities;
using System.Collections.Generic;
using System.Linq;

namespace RouletteApp.Data.Repositories
{
    public class BetRepository : AppRepository<Bet>, IRepository<Bet>
    {
        protected readonly AppDbContext db;

        public BetRepository(AppDbContext db) : base(db)
        {
            this.db = db;
        }

        public List<Bet> GetByRouleteId(int id)
        {
            return db.Bet
                .Where(x => x.IdRoulette == id)
                .ToList();
        }
    }
}
