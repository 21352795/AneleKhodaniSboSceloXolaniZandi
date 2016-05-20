using System;
using System.Collections.Generic;
using System.Linq;
using Template.Data;
using Template.Service.Interface;

namespace Template.Service.Implementation
{
   public class RentalItemRepository: IRentalItemRepository
    {

        private DataContext _datacontext = null;
        private readonly IRepository<RentalItem> _RentalItemRepository;

        public RentalItemRepository()
        {
            _datacontext = new DataContext();
            _RentalItemRepository = new RepositoryService<RentalItem>(_datacontext);
            
        }

        public RentalItem GetById(int id)
        {
           return _RentalItemRepository.GetById(id);
        }

        public List<RentalItem> GetAll()
        {
            return _RentalItemRepository.GetAll().ToList();
        }

        public void Insert(RentalItem model)
        {
            _RentalItemRepository.Insert(model);
        }

        public void Update(RentalItem model)
        {
            _RentalItemRepository.Update(model);
        }

        public void Delete(RentalItem model)
        {
            _RentalItemRepository.Delete(model);
        }

        public IEnumerable<RentalItem> Find(Func<RentalItem, bool> predicate)
        {
           return _RentalItemRepository.Find(predicate).ToList();
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
}
