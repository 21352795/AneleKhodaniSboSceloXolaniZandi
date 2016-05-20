using System;
using System.Collections.Generic;
using Template.Data;

namespace Template.Service.Interface
{
    public interface IProfileApplicationBeneficiaryRepository : IDisposable
    {
        ProfileApplicationBeneficiary GetById(Int32 id);
        //ProfileApplication GetByProfileID(string idNumber);
        List<ProfileApplicationBeneficiary> GetAll();
        void Insert(ProfileApplicationBeneficiary model);
        void Update(ProfileApplicationBeneficiary model);
        void Delete(ProfileApplicationBeneficiary model);
        IEnumerable<ProfileApplicationBeneficiary> Find(Func<ProfileApplicationBeneficiary, bool> predicate);   

    }
}
