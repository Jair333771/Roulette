using System.Collections.Generic;

namespace RouletteApp.Core.Interfaces
{
    public interface IBetRepository<T> where T : class
    {
        List<T> GetByRouleteId(int rouletteId);
    }
}
