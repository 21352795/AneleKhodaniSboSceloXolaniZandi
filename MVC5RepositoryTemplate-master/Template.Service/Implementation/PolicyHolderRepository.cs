using System;
using System.Collections.Generic;
using System.Linq;
using Template.Data;
using Template.Service.Interface;

namespace Template.Service.Implementation
{
    public class PolicyHolderRepository : IPolicyHolderRepository
    {
        private DataContext _datacontext = null;
        private readonly IRepository<PolicyHolder> _PolicyHolderRepository;

        public PolicyHolderRepository()
        {
            _datacontext = new DataContext();
            _PolicyHolderRepository = new RepositoryService<PolicyHolder>(_datacontext);            
        }

        public PolicyHolder GetById(int id)
        {
           return _PolicyHolderRepository.GetById(id);
        }


        public List<PolicyHolder> GetAll()
        {
            return _PolicyHolderRepository.GetAll().ToList();
        }

        public void Insert(PolicyHolder model)
        {
            _PolicyHolderRepository.Insert(model);
            _datacontext.SaveChanges();
        }

        public void Update(PolicyHolder model)
        {
            _PolicyHolderRepository.Update(model);
            _datacontext.SaveChanges();
        }

        public void Archive(PolicyHolder model)
        {
            _PolicyHolderRepository.Delete(model);
            _datacontext.SaveChanges();
        }

        public List<PolicyHolder> Find(Func<PolicyHolder, bool> predicate)
        {
           return _PolicyHolderRepository.Find(predicate).ToList();
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
}
