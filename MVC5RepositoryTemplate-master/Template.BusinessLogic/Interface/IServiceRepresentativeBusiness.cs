using System.Collections.Generic;
using Template.Model.ViewModels;

namespace Template.BusinessLogic.Interface
{
    public interface IServiceRepresentativeBusiness
    {
        List<ServiceRepresentativeView> GetAll();
        void AddServiceRep(ServiceRepresentativeView objServRepView);
        ServiceRepresentativeView GetbyServRepNo(string repno);
        void UpdateServiceRep(ServiceRepresentativeView model);
    }
}
