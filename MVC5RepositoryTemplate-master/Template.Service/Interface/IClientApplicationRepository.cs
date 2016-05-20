using System;
using System.Collections.Generic;
using Template.Data;

namespace Template.Service.Interface
{
    public interface IClientApplicationRepository: IDisposable
    {
        ClientApplication GetById(int id);
        //ClientApplication GetByClientID(string idNumber);
        List<ClientApplication> GetAll();
        void Insert(ClientApplication model);
        void Update(ClientApplication model);
        void Archive(ClientApplication model);
        IEnumerable<ClientApplication> Find(Func<ClientApplication, bool> predicate);   

    }
}
