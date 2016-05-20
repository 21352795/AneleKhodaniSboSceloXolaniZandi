using System;
using System.Collections.Generic;
using System.Linq;
using Template.Data;
using Template.Service.Interface;

namespace Template.Service.Implementation
{
    public class ProfileActivityLogRepository : IProfileActivityLogRepository
    {
        private DataContext _datacontext = null;
        private readonly IRepository<ProfileActivityLog> _ProfileActivityLogRepository;

        public ProfileActivityLogRepository()
        {
            _datacontext = new DataContext();
            _ProfileActivityLogRepository = new RepositoryService<ProfileActivityLog>(_datacontext);
            
        }

        public ProfileActivityLog GetById(string id)
        {
           return _ProfileActivityLogRepository.GetById(id);
        }

        public List<ProfileActivityLog> GetAll()
        {
            return _ProfileActivityLogRepository.GetAll().ToList();
        }

        public void Insert(ProfileActivityLog model)
        {
            _ProfileActivityLogRepository.Insert(model);
            _datacontext.SaveChanges();
        }

        public void Update(ProfileActivityLog model)
        {
            _ProfileActivityLogRepository.Update(model);
        }

        public void Delete(ProfileActivityLog model)
        {
            _ProfileActivityLogRepository.Delete(model);
        }

        public IEnumerable<ProfileActivityLog> Find(Func<ProfileActivityLog, bool> predicate)
        {
           return _ProfileActivityLogRepository.Find(predicate).ToList();
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
}
