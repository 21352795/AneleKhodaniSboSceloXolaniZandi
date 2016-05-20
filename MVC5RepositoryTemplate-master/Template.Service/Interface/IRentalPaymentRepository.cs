using System;
using System.Collections.Generic;
using Template.Data;

namespace Template.Service.Interface
{
    public interface IRentalPaymentRepository : IDisposable
    {
        RentalPayment GetById(int id);
        List<RentalPayment> GetAll();
        void Insert(RentalPayment model);
        void Update(RentalPayment model);
        void Archive(RentalPayment model);
        List<RentalPayment> Find(Func<RentalPayment, bool> predicate);
    }
}
