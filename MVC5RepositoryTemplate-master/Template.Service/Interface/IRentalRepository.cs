using System;
using System.Collections.Generic;
using Template.Data;

namespace Template.Service.Interface
{
    public interface IRentalRepository: IDisposable
    {
        Rental GetById(string id);
        List<Rental> GetAll();
        void Insert(Rental model);
        void Update(Rental model);
        void Delete(Rental model);
        IEnumerable<Rental> Find(Func<Rental, bool> predicate);   

    }
}
