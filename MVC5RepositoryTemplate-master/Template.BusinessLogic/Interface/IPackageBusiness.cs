using System.Collections.Generic;
using Template.Model.ViewModels;

namespace Template.BusinessLogic.Interface
{
    public interface IPackageBusiness
    {
        List<PackageView> GetAll();
        void AddPackage(PackageView model);
        PackageView GetById(int id);
        void UpdatePackage(PackageView model);
        PackageView Details(int id);
    }
}
