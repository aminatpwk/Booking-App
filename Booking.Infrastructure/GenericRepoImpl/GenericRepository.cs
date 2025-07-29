using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.GenericRepoImpl
{
    public class GenericRepository<T>(BookingContext context) : IGenericRepository<T> where T : class
    {
        public async Task<T> GetById(Guid id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> Add(T entity)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public void Delete(T entity)
        {
            context.Remove(entity);
        }

        public void Update(T entity)
        {
            context.Update(entity);
        }
    }
}
