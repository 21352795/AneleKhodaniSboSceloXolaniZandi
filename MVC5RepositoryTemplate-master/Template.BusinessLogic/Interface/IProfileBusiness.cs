using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Model.ViewModels;

namespace Template.BusinessLogic.Interface
{
    public interface IProfileBusiness
    {
        PersonalInfoViewModel getPersonalInfo(string IDNum);
        ContactDetailsViewModel getContactDetails(string IDNum);
        PolicyDetailsViewModel getPolicyDetails(string IDNum);
        List<BeneficiariesViewModel> getBeneficiaries(string IDNum);
        List<PersonalPaymentHistory> getPaymentHistory(string IDNum);
        string getSpouse(string IDNum);
        string getDateOfBirth(string IDNum);
    }
}
