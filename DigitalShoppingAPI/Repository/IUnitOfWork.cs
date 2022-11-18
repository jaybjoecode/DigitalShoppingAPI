using DigitalShoppingAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.Repository
{
    public interface IUnitOfWork
    {
        IGenericRepository<Product> ProductRepository { get; set; }
        IGenericRepository<ProfileInfo> ProfileInfoRepository { get; set; }
        IGenericRepository<ShoppingCar> ShoppingCarRepository { get; set; }
        IGenericRepository<Valoration> ValorationRepository { get; set; }
        Task<int> CommitAsync();
    }
}
