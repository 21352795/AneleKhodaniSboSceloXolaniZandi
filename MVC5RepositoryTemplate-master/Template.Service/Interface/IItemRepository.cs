using System;
using System.Collections.Generic;
using Template.Data;

namespace Template.Service.Interface
{
    public interface IItemRepository: IDisposable
    {
        Item GetById(string id);
        List<Item> GetAll();
        void Insert(Item model);
        void Update(Item model);
        void Delete(Item model);
        IEnumerable<Item> Find(Func<Item, bool> predicate);   

    }
}
