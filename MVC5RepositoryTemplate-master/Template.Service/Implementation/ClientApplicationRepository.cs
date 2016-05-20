using System;
using System.Collections.Generic;
using System.Linq;
using Template.Data;
using Template.Service.Interface;

namespace Template.Service.Implementation
{
    public class ClientApplicationRepository : IClientApplicationRepository
    {
        private DataContext _datacontext = null;
        private readonly IRepository<ClientApplication> _ClientApplicationRepository;

        public ClientApplicationRepository()
        {
            _datacontext = new DataContext();
            _ClientApplicationRepository = new RepositoryService<ClientApplication>(_datacontext);            
        }

        public ClientApplication GetById(int id)
        {
           return _ClientApplicationRepository.GetById(id);
        }


        public List<ClientApplication> GetAll()
        {
            return _ClientApplicationRepository.GetAll().ToList();
        }

        public void Insert(ClientApplication model)
        {
            _ClientApplicationRepository.Insert(model);
            _datacontext.SaveChanges();
        }

        public void Update(ClientApplication model)
        {
            _ClientApplicationRepository.Update(model);
            _datacontext.SaveChanges();
        }

        public void Archive(ClientApplication model)
        {
            _ClientApplicationRepository.Delete(model);
            _datacontext.SaveChanges();
        }

        public IEnumerable<ClientApplication> Find(Func<ClientApplication, bool> predicate)
        {
           return _ClientApplicationRepository.Find(predicate).ToList();
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
}
