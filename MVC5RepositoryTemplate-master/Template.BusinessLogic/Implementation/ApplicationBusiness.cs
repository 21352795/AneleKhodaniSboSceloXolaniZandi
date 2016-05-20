using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Template.Data;
using Template.Model.ViewModels;
using Template.Service.Implementation;

namespace Template.BusinessLogic.Implementation
{
    public class ApplicationBusiness
    {
        public UserManager<ApplicationUser> UserManager { get; set; }
        public static string feedback;
        public ApplicationBusiness()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DataContext()));
            feedback = "";
        }
        public void ApproveApplication(int appID)
        {
            try
            {
                createPolicyHolder(appID);
                using (var CArepo = new ClientApplicationRepository())
                {
                    ClientApplication client = CArepo.Find(x => x.applicationID == appID).SingleOrDefault();
                    createBeneficiaries(client.IDNumber);
                    createDocuments(appID);                    
                    //removeDocs(appID);
                    //removeBenef(client.IDNumber);
                }                    
            }        
            catch (Exception ex)
            {
                feedback = "Request unsuccessfull";
            }
        }
        public void DeclineApplication(int appID,string reason)
        {
            try
            {
                using (var CArepo = new ClientApplicationRepository())
                {
                    ClientApplication client = CArepo.Find(x => x.applicationID == appID).SingleOrDefault();
                    client.status = "Unsuccessfull : " + reason;
                    removeDocs(appID);
                    removeBenef(client.IDNumber);
                    //send email
                }
            }
            catch(Exception ex)
            {
                feedback = "Request unsuccessfull";
            }
        }
        public void createPolicyHolder(int appID)
        {
            var CArepo = new ClientApplicationRepository();
            try
            {
                ClientApplication client = CArepo.Find(x => x.applicationID == appID).SingleOrDefault(); // find client in the applications table
                PolicyHolder ph = new PolicyHolder() //initialise the client as policy holder
                {
                    IDNumber = client.IDNumber,
                    title = client.title,
                    firstName = client.firstName,
                    lastName = client.lastName,
                    province = client.province,
                    contactNumber = client.contactNumber,
                    emailAdress = client.emailAdress,
                    physicalAddress = client.physicalAddress,
                    postalAddress = client.postalAddress,
                    packageID = client.packageID,
                    dateStarted = DateTime.Now,
                    status = "Active"
                };
                using (var polRepo = new PolicyHolderRepository())
                {
                    polRepo.Insert(ph); //Save as policy holder
                    var rolb = new RoleBusiness();
                    rolb.AddUserToRole(ph.IDNumber,"Policy Holder");
                }
            }
            catch (Exception ex)
            {
                feedback = "Request unsuccessfull";
            }
        }
        public void createBeneficiaries(string IDNum)
        {
            var BenefitiaryArepo = new ClientApplicationBeneficiaryRepository();
            var benList = (from ben in BenefitiaryArepo.GetAll()                           
                           where ben.IDNumber == IDNum
                           select ben).ToList();
            foreach(var ben in benList)
            {
                PolicyBeneficiary pb = new PolicyBeneficiary()
                {
                    benIDNumber=ben.benIDNumber,
                    IDNumber = ben.IDNumber,
                    firstName = ben.firstName,
                    lastName= ben.lastName,
                    relationship = ben.relationship,
                    age = ben.age
                };
                using (var polBenRepo = new PolicyBeneficiaryRepository())
                {
                    polBenRepo.Insert(pb);
                }
            }
        }
        public void createDocuments(int appID)
        {
            var CADocRepo = new ClientApplicationDocumentRepository();
            var docList = (from x in CADocRepo.GetAll()
                           where x.applicationID == appID
                           select x).ToList();

            var CArepo = new ClientApplicationRepository();
            ClientApplication client = CArepo.Find(x => x.applicationID == appID).SingleOrDefault(); // find client in the applications table
            var polRepo = new PolicyHolderRepository();
            foreach (var doc in docList)
            {
                using (var polDocRepo = new PolicyDocumentRepository())
                {                    
                    PolicyDocument pd = new PolicyDocument()
                    {                        
                          policyNo = polRepo.Find(x=>x.IDNumber ==client.IDNumber).SingleOrDefault().policyNo,
                          IDNumber = doc.IDNumber,
                          fullname = doc.fullname,
                          documentName = doc.documentName,
                          document = doc.document
                    };               
                    polDocRepo.Insert(pd);
                }
            }
        }
        public void updateAppStatus(int appID, string stat)
        {
            using (var CArepo = new ClientApplicationRepository())
            {
                ClientApplication x = CArepo.GetById(appID);
                x.status = stat;
                CArepo.Update(x);
            }
        }
        public void removeBenef(string IDNum)
        {
            var BenefitiaryArepo = new ClientApplicationBeneficiaryRepository();
            var benList = (from ben in BenefitiaryArepo.GetAll()
                           where ben.IDNumber == IDNum
                           select ben).ToList();
            foreach (var ben in benList)
            {
                BenefitiaryArepo.Archive(ben);
            }
        }
        public void removeDocs(int appID)
        {
            var CADocRepo = new ClientApplicationDocumentRepository();
            var docList = (from x in CADocRepo.GetAll()
                           where x.applicationID == appID
                           select x).ToList();
            foreach (var doc in docList)
            {
                CADocRepo.Delete(doc);
            }
        }
        public List<ClientApplication> getAllApplications()
        {
            using (var CArepo = new ClientApplicationRepository())
            {
                return (from x in CArepo.GetAll()
                        select x).ToList();
            }
        }
        public List<ClientApplication> getNewApplications()
        {
            using (var CArepo = new ClientApplicationRepository())
            {
                return (from x in CArepo.GetAll()
                        where x.status== "Proccessing"
                        select x).ToList();
            }
        }
        public List<ClientApplication> getIncompleteApplications()
        {
            using (var CArepo = new ClientApplicationRepository())
            {
                return (from x in CArepo.GetAll()
                        where x.status == "Awaiting outstanding supporting documents"
                        select x).ToList();
            }
        }
        public List<ClientApplication> getApprovedApplications()
        {
            using (var CArepo = new ClientApplicationRepository())
            {
                return (from x in CArepo.GetAll()
                        where x.status== "Approved"
                        select x).ToList();
            }
        }
        public List<ClientApplication> getDeclinedApplications()
        {
            using (var CArepo = new ClientApplicationRepository())
            {
                return (from x in CArepo.GetAll()
                        where x.status == "Declined"
                        select x).ToList();
            }
        }
        public ApplicationViewModel getApplication(int appID)
        {
            var CArepo = new ClientApplicationRepository();
            ApplicationViewModel appView = new ApplicationViewModel();
            try
            {
                ClientApplication client = CArepo.Find(x => x.applicationID == appID).SingleOrDefault(); // find client in the applications table
                appView.applicationID = appID;
                appView.IDNumber = client.IDNumber;
                appView.title = client.title;
                appView.firstName = client.firstName;
                appView.lastName = client.lastName;
                appView.province = client.province;
                appView.contactNumber = client.contactNumber;
                appView.emailAdress = client.emailAdress;
                appView.physicalAddress = client.physicalAddress;
                appView.postalAddress = client.postalAddress;
                using (var pac = new PackageRepository())
                {
                    appView.packageID = pac.GetById(client.packageID).Name;
                }                
                appView.dateSubmitted = client.dateSubmitted;
                appView.status = client.status;

                var BenefitiaryArepo = new ClientApplicationBeneficiaryRepository();
                var benList = (from ben in BenefitiaryArepo.GetAll()
                               where ben.IDNumber == client.IDNumber
                               select ben).ToList();
                foreach (var ben in benList)
                {
                    appView.beneficiaries.Add(ben);
                }

                var CADocRepo = new ClientApplicationDocumentRepository();
                var docList = (from x in CADocRepo.GetAll()
                               where x.applicationID == appID
                               select x).ToList();
                foreach (var doc in docList)
                {
                    appView.documents.Add(doc);
                }
            }
            catch (Exception ex)
            {
                feedback = "Request unsuccessfull";
            }
            return appView;
        }
        public List<ClientApplicationBeneficiary> getBeneficiariesByAppID(int appID)
        {
            var BenefitiaryArepo = new ClientApplicationBeneficiaryRepository();
            var ClientArepo = new ClientApplicationRepository();
            ClientApplication client = ClientArepo.Find(x => x.applicationID == appID).SingleOrDefault(); // find client
            List<ClientApplicationBeneficiary> cab = new List<ClientApplicationBeneficiary>();
            cab = (from cl in ClientArepo.GetAll()
                   join ben in BenefitiaryArepo.GetAll()
                   on cl.IDNumber equals ben.IDNumber
                   where ben.IDNumber== client.IDNumber
                   select ben).ToList();
            return cab;
        }
        public List<ClientApplicationDocument> AttachedDocList(int appID)
        {
            var ClientArepo = new ClientApplicationRepository();
            var CADocRepo = new ClientApplicationDocumentRepository();
            var docList = (from x in CADocRepo.GetAll()
                           where x.applicationID == appID
                           select x).ToList();
            return docList; //list of supporting documents 
        }
        public ClientApplication getApplicationByIDNumber(string ID)
        {
            using (var CArepo = new ClientApplicationRepository())
            {
                return CArepo.Find(x => x.IDNumber == ID).FirstOrDefault();
            }
        }
        public byte[] doc(int id)
        {
            var CADocRepo = new ClientApplicationDocumentRepository();
            return CADocRepo.GetById(id).document;
        }
    }
}
