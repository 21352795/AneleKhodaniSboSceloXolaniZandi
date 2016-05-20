using System;
using System.Collections.Generic;
using System.Linq;
using Template.Data;
using Template.Service.Interface;

namespace Template.Service.Implementation
{
    public class PremiumPaymentRepository : IPremiumPaymentRepository
    {
        private DataContext _datacontext = null;
        private readonly IRepository<PremiumPayment> _PremiumPaymentRepository;

        public PremiumPaymentRepository()
        {
            _datacontext = new DataContext();
            _PremiumPaymentRepository = new RepositoryService<PremiumPayment>(_datacontext);            
        }

        public PremiumPayment GetById(int id)
        {
           return _PremiumPaymentRepository.GetById(id);
        }


        public List<PremiumPayment> GetAll()
        {
            return _PremiumPaymentRepository.GetAll().ToList();
        }

        public void Insert(PremiumPayment model)
        {
            _PremiumPaymentRepository.Insert(model);
            _datacontext.SaveChanges();
        }

        public void Update(PremiumPayment model)
        {
            _PremiumPaymentRepository.Update(model);
            _datacontext.SaveChanges();
        }

        public void Archive(PremiumPayment model)
        {
            _PremiumPaymentRepository.Delete(model);
            _datacontext.SaveChanges();
        }

        public List<PremiumPayment> Find(Func<PremiumPayment, bool> predicate)
        {
           return _PremiumPaymentRepository.Find(predicate).ToList();
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
}
