using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Data;
using Template.Model.ViewModels;
using Template.Service.Implementation;

namespace Template.BusinessLogic.Implementation
{
    public class PolicyHolderBusiness
    {
        public UserManager<ApplicationUser> UserManager { get; set; }
        public PolicyHolderBusiness()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DataContext()));
        }

        public List<PolicyHolderViewModel> getAll()
        {
            using (var phr = new PolicyHolderRepository())
            {
                return phr.GetAll().Select(x => new PolicyHolderViewModel()
                {
                    policyNo = x.policyNo,
                    IDNumber = x.IDNumber,
                    title = x.title,
                    firstName = x.firstName,
                    lastName = x.lastName,
                    dateStarted = x.dateStarted,
                    status = x.status
                }).ToList();
            }
        }
        public List<PolicyHolderViewModel> get_PH_ByProvince(string prv)
        {
            using (var phr = new PolicyHolderRepository())
            {
                return phr.GetAll().Where(q=>q.province == prv).Select(x => new PolicyHolderViewModel()
                {
                    policyNo = x.policyNo,
                    IDNumber = x.IDNumber,
                    title = x.title,
                    firstName = x.firstName,
                    lastName = x.lastName,
                    dateStarted = x.dateStarted,
                    status = x.status
                }).ToList();
            }
        }
        public List<PolicyHolderViewModel> get_PH_ByGender(string gender)
        {
            using (var phr = new PolicyHolderRepository())
            {
                return phr.GetAll().Where(q=> getGender(q.IDNumber)==gender).Select(x => new PolicyHolderViewModel()
                {
                    policyNo = x.policyNo,
                    IDNumber = x.IDNumber,
                    title = x.title,
                    firstName = x.firstName,
                    lastName = x.lastName,
                    dateStarted = x.dateStarted,
                    status = x.status
                }).ToList();
            }
        }
        public List<PolicyHolderViewModel> get_PH_ByPackage(int pack)
        {
            using (var phr = new PolicyHolderRepository())
            {
                return phr.GetAll().Where(q => q.packageID == pack).Select(x => new PolicyHolderViewModel()
                {
                    policyNo = x.policyNo,
                    IDNumber = x.IDNumber,
                    title = x.title,
                    firstName = x.firstName,
                    lastName = x.lastName,
                    dateStarted = x.dateStarted,
                    status = x.status
                }).ToList();
            }
        }

        public List<ProfileApplicationView> getApplications()
        {
            using (var ProfileAppBenRep = new ProfileApplicationBeneficiaryRepository())
            {
                using (var ProfileAppDocRep = new ProfileApplicationDocumentsRepository())
                {
                    return ProfileAppBenRep.GetAll().Select(x => new ProfileApplicationView()
                    {
                        beneficiaryID = x.beneficiaryID,
                        benIDNumber = x.benIDNumber,
                        IDNumber = x.IDNumber,
                        firstName = x.firstName,
                        lastName = x.lastName,
                        relationship = x.relationship,
                        age = x.age,
                        AddOrDelete = x.AddOrDelete,
                        reason = x.reason,
                        documents = ProfileAppDocRep.GetAll().Select(d => new ProfileApplicationDocuments()
                        {
                            documentID =d.documentID,
                            IDNumber = d.IDNumber,
                            documentName = d.documentName,
                            document = d.document,
                            fullname = d.fullname,
                            PolicyHolderIDN = d.PolicyHolderIDN
                        }).ToList()
                    }).ToList();
                }
            }                
        }


        public string getGender(string IDNum)
        {
            if (Convert.ToInt16(IDNum.Substring(6, 1)) < 5)
                return "Female";
            else
                return "Male";
        }
    }
}
