using System;
using System.Collections.Generic;
using Template.Data;

namespace Template.Service.Interface
{
    public interface IPolicyBeneficiaryRepository : IDisposable
    {
        PolicyBeneficiary GetById(int id);
        List<PolicyBeneficiary> GetAll();
        void Insert(PolicyBeneficiary model);
        void Update(PolicyBeneficiary model);
        void Archive(PolicyBeneficiary model);
        List<PolicyBeneficiary> Find(Func<PolicyBeneficiary, bool> predicate);
    }
}
