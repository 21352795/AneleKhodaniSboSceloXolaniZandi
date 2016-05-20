using System;
using System.Collections.Generic;
using Template.Data;

namespace Template.Service.Interface
{
    public interface IPremiumPaymentRepository : IDisposable
    {
        PremiumPayment GetById(int id);
        List<PremiumPayment> GetAll();
        void Insert(PremiumPayment model);
        void Update(PremiumPayment model);
        void Archive(PremiumPayment model);
        List<PremiumPayment> Find(Func<PremiumPayment, bool> predicate);
    }
}
