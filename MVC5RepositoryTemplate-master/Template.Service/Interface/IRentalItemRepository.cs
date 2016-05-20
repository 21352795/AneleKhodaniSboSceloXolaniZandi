using System;
using System.Collections.Generic;
using Template.Data;

namespace Template.Service.Interface
{
  public   interface IRentalItemRepository :IDisposable
    {

        RentalItem GetById(int id);
        List<RentalItem> GetAll();
        void Insert(RentalItem model);
        void Update(RentalItem model);
        void Delete(RentalItem model);
        IEnumerable<RentalItem> Find(Func<RentalItem, bool> predicate);
    }
}
