using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Template.Data;
using Template.Model.RegisterNlogin;
using Template.Model.ViewModels;
using Template.Service.Implementation;

namespace Template.BusinessLogic
{
    public class RegisterBusiness
    {
        public UserManager<ApplicationUser> UserManager { get; set; }
        public static string feedback;
        public RegisterBusiness()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DataContext()));
            feedback = "";
        }
        public bool FindUser(string userName, IAuthenticationManager authenticationManager)
        {
            var user = UserManager.FindByName(userName);
            if (user != null)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> RegisterUser(RegisterModel objRegisterModel, IAuthenticationManager authenticationManager)
        {
            var newuser = new ApplicationUser()
            {
                Id = objRegisterModel.identityNumber,
                UserName = objRegisterModel.identityNumber,
                Email = objRegisterModel.Email,
                PasswordHash = UserManager.PasswordHasher.HashPassword(objRegisterModel.Password)
            };

            var result = await UserManager.CreateAsync(
               newuser, objRegisterModel.Password);

            if (result.Succeeded)
            {
                CreateApplication(objRegisterModel);
                await SignInAsync(newuser, false, authenticationManager);
                return true;
            }
            return false;
        }
        //Add Personal information
        public void CreateApplication(RegisterModel objRegisterModel)
        {
            try
            {
                ClientApplication capp = new ClientApplication
                {
                    IDNumber = objRegisterModel.identityNumber,
                    title = objRegisterModel.title,
                    firstName = objRegisterModel.firstName,
                    lastName = objRegisterModel.lastName,
                    province = objRegisterModel.province,
                    contactNumber = objRegisterModel.contactNumber,
                    emailAdress = objRegisterModel.Email,
                    physicalAddress = renderPhysicalAddressSave(objRegisterModel.streetAddress, objRegisterModel.suburb, objRegisterModel.city, objRegisterModel.postalCode.ToString()),                                      
                    packageID = 1,
                    dateSubmitted = DateTime.Now,
                    status = "Awaiting outstanding supporting documents"
                };
                if(objRegisterModel.postalOffice==null)
                {
                    capp.postalAddress = renderPhysicalAddressSave(objRegisterModel.streetAddress, objRegisterModel.suburb, objRegisterModel.city, objRegisterModel.postalCode.ToString());
                }
                else
                {
                    capp.postalAddress = renderPostalAddressSave(objRegisterModel.postalOffice, objRegisterModel.town, objRegisterModel.boxpostalCode);
                }
                using (var CArepo = new ClientApplicationRepository())
                {                    
                    CArepo.Insert(capp);
                    ClientApplication client = CArepo.Find(x => x.IDNumber == capp.IDNumber).SingleOrDefault();
                    ClientApplicationDocument docmnt = new ClientApplicationDocument()
                    {
                        applicationID = client.applicationID,
                        IDNumber = client.IDNumber,
                        fullname = client.firstName + " " + client.lastName,
                        documentName = "ID Document",
                        document = null,
                    };
                    using (var CADocRepo = new ClientApplicationDocumentRepository())
                    {
                        CADocRepo.Insert(docmnt);
                    }
                    ClientApplicationDocument doc = new ClientApplicationDocument()
                    {
                        applicationID = client.applicationID,
                        IDNumber = client.IDNumber,
                        fullname = client.firstName + " " + client.lastName,
                        documentName = "Signed Policy Document",
                        document = null,
                    };
                    using (var CADocRepo = new ClientApplicationDocumentRepository())
                    {
                        CADocRepo.Insert(doc);
                    }
                    feedback = "We recieved your Personal Information, please indicate your Beneficiaries.";
                    //send email
                }
            }
            catch(Exception ex)
            {
                feedback +=" : "+ ex;
            }
        }
        //When the user clicks back button, redisplay the information for edition
        public RegisterModel viewApplicant(string id)
        {
            if (getApplicant(id) != null)
            {
                return new RegisterModel()
                {
                    identityNumber = getApplicant(id).IDNumber,
                    title = getApplicant(id).title,
                    firstName = getApplicant(id).firstName,
                    lastName = getApplicant(id).lastName,

                    streetAddress = renderStreetView(getApplicant(id).physicalAddress),
                    suburb = renderSuburbView(getApplicant(id).physicalAddress),
                    city = renderCityView(getApplicant(id).physicalAddress),
                    postalCode = Convert.ToInt16(renderPostalView(getApplicant(id).physicalAddress)),

                    postalOffice = renderPOView(getApplicant(id).postalAddress),
                    town = renderTownView(getApplicant(id).postalAddress),
                    boxpostalCode = renderPostalOBView(getApplicant(id).postalAddress),

                    Email = getApplicant(id).emailAdress,
                    province = getApplicant(id).province,
                    contactNumber = getApplicant(id).contactNumber
                };
            }
            return new RegisterModel();
        }
        //When Next is clicked Again on Step 1, update the information
        public void UpdateApplication(RegisterModel objRegisterModel, string oldPassword)
        {
            using (ClientApplicationRepository car = new ClientApplicationRepository())
            {
                var client = getApplicant(objRegisterModel.identityNumber);
                client.title = objRegisterModel.title;
                client.firstName = objRegisterModel.firstName;
                client.lastName = objRegisterModel.lastName;
                client.province = objRegisterModel.province;
                client.contactNumber = objRegisterModel.contactNumber;
                client.emailAdress = objRegisterModel.Email;
                client.physicalAddress = renderPhysicalAddressSave(objRegisterModel.streetAddress, objRegisterModel.suburb, objRegisterModel.city, objRegisterModel.postalCode.ToString());
                if (objRegisterModel.postalOffice == null)
                    client.postalAddress = renderPhysicalAddressSave(objRegisterModel.streetAddress, objRegisterModel.suburb, objRegisterModel.city, objRegisterModel.postalCode.ToString());
                else
                {
                    client.postalAddress = renderPostalAddressSave(objRegisterModel.postalOffice, objRegisterModel.town, objRegisterModel.boxpostalCode);
                }
                car.Update(client);
                DataContext dc = new DataContext();
                dc.SaveChanges();            
            }
            updatePassword(objRegisterModel, oldPassword);
        }
        public void updatePassword(RegisterModel objRegisterModel, string oldPassword)
        {
            IdentityUser user = UserManager.FindByName(objRegisterModel.identityNumber);
            IdentityResult result = UserManager.ChangePassword(user.Id, oldPassword, objRegisterModel.Password);
        }
        public List<ClientApplicationBeneficiary> getBeneficiariesByClientID(string ID)
        {
            var BenefitiaryArepo = new ClientApplicationBeneficiaryRepository();
            var ClientArepo = new ClientApplicationRepository();

            List<ClientApplicationBeneficiary> cab = new List<ClientApplicationBeneficiary>();
            cab = (from cl in ClientArepo.GetAll()
                           join ben in BenefitiaryArepo.GetAll()
                           on cl.IDNumber equals ben.IDNumber
                           where ben.IDNumber == ID
                           select ben).ToList();
            return cab;
        }
        public int calcAge(string id)
        {
            return Convert.ToInt16((DateTime.Now.Year - Convert.ToInt16(id.Substring(0, 2))).ToString().Substring(2));
        }
        public void addBeneficiary(BeneficiaryViewModel model, string ID)
        {
            var BenefitiaryArepo = new ClientApplicationBeneficiaryRepository();
            if (BenefitiaryArepo.Find(x => x.benIDNumber == model.benIDNumber).SingleOrDefault() == null)
            {                
                ClientApplicationBeneficiary ben = new ClientApplicationBeneficiary()
                {
                    benIDNumber = model.benIDNumber,
                    IDNumber = ID,
                    firstName = model.firstName,
                    lastName = model.lastName,
                    relationship = model.relationship,
                    age = calcAge(model.benIDNumber)
                };
                
                var ClientArepo = new ClientApplicationRepository();
                var CADocRepo = new ClientApplicationDocumentRepository();
                ClientApplication client = ClientArepo.Find(x => x.IDNumber == ID).SingleOrDefault();
                if(ben.age >=65)
                {
                    feedback = "We cannot cover a beneficiary of more than 65 years of age";
                    return;
                }
                if(ben.relationship == "Spouse")
                {
                    if (ben.age < 18)
                    {
                        feedback = "Spouse must be at least 18 years of age";
                        return;
                    }
                }

                if (ben.relationship == "Parent" ||ben.relationship== "Grandparent" || ben.relationship == "Parent-in-law")
                {
                    if(ben.age < (calcAge(client.IDNumber)))
                    {
                        feedback = "Parent cannot be younger than the Applicant";
                        return;
                    }
                    else if(ben.age == (calcAge(client.IDNumber)))
                    {
                        feedback = "Parent cannot be at the same age as the Applicant";
                        return;
                    }
                    else if(ben.age > calcAge(client.IDNumber) && (ben.age-calcAge(client.IDNumber))<13)
                    {
                        feedback = "Parent must be at least 13 years older than the Applicant";
                        return;
                    }
                }
                if (ben.relationship == "Uncle" || ben.relationship == "Aunt")
                {
                    if (ben.age < (calcAge(client.IDNumber)))
                    {
                        feedback = "Uncle or Aunt cannot be younger than the Applicant";
                        return;
                    }
                    else if (ben.age == (calcAge(client.IDNumber)))
                    {
                        feedback = "Uncle or Aunt cannot be at the same age as the Applicant";
                        return;
                    }
                    else if (ben.age > calcAge(client.IDNumber) && (ben.age - calcAge(client.IDNumber)) < 5)
                    {
                        feedback = "Uncle or Aunt must be at least 5 years older than the Applicant";
                        return;
                    }
                }
                if (ben.relationship == "Child" || ben.relationship == "Grandchild")
                {
                    if (ben.age > (calcAge(client.IDNumber)))
                    {
                        feedback = "Child cannot be older than you, " + client.firstName;
                        return;
                    }
                    else if (ben.age == (calcAge(client.IDNumber)))
                    {
                        feedback = "Child cannot be at the same age as the Applicant";
                        return;
                    }
                    else if (ben.age < calcAge(client.IDNumber) && (calcAge(client.IDNumber)- ben.age) < 13)
                    {
                        feedback = "Applicant must be at least 13 years older than the Child";
                        return;
                    }
                }
                BenefitiaryArepo.Insert(ben);

                if (ben.relationship == "Spouse")
                {
                    ClientApplicationDocument doc = new ClientApplicationDocument()
                    {
                        applicationID = client.applicationID,
                        IDNumber = client.IDNumber,
                        fullname = client.firstName + " "+client.lastName,
                        documentName = "Marriage Certificate",
                        document = null,
                    };
                    CADocRepo.Insert(doc);
                    ClientApplicationDocument docmnt = new ClientApplicationDocument()
                    {
                        applicationID = client.applicationID,
                        IDNumber = ben.IDNumber,
                        fullname = ben.firstName + " " + ben.lastName,
                        documentName = "ID Document",
                        document = null,
                    };
                    CADocRepo.Insert(docmnt);
                }
                else if (ben.age >= 21 )
                {
                    ClientApplicationDocument docmnt = new ClientApplicationDocument()
                    {
                        applicationID = client.applicationID,
                        IDNumber = ben.IDNumber,
                        fullname = ben.firstName + " " + ben.lastName,
                        documentName = "ID Document",
                        document = null,
                    };
                    CADocRepo.Insert(docmnt);
                    ClientApplicationDocument doc = new ClientApplicationDocument()
                    {
                        applicationID = client.applicationID,
                        IDNumber = ben.benIDNumber,
                        fullname = ben.firstName + " " + ben.lastName,
                        documentName = "Institutional Proof of Registration | (Full time study)",
                        document = null,
                    };
                    CADocRepo.Insert(doc);
                }
                else if (ben.age < 18)
                {
                    ClientApplicationDocument doc = new ClientApplicationDocument()
                    {
                        applicationID = client.applicationID,
                        IDNumber = ben.benIDNumber,
                        fullname = ben.firstName + " " + ben.lastName,
                        documentName = "Birth Certificate",
                        document = null,
                    };
                    CADocRepo.Insert(doc);
                }
                else if (ben.age >= 18 && ben.age < 21)
                {
                    ClientApplicationDocument doc = new ClientApplicationDocument()
                    {
                        applicationID = client.applicationID,
                        IDNumber = ben.benIDNumber,
                        fullname = ben.firstName + " " + ben.lastName,
                        documentName = "ID Document",
                        document = null,
                    };
                    CADocRepo.Insert(doc);
                }
            }
            else
                feedback = "ID Number already exist!";            
        }
        public void ChoosePackage(int packID, string id)
        {
            using (var CArepo = new ClientApplicationRepository())
            {
                ClientApplication client = CArepo.Find(x => x.IDNumber == id).SingleOrDefault();
                client.packageID = packID;
                CArepo.Update(client);
            }
        }
        public List<ClientApplicationDocument> OutstandingDocList(string id)
        {
            if (id == null)
            {
                feedback = "Bad request";
                return null;
            }
            var ClientArepo = new ClientApplicationRepository();
          
            ClientApplication client = ClientArepo.Find(x => x.IDNumber == id).SingleOrDefault();

            var CADocRepo = new ClientApplicationDocumentRepository();
            var docList = (from x in CADocRepo.GetAll()
                           where x.applicationID == client.applicationID
                           select x).ToList();
            return docList; //list of supporting documents 
        }
        public int NumberOfOutstandingDocs(string id)
        {
            return (from x in OutstandingDocList(id)
                    where x.document == null
                    select x).Count();
        }
        public void AppStatusUpdate(string id)
        {
            using (var ClientArepo = new ClientApplicationRepository())
            {
                ClientApplication client = ClientArepo.Find(x => x.IDNumber == id).SingleOrDefault();
                client.status = "Proccessing";
                ClientArepo.Update(client);
            }                
        }
        public DocumentViewModel getPersonIn_Docs(int id)
        {
            DocumentViewModel model = new DocumentViewModel();
            using (var CADocRepo = new ClientApplicationDocumentRepository())
            {
                var doc = CADocRepo.GetById(id);
                model.documentID = doc.documentID;
                model.applicationID = doc.applicationID;
                model.IDNumber = doc.IDNumber;
                model.fullname = doc.fullname;
                model.documentName = doc.documentName;
                model.document = doc.document;
            }
            return model;
        }
        public string UploadDocument(DocumentViewModel model)
        {
            try
            {
                using (var CADocRepo = new ClientApplicationDocumentRepository())
                {
                    ClientApplicationDocument CADoc = CADocRepo.Find(x => x.IDNumber == model.IDNumber && x.fullname == model.fullname && x.documentName == model.documentName).SingleOrDefault();

                    CADoc.documentID = CADoc.documentID;
                    CADoc.applicationID = model.applicationID;
                    CADoc.IDNumber = model.IDNumber;
                    CADoc.documentName = model.documentName;
                    CADoc.document = model.document;
                                       
                    CADocRepo.Update(CADoc);
                }
            }
            catch(Exception ex)
            {
                feedback = "Unable to upload documents, our servers are currently down, please try again.";
            }
            return feedback;
        }
        private async Task SignInAsync(ApplicationUser user, bool isPersistent, IAuthenticationManager authenticationManager)
        {
            authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }
        public ClientApplication getApplicant(string id)
        {
            using (var ClientArepo = new ClientApplicationRepository())
            {
                return ClientArepo.Find(x=>x.IDNumber ==id).First();
            }
        }       
        //Updates the document
        public void replace(int idDoc)
        {
            using (var CADocRepo = new ClientApplicationDocumentRepository())
            {
                var doc = CADocRepo.GetById(idDoc);
                doc.document = null;
                CADocRepo.Update(doc);
            }
        }
        public bool AddUserToRole(string user, string role)
        {
            var result = UserManager.AddToRole(user, role);

            return result.Succeeded;
        }          
        //To save the physical address in one line
        public string renderPhysicalAddressSave(string str, string subb, string city, string postCode)
        {
            return str + "!" + subb + "@" + city + "#" + postCode;
        }
        //retrieve physical address in different line
        public string renderStreetView(string physical)
        {
            return physical.Substring(0, physical.IndexOf('!'));
        }
        public string renderSuburbView(string physical)
        {
            return physical.Substring(physical.IndexOf('!')+1, physical.IndexOf('@')- (physical.IndexOf('!')+1));
        }
        public string renderCityView(string physical)
        {
            return physical.Substring(physical.IndexOf('@')+1, physical.IndexOf('#')- (physical.IndexOf('@')+1));
        }
        public string renderPostalView(string physical)
        {
            return physical.Substring(physical.IndexOf('#')+1);
        }
        //to save postal address in one line
        public string renderPostalAddressSave(string po, string town, string postCode)
        {
            return po + "!" + "@" + town + "#" + postCode;
        }
        public string renderPOView(string postal)
        {
            return postal.Substring(0,postal.IndexOf('!'));
        }
        public string renderTownView(string postal)
        {
            return postal.Substring(postal.IndexOf('@') + 1, postal.IndexOf('#') - (postal.IndexOf('@') + 1));
        }
        public string renderPostalOBView(string postal)
        {
            return postal.Substring(postal.IndexOf('#') + 1);
        }
    }
}
