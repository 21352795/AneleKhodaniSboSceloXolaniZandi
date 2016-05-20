using System;
using System.Collections.Generic;
using Template.Data;

namespace Template.Service.Interface
{
    public interface IProfileApplicationDocumentsRepository : IDisposable
    {
        ProfileApplicationDocuments GetById(int id);
        List<ProfileApplicationDocuments> GetAll();
        void Insert(ProfileApplicationDocuments model);
        void Update(ProfileApplicationDocuments model);
        void Archive(ProfileApplicationDocuments model);
        List<ProfileApplicationDocuments> Find(Func<ProfileApplicationDocuments, bool> predicate);
    }
}
