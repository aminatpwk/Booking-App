using Booking.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        public async Task Delete(T entity)
        {
            context.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            var entry = context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                var existing = await context.Set<T>().FindAsync();
                if (existing != null)
                {
                    context.Entry(existing).CurrentValues.SetValues(entity);
                }
                else
                {
                    context.Update(entity);
                }
            }

            await context.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await context.SaveChangesAsync(cancellationToken);
        }
    }
}
