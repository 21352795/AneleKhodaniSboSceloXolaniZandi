using System;
using System.Collections.Generic;
using Template.Data;

namespace Template.Service.Interface
{
    public interface ICategoryRepository: IDisposable
    {
        Category GetById(Int32 id);
        List<Category> GetAll();
        void Insert(Category model);
        void Update(Category model);
        void Delete(Category model);
        IEnumerable<Category> Find(Func<Category, bool> predicate);   

    }
}
