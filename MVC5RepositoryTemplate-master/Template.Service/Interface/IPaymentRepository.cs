using System;
using System.Collections.Generic;
using Template.Data;

namespace Template.Service.Interface
{
    public interface IPaymentRepository : IDisposable
    {
        Payment GetById(int id);
        List<Payment> GetAll();
        void Insert(Payment model);
        void Update(Payment model);
        void Archive(Payment model);
        List<Payment> Find(Func<Payment, bool> predicate);
    }
}
