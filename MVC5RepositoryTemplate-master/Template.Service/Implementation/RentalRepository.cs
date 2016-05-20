using System;
using System.Collections.Generic;
using System.Linq;
using Template.Data;
using Template.Service.Interface;

namespace Template.Service.Implementation
{
    public class RentalRepository:IRentalRepository
    {
        private DataContext _datacontext = null;
        private readonly IRepository<Rental> _RentalRepository;

        public RentalRepository()
        {
            _datacontext = new DataContext();
            _RentalRepository = new RepositoryService<Rental>(_datacontext);
            
        }

        public Rental GetById(string id)
        {
           return _RentalRepository.GetById(id);
        }

        public List<Rental> GetAll()
        {
            return _RentalRepository.GetAll().ToList();
        }

        public void Insert(Rental model)
        {
            _RentalRepository.Insert(model);
            _datacontext.SaveChanges();
        }

        public void Update(Rental model)
        {
            _RentalRepository.Update(model);
        }

        public void Delete(Rental model)
        {
            _RentalRepository.Delete(model);
        }

        public IEnumerable<Rental> Find(Func<Rental, bool> predicate)
        {
           return _RentalRepository.Find(predicate).ToList();
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
}
