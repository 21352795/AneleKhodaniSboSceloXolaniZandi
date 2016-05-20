using System;
using System.Collections.Generic;
using System.Linq;
using Template.Data;
using Template.Service.Interface;

namespace Template.Service.Implementation
{
    public class ProfileApplicationDocumentsRepository : IProfileApplicationDocumentsRepository
    {
        private DataContext _datacontext = null;
        private readonly IRepository<ProfileApplicationDocuments> _ProfileApplicationDocumentsRepository;

        public ProfileApplicationDocumentsRepository()
        {
            _datacontext = new DataContext();
            _ProfileApplicationDocumentsRepository = new RepositoryService<ProfileApplicationDocuments>(_datacontext);            
        }

        public ProfileApplicationDocuments GetById(int id)
        {
           return _ProfileApplicationDocumentsRepository.GetById(id);
        }


        public List<ProfileApplicationDocuments> GetAll()
        {
            return _ProfileApplicationDocumentsRepository.GetAll().ToList();
        }

        public void Insert(ProfileApplicationDocuments model)
        {
            _ProfileApplicationDocumentsRepository.Insert(model);
            _datacontext.SaveChanges();
        }

        public void Update(ProfileApplicationDocuments model)
        {
            _ProfileApplicationDocumentsRepository.Update(model);
            _datacontext.SaveChanges();
        }

        public void Archive(ProfileApplicationDocuments model)
        {
            _ProfileApplicationDocumentsRepository.Delete(model);
            _datacontext.SaveChanges();
        }

        public List<ProfileApplicationDocuments> Find(Func<ProfileApplicationDocuments, bool> predicate)
        {
           return _ProfileApplicationDocumentsRepository.Find(predicate).ToList();
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
}
