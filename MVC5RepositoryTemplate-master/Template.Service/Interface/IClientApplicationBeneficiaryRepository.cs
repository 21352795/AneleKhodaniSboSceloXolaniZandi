using System;
using System.Collections.Generic;
using Template.Data;

namespace Template.Service.Interface
{
    public interface IClientApplicationBeneficiaryRepository : IDisposable
    {
        ClientApplicationBeneficiary GetById(Int32 id);
        //ClientApplication GetByClientID(string idNumber);
        List<ClientApplicationBeneficiary> GetAll();
        void Insert(ClientApplicationBeneficiary model);
        void Update(ClientApplicationBeneficiary model);
        void Archive(ClientApplicationBeneficiary model);
        IEnumerable<ClientApplicationBeneficiary> Find(Func<ClientApplicationBeneficiary, bool> predicate);   

    }
}
