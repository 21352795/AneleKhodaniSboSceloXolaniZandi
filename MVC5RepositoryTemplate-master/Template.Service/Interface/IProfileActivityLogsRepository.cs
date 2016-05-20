using System;
using System.Collections.Generic;
using Template.Data;

namespace Template.Service.Interface
{
    public interface IProfileActivityLogRepository : IDisposable
    {
        ProfileActivityLog GetById(string id);
        List<ProfileActivityLog> GetAll();
        void Insert(ProfileActivityLog model);
        void Update(ProfileActivityLog model);
        void Delete(ProfileActivityLog model);
        IEnumerable<ProfileActivityLog> Find(Func<ProfileActivityLog, bool> predicate);   

    }
}
