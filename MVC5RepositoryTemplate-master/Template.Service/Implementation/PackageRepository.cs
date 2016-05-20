using System.Collections.Generic;
using System.Linq;
using Template.Data;
using Template.Service.Interface;

namespace Template.Service.Implementation
{
    public class PackageRepository : IPackageRepository
    {
        private DataContext _datacontext;
        private readonly IRepository<Package> _loRepository;

        public PackageRepository()
        {
            _datacontext = new DataContext();
            _loRepository = new RepositoryService<Package>(_datacontext);

        }
        public void Insert(Package model)
        {
            _loRepository.Insert(model);
        }

        public void Update(Package model)
        {
            _loRepository.Update(model);
        }

        public void Delete(Package model)
        {
            _loRepository.Delete(model);
        }

        public List<Package> GetAll()
        {
            return _loRepository.GetAll().ToList();
        }
        public Package GetById(int id)
        {
            return _loRepository.GetById(id);
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
    public class PackageBenefitRepository : IPackageBenefitRepository
    {
        private DataContext _datacontext;
        private readonly IRepository<PackageBenefit> _loRepository;

        public PackageBenefitRepository()
        {
            _datacontext = new DataContext();
            _loRepository = new RepositoryService<PackageBenefit>(_datacontext);

        }
        public void Insert(PackageBenefit model)
        {
            _loRepository.Insert(model);
        }

        public void Update(PackageBenefit model)
        {
            _loRepository.Update(model);
        }

        public void Delete(PackageBenefit model)
        {
            _loRepository.Delete(model);
        }

        public List<PackageBenefit> GetAll()
        {
            return _loRepository.GetAll().ToList();
        }
        public PackageBenefit GetById(int id)
        {
            return _loRepository.GetById(id);
        }

        public void Dispose()
        {
            _datacontext.Dispose();
            _datacontext = null;
        }
    }
    public class BenefitRepository : IBenefitRepository
    {
        private DataContext _datacontext;
        private readonly IRepository<Benefit> _loRepository;

        public BenefitRepository()
        {
            _datacontext = new DataContext();
            _loRepository = new RepositoryService<Benefit>(_datacontext);

        }
        public void Insert(Benefit model)
        {
            _loRepository.Insert(model);
        }

        public void Update(Benefit model)
        {
            _loRepository.Update(model);
        }

        public void Delete(Benefit model)
        {
            _loRepository.Delete(model);
        }

        public List<Benefit> GetAll()
        {
            return _loRepository.GetAll().ToList();
        }
        public Benefit GetById(int id)
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

