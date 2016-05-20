using System;
using System.Collections.Generic;
using System.Linq;
using Template.Data;
using Template.Service.Interface;

namespace Template.Service.Implementation
{
    public class ItemRepository:IItemRepository
    {
        private DataContext _datacontext = null;
        private readonly IRepository<Item> _ItemRepository;

        public ItemRepository()
        {
            _datacontext = new DataContext();
            _ItemRepository = new RepositoryService<Item>(_datacontext);
            
        }

        public Item GetById(string id)
        {
           return _ItemRepository.GetById(id);
        }

        public List<Item> GetAll()
        {
            return _ItemRepository.GetAll().ToList();
        }

        public void Insert(Item model)
        {
            _ItemRepository.Insert(model);
            _datacontext.SaveChanges();
        }

        public void Update(Item model)
        {
            _ItemRepository.Update(model);
        }

        public void Delete(Item model)
        {
            _ItemRepository.Delete(model);
        }

        public IEnumerable<Item> Find(Func<Item, bool> predicate)
        {
           return _ItemRepository.Find(predicate).ToList();
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
}
