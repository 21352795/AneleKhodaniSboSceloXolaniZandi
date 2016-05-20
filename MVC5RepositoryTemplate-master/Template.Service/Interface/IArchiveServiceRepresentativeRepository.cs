using System;
using System.Collections.Generic;
using Template.Data;

namespace Template.Service.Interface
{
    public interface IArchiveServiceRepresentativeRepository : IDisposable
    {
        void Insert(ArchiveServiceRepresentative model);
        void Delete(ArchiveServiceRepresentative model);
        List<ArchiveServiceRepresentative> GetAll();
        ArchiveServiceRepresentative GetById(string id);
        
    }
}
