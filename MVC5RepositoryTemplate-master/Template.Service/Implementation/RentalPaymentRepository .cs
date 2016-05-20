using System;
using System.Collections.Generic;
using System.Linq;
using Template.Data;
using Template.Service.Interface;

namespace Template.Service.Implementation
{
    public class RentalPaymentRepository : IRentalPaymentRepository
    {
        private DataContext _datacontext = null;
        private readonly IRepository<RentalPayment> _RentalPaymentRepository;

        public RentalPaymentRepository()
        {
            _datacontext = new DataContext();
            _RentalPaymentRepository = new RepositoryService<RentalPayment>(_datacontext);            
        }

        public RentalPayment GetById(int id)
        {
           return _RentalPaymentRepository.GetById(id);
        }


        public List<RentalPayment> GetAll()
        {
            return _RentalPaymentRepository.GetAll().ToList();
        }

        public void Insert(RentalPayment model)
        {
            _RentalPaymentRepository.Insert(model);
            _datacontext.SaveChanges();
        }

        public void Update(RentalPayment model)
        {
            _RentalPaymentRepository.Update(model);
            _datacontext.SaveChanges();
        }

        public void Archive(RentalPayment model)
        {
            _RentalPaymentRepository.Delete(model);
            _datacontext.SaveChanges();
        }

        public List<RentalPayment> Find(Func<RentalPayment, bool> predicate)
        {
           return _RentalPaymentRepository.Find(predicate).ToList();
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
}
