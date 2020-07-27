using Microsoft.EntityFrameworkCore;
using RouletteApp.Core.Interfaces;
using RouletteApp.Data.Context;
using System.Collections.Generic;
using System.Linq;

namespace RouletteApp.Data.Repositories
{
    public class AppRepository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext context;
        protected readonly DbSet<T> table;

        public AppRepository(AppDbContext context)
        {
            this.context = context;
            table = this.context.Set<T>();
        }

        public List<T> GetAll()
        {

            return table.ToList();
        }

        public T GetById(int id)
        {
            return table.Find(id);
        }

        public T Add(T obj)
        {
            table.Add(obj);
            context.SaveChanges();
            return obj;
        }

        public T Update(T obj)
        {
            table.Update(obj);
            context.SaveChanges();
            return obj;
        }

        public int Delete(object id)
        {
            T obj = table.Find(id);
            table.Remove(obj);
            return context.SaveChanges();
        }
    }
}