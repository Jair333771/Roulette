using System.Collections.Generic;

namespace RouletteApp.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public List<T> GetAll();

        public T GetById(int id);

        public T Add(T obj);

        public T Update(T obj);
    }
}
