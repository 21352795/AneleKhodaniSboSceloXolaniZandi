using System;
using System.Collections.Generic;
using System.Linq;
using Template.Data;
using Template.Service.Interface;

namespace Template.Service.Implementation
{
    public class ServiceRepresentativeRepository : IServiceRepresentativeRepository
    {
        private DataContext _datacontext = null;
        private readonly IRepository<ServiceRepresentative> _ServiceRepresentativeRepository;

        public ServiceRepresentativeRepository()
        {
            _datacontext = new DataContext();
            _ServiceRepresentativeRepository = new RepositoryService<ServiceRepresentative>(_datacontext);            
        }

        public ServiceRepresentative GetByServRepNo(string id)
        {
           return _ServiceRepresentativeRepository.GetById(id);
        }

        public List<ServiceRepresentative> GetAll()
        {
            return _ServiceRepresentativeRepository.GetAll().ToList();
        }

        public void Insert(ServiceRepresentative model)
        {
            _ServiceRepresentativeRepository.Insert(model);
        }

        public void Update(ServiceRepresentative model)
        {
            _ServiceRepresentativeRepository.Update(model);
        }

        public void Delete(ServiceRepresentative model)
        {
            _ServiceRepresentativeRepository.Delete(model);
        }

        public IEnumerable<ServiceRepresentative> Find(Func<ServiceRepresentative, bool> predicate)
        {
           return _ServiceRepresentativeRepository.Find(predicate).ToList();
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }    
}
