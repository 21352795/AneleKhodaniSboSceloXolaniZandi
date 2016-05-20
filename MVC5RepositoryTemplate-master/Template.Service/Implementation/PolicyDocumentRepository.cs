using System;
using System.Collections.Generic;
using System.Linq;
using Template.Data;
using Template.Service.Interface;

namespace Template.Service.Implementation
{
    public class PolicyDocumentRepository : IPolicyDocumentRepository
    {
        private DataContext _datacontext = null;
        private readonly IRepository<PolicyDocument> _PolicyDocumentRepository;

        public PolicyDocumentRepository()
        {
            _datacontext = new DataContext();
            _PolicyDocumentRepository = new RepositoryService<PolicyDocument>(_datacontext);            
        }

        public PolicyDocument GetById(int id)
        {
           return _PolicyDocumentRepository.GetById(id);
        }


        public List<PolicyDocument> GetAll()
        {
            return _PolicyDocumentRepository.GetAll().ToList();
        }

        public void Insert(PolicyDocument model)
        {
            _PolicyDocumentRepository.Insert(model);
            _datacontext.SaveChanges();
        }

        public void Update(PolicyDocument model)
        {
            _PolicyDocumentRepository.Update(model);
            _datacontext.SaveChanges();
        }

        public void Archive(PolicyDocument model)
        {
            _PolicyDocumentRepository.Delete(model);
            _datacontext.SaveChanges();
        }

        public List<PolicyDocument> Find(Func<PolicyDocument, bool> predicate)
        {
           return _PolicyDocumentRepository.Find(predicate).ToList();
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
}
