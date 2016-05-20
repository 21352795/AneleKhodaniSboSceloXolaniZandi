using System;
using System.Collections.Generic;
using System.Linq;
using Template.Data;
using Template.Service.Interface;

namespace Template.Service.Implementation
{
    public class ClientApplicationBeneficiaryRepository : IClientApplicationBeneficiaryRepository
    {
        private DataContext _datacontext = null;
        private readonly IRepository<ClientApplicationBeneficiary> _ClientApplicationBeneficiaryRepository;

        public ClientApplicationBeneficiaryRepository()
        {
            _datacontext = new DataContext();
            _ClientApplicationBeneficiaryRepository = new RepositoryService<ClientApplicationBeneficiary>(_datacontext);
            
        }

        public ClientApplicationBeneficiary GetById(int id)
        {
           return _ClientApplicationBeneficiaryRepository.GetById(id);
        }


        public List<ClientApplicationBeneficiary> GetAll()
        {
            return _ClientApplicationBeneficiaryRepository.GetAll().ToList();
        }

        public void Insert(ClientApplicationBeneficiary model)
        {
            _ClientApplicationBeneficiaryRepository.Insert(model);
            _datacontext.SaveChanges();
        }

        public void Update(ClientApplicationBeneficiary model)
        {
            _ClientApplicationBeneficiaryRepository.Update(model);
            _datacontext.SaveChanges();
        }

        public void Archive(ClientApplicationBeneficiary model)
        {
            _ClientApplicationBeneficiaryRepository.Delete(model);
            _datacontext.SaveChanges();
        }

        public IEnumerable<ClientApplicationBeneficiary> Find(Func<ClientApplicationBeneficiary, bool> predicate)
        {
           return _ClientApplicationBeneficiaryRepository.Find(predicate).ToList();
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
}
