using System;
using System.Collections.Generic;
using Template.Data;

namespace Template.Service.Interface
{
    public interface IPolicyDocumentRepository : IDisposable
    {
        PolicyDocument GetById(int id);
        List<PolicyDocument> GetAll();
        void Insert(PolicyDocument model);
        void Update(PolicyDocument model);
        void Archive(PolicyDocument model);
        List<PolicyDocument> Find(Func<PolicyDocument, bool> predicate);
    }
}
