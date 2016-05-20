using System;
using System.Collections.Generic;
using System.Linq;
using Template.Data;
using Template.Service.Interface;

namespace Template.Service.Implementation
{
    public class ClientApplicationDocumentRepository : IClientApplicationDocumentRepository
    {
        private DataContext _datacontext = null;
        private readonly IRepository<ClientApplicationDocument> _ClientApplicationDocumentRepository;

        public ClientApplicationDocumentRepository()
        {
            _datacontext = new DataContext();
            _ClientApplicationDocumentRepository = new RepositoryService<ClientApplicationDocument>(_datacontext);
            
        }

        public ClientApplicationDocument GetById(int id)
        {
           return _ClientApplicationDocumentRepository.GetById(id);
        }


        public List<ClientApplicationDocument> GetAll()
        {
            return _ClientApplicationDocumentRepository.GetAll().ToList();
        }

        public void Insert(ClientApplicationDocument model)
        {
            _ClientApplicationDocumentRepository.Insert(model);
            _datacontext.SaveChanges();
        }

        public void Update(ClientApplicationDocument model)
        {
            _ClientApplicationDocumentRepository.Update(model);
            _datacontext.SaveChanges();
        }

        public void Delete(ClientApplicationDocument model)
        {
            _ClientApplicationDocumentRepository.Delete(model);
            _datacontext.SaveChanges();
        }

        public IEnumerable<ClientApplicationDocument> Find(Func<ClientApplicationDocument, bool> predicate)
        {
           return _ClientApplicationDocumentRepository.Find(predicate).ToList();
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
}
