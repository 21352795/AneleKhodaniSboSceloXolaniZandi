using System;
using System.Collections.Generic;
using Template.Data;

namespace Template.Service.Interface
{
    public interface IServiceRepresentativeRepository : IDisposable
    {
        ServiceRepresentative GetByServRepNo(string id);
        List<ServiceRepresentative> GetAll();
        void Insert(ServiceRepresentative model);
        void Update(ServiceRepresentative model);
        void Delete(ServiceRepresentative model);
        IEnumerable<ServiceRepresentative> Find(Func<ServiceRepresentative, bool> predicate);
    }
}
