using System.Collections.Generic;
using Template.Model.ViewModels;

namespace Template.BusinessLogic.Interface
{
    public interface IArchiveServiceRepresentativeBusiness
    {
        void DeleteArchive(string id);
        ArchiveServiceRepresentativeView GetArchServRep(string id);
        List<ArchiveServiceRepresentativeView> GetAllArchServReps();
        void ArchiveServiceRep(string id);
        void Restore(string id);
      
    }
}
