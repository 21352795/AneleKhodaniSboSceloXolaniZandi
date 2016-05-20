using System;
using System.Collections.Generic;
using System.Linq;
using Template.Data;
using Template.Service.Interface;

namespace Template.Service.Implementation
{
    public class PaymentRepository : IPaymentRepository
    {
        private DataContext _datacontext = null;
        private readonly IRepository<Payment> _PaymentRepository;

        public PaymentRepository()
        {
            _datacontext = new DataContext();
            _PaymentRepository = new RepositoryService<Payment>(_datacontext);            
        }

        public Payment GetById(int id)
        {
           return _PaymentRepository.GetById(id);
        }


        public List<Payment> GetAll()
        {
            return _PaymentRepository.GetAll().ToList();
        }

        public void Insert(Payment model)
        {
            _PaymentRepository.Insert(model);
            _datacontext.SaveChanges();
        }

        public void Update(Payment model)
        {
            _PaymentRepository.Update(model);
            _datacontext.SaveChanges();
        }

        public void Archive(Payment model)
        {
            _PaymentRepository.Delete(model);
            _datacontext.SaveChanges();
        }

        public List<Payment> Find(Func<Payment, bool> predicate)
        {
           return _PaymentRepository.Find(predicate).ToList();
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
}
