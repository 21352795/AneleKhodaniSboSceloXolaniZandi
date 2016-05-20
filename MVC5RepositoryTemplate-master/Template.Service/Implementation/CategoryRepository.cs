using System;
using System.Collections.Generic;
using System.Linq;
using Template.Data;
using Template.Service.Interface;

namespace Template.Service.Implementation
{
    public class CategoryRepository:ICategoryRepository
    {
        private DataContext _datacontext = null;
        private readonly IRepository<Category> _categoryRepository;

        public CategoryRepository()
        {
            _datacontext = new DataContext();
            _categoryRepository = new RepositoryService<Category>(_datacontext);
            
        }

        public Category GetById(int id)
        {
           return _categoryRepository.GetById(id);
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll().ToList();
        }

        public void Insert(Category model)
        {
            _categoryRepository.Insert(model);
        }

        public void Update(Category model)
        {
            _categoryRepository.Update(model);
        }

        public void Delete(Category model)
        {
            _categoryRepository.Delete(model);
        }

        public IEnumerable<Category> Find(Func<Category, bool> predicate)
        {
           return _categoryRepository.Find(predicate).ToList();
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
}
