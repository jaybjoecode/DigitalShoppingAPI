using DigitalShoppingAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DigitalShoppingDbContext _context;

        public UnitOfWork(DigitalShoppingDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            ProductRepository ??= new GenericRepository<Product>(_context);
            ProfileInfoRepository ??= new GenericRepository<ProfileInfo>(_context);
            ShoppingCarRepository ??= new GenericRepository<ShoppingCar>(_context);
            ValorationRepository ??= new GenericRepository<Valoration>(_context);
        }

        public IGenericRepository<Product> ProductRepository { get; set; }
        public IGenericRepository<ProfileInfo> ProfileInfoRepository { get; set; }
        public IGenericRepository<ShoppingCar> ShoppingCarRepository { get; set; }
        public IGenericRepository<Valoration> ValorationRepository { get; set; }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
