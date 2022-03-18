using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        private Hashtable _repositories;
        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            // Check if we have anything inside our repositories
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            // Get the entity type
            var type = typeof(TEntity).Name;

            // Check if we already have a repo of this type in our hash table
            if(!_repositories.ContainsKey(type))
            {
                // If we don't have one, we create the repository
                var repositoryType = typeof(GenericRepository<>);
                var repoInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

                // Add the repository to the hash table
                _repositories.Add(type, repoInstance);
            }

            return (IGenericRepository<TEntity>) _repositories[type];
        }
    }
}