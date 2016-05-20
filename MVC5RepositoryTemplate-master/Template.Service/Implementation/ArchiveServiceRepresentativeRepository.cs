using System.Collections.Generic;
using System.Linq;
using Template.Data;
using Template.Service.Interface;

namespace Template.Service.Implementation
{
    public class ArchiveServiceRepresentativeRepository : IArchiveServiceRepresentativeRepository
    {
        private DataContext _datacontext;
        private readonly IRepository<ArchiveServiceRepresentative> _loRepository;

        public ArchiveServiceRepresentativeRepository()
        {
            _datacontext = new DataContext();
            _loRepository = new RepositoryService<ArchiveServiceRepresentative>(_datacontext);

        }
        public void Insert(ArchiveServiceRepresentative model)
        {
            _loRepository.Insert(model);
        }

        public void Delete(ArchiveServiceRepresentative model)
        {
            _loRepository.Delete(model);
        }

        public List<ArchiveServiceRepresentative> GetAll()
        {
            return _loRepository.GetAll().ToList();
        }
        public ArchiveServiceRepresentative GetById(string id)
        {
            return _loRepository.GetById(id);
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }

}

