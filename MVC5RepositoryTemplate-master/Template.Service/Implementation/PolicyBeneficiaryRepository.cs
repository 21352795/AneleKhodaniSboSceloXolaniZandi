using System;
using System.Collections.Generic;
using System.Linq;
using Template.Data;
using Template.Service.Interface;

namespace Template.Service.Implementation
{
    public class PolicyBeneficiaryRepository : IPolicyBeneficiaryRepository
    {
        private DataContext _datacontext = null;
        private readonly IRepository<PolicyBeneficiary> _PolicyBeneficiaryRepository;

        public PolicyBeneficiaryRepository()
        {
            _datacontext = new DataContext();
            _PolicyBeneficiaryRepository = new RepositoryService<PolicyBeneficiary>(_datacontext);            
        }

        public PolicyBeneficiary GetById(int id)
        {
           return _PolicyBeneficiaryRepository.GetById(id);
        }


        public List<PolicyBeneficiary> GetAll()
        {
            return _PolicyBeneficiaryRepository.GetAll().ToList();
        }

        public void Insert(PolicyBeneficiary model)
        {
            _PolicyBeneficiaryRepository.Insert(model);
            _datacontext.SaveChanges();
        }

        public void Update(PolicyBeneficiary model)
        {
            _PolicyBeneficiaryRepository.Update(model);
            _datacontext.SaveChanges();
        }

        public void Archive(PolicyBeneficiary model)
        {
            _PolicyBeneficiaryRepository.Delete(model);
            _datacontext.SaveChanges();
        }

        public List<PolicyBeneficiary> Find(Func<PolicyBeneficiary, bool> predicate)
        {
           return _PolicyBeneficiaryRepository.Find(predicate).ToList();
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
}
