using System;
using System.Collections.Generic;
using System.Linq;
using Template.Data;
using Template.Service.Interface;

namespace Template.Service.Implementation
{
    public class ProfileApplicationBeneficiaryRepository : IProfileApplicationBeneficiaryRepository
    {
        private DataContext _datacontext = null;
        private readonly IRepository<ProfileApplicationBeneficiary> _ProfileApplicationBeneficiaryRepository;

        public ProfileApplicationBeneficiaryRepository()
        {
            _datacontext = new DataContext();
            _ProfileApplicationBeneficiaryRepository = new RepositoryService<ProfileApplicationBeneficiary>(_datacontext);
            
        }

        public ProfileApplicationBeneficiary GetById(int id)
        {
           return _ProfileApplicationBeneficiaryRepository.GetById(id);
        }


        public List<ProfileApplicationBeneficiary> GetAll()
        {
            return _ProfileApplicationBeneficiaryRepository.GetAll().ToList();
        }

        public void Insert(ProfileApplicationBeneficiary model)
        {
            _ProfileApplicationBeneficiaryRepository.Insert(model);
            _datacontext.SaveChanges();
        }

        public void Update(ProfileApplicationBeneficiary model)
        {
            _ProfileApplicationBeneficiaryRepository.Update(model);
            _datacontext.SaveChanges();
        }

        public void Delete(ProfileApplicationBeneficiary model)
        {
            _ProfileApplicationBeneficiaryRepository.Delete(model);
            _datacontext.SaveChanges();
        }

        public IEnumerable<ProfileApplicationBeneficiary> Find(Func<ProfileApplicationBeneficiary, bool> predicate)
        {
           return _ProfileApplicationBeneficiaryRepository.Find(predicate).ToList();
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
}
