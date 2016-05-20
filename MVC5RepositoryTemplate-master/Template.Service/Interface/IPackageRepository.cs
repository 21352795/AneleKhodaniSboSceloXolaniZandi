using System;
using System.Collections.Generic;
using Template.Data;

namespace Template.Service.Interface
{
    public interface IPackageRepository : IDisposable
    {
        void Insert(Package model);
        void Update(Package model);
        void Delete(Package model);
        List<Package> GetAll();
        Package GetById(int id);
    }

    public interface IPackageBenefitRepository : IDisposable
    {
        void Insert(PackageBenefit model);
        void Update(PackageBenefit model);
        void Delete(PackageBenefit model);
        List<PackageBenefit> GetAll();
        PackageBenefit GetById(int id);
    }

    public interface IBenefitRepository : IDisposable
    {
        void Insert(Benefit model);
        void Update(Benefit model);
        void Delete(Benefit model);
        List<Benefit> GetAll();
        Benefit GetById(int id);
    }
}
