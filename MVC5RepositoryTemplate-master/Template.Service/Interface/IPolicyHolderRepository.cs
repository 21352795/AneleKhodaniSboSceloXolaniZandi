using System;
using System.Collections.Generic;
using Template.Data;

namespace Template.Service.Interface
{
    public interface IPolicyHolderRepository : IDisposable
    {
        PolicyHolder GetById(int id);
        List<PolicyHolder> GetAll();
        void Insert(PolicyHolder model);
        void Update(PolicyHolder model);
        void Archive(PolicyHolder model);
        List<PolicyHolder> Find(Func<PolicyHolder, bool> predicate);
    }
}
