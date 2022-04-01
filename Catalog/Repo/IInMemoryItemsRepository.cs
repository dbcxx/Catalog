using Catalog.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Repo
{
    public interface IInMemoryItemsRepository
    {
        
        IEnumerable<Item> GetItems();
        Item GetItem(Guid id);
        void CreateItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(Guid id);
        
    }
}