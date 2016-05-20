using System;
using System.Collections.Generic;
using Template.Data;

namespace Template.Service.Interface
{
    public interface IClientApplicationDocumentRepository : IDisposable
    {
        ClientApplicationDocument GetById(Int32 id);
        //ClientApplication GetByClientID(string idNumber);
        List<ClientApplicationDocument> GetAll();
        void Insert(ClientApplicationDocument model);
        void Update(ClientApplicationDocument model);
        void Delete(ClientApplicationDocument model);
        IEnumerable<ClientApplicationDocument> Find(Func<ClientApplicationDocument, bool> predicate);   

    }
}
