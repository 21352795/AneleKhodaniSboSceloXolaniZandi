using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.BusinessLogic.Interface;
using Template.Data;
using Template.Model.ViewModels;
using Template.Service.Implementation;

namespace Template.BusinessLogic.Implementation
{
    public class ProfileBusiness : IProfileBusiness
    {
        public UserManager<ApplicationUser> UserManager { get; set; }
        public ProfileBusiness()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DataContext()));
        }
        //Viewing Profile
        public PersonalInfoViewModel getPersonalInfo(string IDNum)
        {
            using (var Holder = new PolicyHolderRepository())
            {
                PolicyHolder member = Holder.Find(x=>x.IDNumber==IDNum).FirstOrDefault();
                var PersonalInfo = new PersonalInfoViewModel();
                if (member != null)
                {
                    PersonalInfo.IDNumber = member.IDNumber;
                    PersonalInfo.firstName = member.firstName;
                    PersonalInfo.lastName = member.lastName;
                    PersonalInfo.province = member.province;
                    PersonalInfo.title = member.title;
                    PersonalInfo.spouse = getSpouse(IDNum);
                    PersonalInfo.dateOfBirth = getDateOfBirth(IDNum);
                    PersonalInfo.gender = getGender(IDNum);
                }
                return PersonalInfo;
            }
        }
        public ContactDetailsViewModel getContactDetails(string IDNum)
        {
            using (var Holder = new PolicyHolderRepository())
            {
                PolicyHolder member = Holder.Find(x => x.IDNumber == IDNum).FirstOrDefault();
                var ContactDetails = new ContactDetailsViewModel();
                var rb = new RegisterBusiness();
                if (member != null)
                {
                    ContactDetails.contactNumber = member.contactNumber;
                    ContactDetails.emailAdress = member.emailAdress;
                    ContactDetails.streetAddress = rb.renderStreetView(member.physicalAddress);
                    ContactDetails.suburb = rb.renderSuburbView(member.physicalAddress);
                    ContactDetails.city = rb.renderCityView(member.physicalAddress);
                    ContactDetails.postalCode=Convert.ToInt16( rb.renderPostalView(member.physicalAddress));
                    ContactDetails.postalOffice = rb.renderPOView(member.postalAddress);
                    ContactDetails.town = rb.renderTownView(member.postalAddress);
                    ContactDetails.boxpostalCode = rb.renderPostalOBView(member.postalAddress);
                }
                return ContactDetails;
            }
        }
        public PolicyDetailsViewModel getPolicyDetails(string IDNum)
        {
            using (var Holder = new PolicyHolderRepository())
            {
                PolicyHolder member = Holder.Find(x => x.IDNumber == IDNum).FirstOrDefault();
                var PolicyDetails = new PolicyDetailsViewModel();
                if (member != null)
                {
                    PolicyDetails.dateStarted = member.dateStarted;
                    using (var pac = new PackageRepository())
                    {
                        PolicyDetails.packageName = pac.GetById(member.packageID).Name;
                    }                        
                    PolicyDetails.policyNo = member.policyNo;
                    PolicyDetails.status = member.status;
                }
                return PolicyDetails;
            }
        }
        public List<BeneficiariesViewModel> getBeneficiaries(string IDNum)
        {
            using (var ben = new PolicyBeneficiaryRepository())
            {
                var Beneficiaries = (from i in ben.GetAll()
                                     where i.IDNumber == IDNum
                                     select i).ToList();                
                return Beneficiaries.Select(x => new BeneficiariesViewModel()
                {
                    benIDNumber = x.benIDNumber,
                    firstName = x.firstName,
                    lastName = x.lastName,
                    relationship = x.relationship,
                    age = x.age
                }).ToList();
            }
        }
        public BeneficiariesViewModel getOneBeneficiary(string benefIDNum)
        {
            using (var ben = new PolicyBeneficiaryRepository())
            {
                var beneficiary = ben.GetAll().Find(x => x.benIDNumber == benefIDNum);
                return new BeneficiariesViewModel()
                {
                    benIDNumber = beneficiary.benIDNumber,
                    firstName = beneficiary.firstName,
                    lastName = beneficiary.lastName,
                    relationship = beneficiary.relationship,
                    age = beneficiary.age
                };
            }
        }
        public List<PersonalPaymentHistory> getPaymentHistory(string IDNum)
        {
            using (var pay = new PremiumPaymentRepository())
            {
                var getMyOwn = (from  i in pay.GetAll()
                                where i.IDNumber == IDNum
                                select i).ToList();

                return getMyOwn.Select(x => new PersonalPaymentHistory()
                {
                    AmountPaid = x.AmountPaid,
                    Date = x.Date,
                    Month = x.Month,
                    PaymentFor = x.PaymentFor,
                    PaymentId = x.PaymentId,
                    PaymentMethod = x.PaymentMethod
                }).ToList();
            }
        }
        //Searching 
        public PersonalPaymentHistory getPaymentByMonth(string IDNum, string month)
        {
            using (var pay = new PremiumPaymentRepository())
            {
                var getMyOwn = pay.GetAll().Find(x=>x.IDNumber ==IDNum && x.Month.ToLower() == month.ToLower());
                if(getMyOwn!=null)
                {
                    return new PersonalPaymentHistory()
                    {
                        AmountPaid = getMyOwn.AmountPaid,
                        Date = getMyOwn.Date,
                        Month = getMyOwn.Month,
                        PaymentFor = getMyOwn.PaymentFor,
                        PaymentId = getMyOwn.PaymentId,
                        PaymentMethod = getMyOwn.PaymentMethod
                    };
                }
                return null;                
            }
        }
        public DocumentViewModel getPersonIn_Docs(int id)
        {
            DocumentViewModel model = new DocumentViewModel();
            using (var padr = new ProfileApplicationDocumentsRepository())
            {
                var doc = padr.GetById(id);
                model.documentID = doc.documentID;
                model.IDNumber = doc.IDNumber;
                model.fullname = doc.fullname;
                model.documentName = doc.documentName;
                model.document = doc.document;
            }
            return model;
        }
        public string getSpouse(string IDNum)
        {
            string name = "";
            using (var ben = new PolicyBeneficiaryRepository())
            {
                name = "Married to " +
                        (from i in ben.GetAll()
                         where i.IDNumber == IDNum && i.relationship == "Spouse"
                         select i.firstName + " " + i.lastName).FirstOrDefault();
                if (name == "Married to ")
                    name = "Not specified";
            }
            return name;
        }
        public string getDateOfBirth(string IDNum)
        {
            string[] month = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            return IDNum.Substring(4, 2) + " " + month[Convert.ToInt16(IDNum.Substring(2, 2)) - 1];
        }
        public string getGender(string IDNum)
        {
            if (Convert.ToInt16(IDNum.Substring(6, 1)) < 5)
                return "Female";
            else
                return "Male";
        }
        //Updating        
        public string updateContactDetails(string IDNum, ContactDetailsViewModel model)
        {
            string feedback = "Request unsuccessfull";
            using (var Holder = new PolicyHolderRepository())
            {
                var rb = new RegisterBusiness();
                PolicyHolder member = Holder.Find(x => x.IDNumber == IDNum).FirstOrDefault();
                if (member != null)
                {
                    member.contactNumber = model.contactNumber;
                    member.physicalAddress = rb.renderPhysicalAddressSave(model.streetAddress,model.suburb,model.city, model.postalCode.ToString());
                    member.postalAddress = rb.renderPostalAddressSave(model.postalOffice,model.town,model.boxpostalCode);
                    Holder.Update(member);
                    try
                    {
                        using (var eventLog = new ProfileActivityLogRepository())
                        {
                            ProfileActivityLog pal = new ProfileActivityLog()
                            {
                                IDNumber = IDNum,
                                EventDate = DateTime.Now,
                                Activity = "Contact Information was updated"
                            };
                            eventLog.Insert(pal);
                        }
                    }
                    catch (Exception ex) { feedback += ex; }
                    feedback = "Contact Information changed";
                }
            }
            return feedback;
        }
        //Applying
        public string applyToRemoveBen(string IDNum, string benefIDNum, string reason)
        {
            string feedback = "Request unsuccessfull : ";
            using (var Holder = new PolicyHolderRepository())
            {
                PolicyHolder member = Holder.Find(x => x.IDNumber == IDNum).FirstOrDefault();
                if (member != null)
                {
                    using (var ben = new PolicyBeneficiaryRepository())
                    {
                        var Beneficiary = (from i in ben.GetAll()
                                           where i.IDNumber == IDNum && i.benIDNumber == benefIDNum
                                           select i).FirstOrDefault();
                        if (Beneficiary != null)
                        {
                            //application here
                            using (var prof = new ProfileApplicationBeneficiaryRepository())
                            {
                                ProfileApplicationBeneficiary pab = new ProfileApplicationBeneficiary()
                                {
                                    IDNumber=IDNum,
                                    benIDNumber = Beneficiary.benIDNumber,
                                    firstName = Beneficiary.firstName,
                                    lastName = Beneficiary.lastName,
                                    relationship = Beneficiary.relationship,
                                    age = Beneficiary.age,
                                    AddOrDelete = "Remove",
                                    reason = reason
                                };
                                prof.Insert(pab);
                                //Record this in the event log
                                try
                                {
                                    using (var eventLog = new ProfileActivityLogRepository())
                                    {
                                        ProfileActivityLog pal = new ProfileActivityLog()
                                        {
                                            IDNumber = IDNum,
                                            EventDate = DateTime.Now,
                                            Activity = "Applied to Remove "+ Beneficiary.firstName+" " +Beneficiary.lastName +" as you beneficiary"
                                        };
                                        eventLog.Insert(pal);
                                    }
                                }catch(Exception ex) { feedback += ex; }
                                feedback = "Request successfull, look forward to recieve a report in 3 to 5 working days.";
                            }
                        }
                        else
                            feedback = "Error while proccessing your request";
                    }
                }
                else
                    feedback += " Something went wrong!";
            }
            return feedback;
        }
        public string applyToAddBen(string IDNum, BeneficiaryViewModel model, string reason)
        {
            string feedback = "Request unsuccessfull";
            using (var Holder = new PolicyHolderRepository())
            {
                PolicyHolder member = Holder.Find(x => x.IDNumber == IDNum).FirstOrDefault();
                if (member != null)// if member was found
                {
                    var rb = new RegisterBusiness();
                    using (var ProfileAppBenRep = new ProfileApplicationBeneficiaryRepository())
                    {//start using ProfileApplicationBeneficiaryRepository
                        if (ProfileAppBenRep.Find(x => x.benIDNumber == model.benIDNumber).SingleOrDefault() == null)
                        {//start if there's no beneficiary found
                            //application here
                            ProfileApplicationBeneficiary ben = new ProfileApplicationBeneficiary()
                            {
                                benIDNumber = model.benIDNumber,
                                IDNumber = IDNum,
                                firstName = model.firstName,
                                lastName = model.lastName,
                                relationship = model.relationship,
                                age = rb.calcAge(model.benIDNumber),
                                AddOrDelete = "Add",
                                reason = reason                                
                            };
                            //Validate the age range of the beneficiary
                            if (ben.age >= 65)
                            {
                                return "We cannot cover a beneficiary of more than 65 years of age";
                            }
                            if (ben.relationship == "Spouse")
                            {
                                if (ben.age < 18)
                                {
                                    return "Spouse must be at least 18 years of age";
                                }
                            }
                            //Compare the Beneficiarie's age with the Policy holder based on their relationship
                            if (ben.relationship == "Parent" || ben.relationship == "Grandparent" || ben.relationship == "Parent-in-law")
                            {
                                if (ben.age < (rb.calcAge(member.IDNumber)))
                                {
                                    return "Parent cannot be younger than the Applicant";
                                }
                                else if (ben.age == (rb.calcAge(member.IDNumber)))
                                {
                                    return "Parent cannot be at the same age as the Applicant";
                                }
                                else if (ben.age > rb.calcAge(member.IDNumber) && (ben.age - rb.calcAge(member.IDNumber)) < 13)
                                {
                                    return "Parent must be at least 13 years older than the Applicant";
                                }
                            }
                            if (ben.relationship == "Uncle" || ben.relationship == "Aunt")
                            {
                                if (ben.age < (rb.calcAge(member.IDNumber)))
                                {
                                    return "Uncle or Aunt cannot be younger than the Applicant";
                                }
                                else if (ben.age == (rb.calcAge(member.IDNumber)))
                                {
                                    return "Uncle or Aunt cannot be at the same age as the Applicant";
                                }
                                else if (ben.age > rb.calcAge(member.IDNumber) && (ben.age - rb.calcAge(member.IDNumber)) < 5)
                                {
                                    return "Uncle or Aunt must be at least 5 years older than the Applicant";
                                }
                            }
                            if (ben.relationship == "Child" || ben.relationship == "Grandchild")
                            {
                                if (ben.age > (rb.calcAge(member.IDNumber)))
                                {
                                    return "Child cannot be older than you, " + member.firstName;
                                }
                                else if (ben.age == (rb.calcAge(member.IDNumber)))
                                {
                                    return "Child cannot be at the same age as the Applicant";
                                }
                                else if (ben.age < rb.calcAge(member.IDNumber) && (rb.calcAge(member.IDNumber) - ben.age) < 13)
                                {
                                    return "Applicant must be at least 13 years older than the Child";
                                }
                            }
                            //We can now create an application
                            ProfileAppBenRep.Insert(ben);
                            //Record this in the event log
                            try
                            {
                                using (var eventLog = new ProfileActivityLogRepository())
                                {
                                    ProfileActivityLog pal = new ProfileActivityLog()
                                    {
                                        IDNumber = IDNum,
                                        EventDate = DateTime.Now,
                                        Activity = "Applied to Add " + ben.firstName + " " + ben.lastName + "as you beneficiary"
                                    };
                                    eventLog.Insert(pal);
                                }
                            }
                            catch (Exception ex) { feedback += ex; }
                            feedback = "Request successfull, look forward to recieve a report in 3 to 5 working days.";

                            //Let's generate the required supporting documents
                            using (var padr = new ProfileApplicationDocumentsRepository())
                            {
                                if (ben.relationship == "Spouse")
                                {
                                    ProfileApplicationDocuments doc = new ProfileApplicationDocuments()
                                    {
                                        PolicyHolderIDN = member.IDNumber,
                                        IDNumber = member.IDNumber,
                                        fullname = member.firstName + " " + member.lastName,
                                        documentName = "Marriage Certificate",
                                        document = null,
                                    };
                                    padr.Insert(doc);
                                    ProfileApplicationDocuments docmnt = new ProfileApplicationDocuments()
                                    {
                                        PolicyHolderIDN = member.IDNumber,
                                        IDNumber = ben.IDNumber,
                                        fullname = ben.firstName + " " + ben.lastName,
                                        documentName = "ID Document",
                                        document = null,
                                    };
                                    padr.Insert(docmnt);
                                }
                                else if (ben.age >= 21)
                                {
                                    ProfileApplicationDocuments docmnt = new ProfileApplicationDocuments()
                                    {
                                        PolicyHolderIDN = member.IDNumber,
                                        IDNumber = ben.IDNumber,
                                        fullname = ben.firstName + " " + ben.lastName,
                                        documentName = "ID Document",
                                        document = null,
                                    };
                                    padr.Insert(docmnt);
                                    ProfileApplicationDocuments doc = new ProfileApplicationDocuments()
                                    {
                                        PolicyHolderIDN = member.IDNumber,
                                        IDNumber = ben.benIDNumber,
                                        fullname = ben.firstName + " " + ben.lastName,
                                        documentName = "Institutional Proof of Registration | (Full time study)",
                                        document = null,
                                    };
                                    padr.Insert(doc);
                                }
                                else if (ben.age < 18)
                                {
                                    ProfileApplicationDocuments doc = new ProfileApplicationDocuments()
                                    {
                                        PolicyHolderIDN = member.IDNumber,
                                        IDNumber = ben.benIDNumber,
                                        fullname = ben.firstName + " " + ben.lastName,
                                        documentName = "Birth Certificate",
                                        document = null,
                                    };
                                    padr.Insert(doc);
                                }
                                else if (ben.age >= 18 && ben.age < 21)
                                {
                                    ProfileApplicationDocuments doc = new ProfileApplicationDocuments()
                                    {
                                        PolicyHolderIDN = member.IDNumber,
                                        IDNumber = ben.benIDNumber,
                                        fullname = ben.firstName + " " + ben.lastName,
                                        documentName = "ID Document",
                                        document = null,
                                    };
                                    padr.Insert(doc);
                                }
                            }//---End  using ProfileApplicationDocumentsRepository
                        }//---End if there's no beneficiary found
                        feedback = "ID Number already exist!";
                    }//---End using ProfileApplicationBeneficiaryRepository
                }//---End if member was found
                else
                    feedback = "An Error occured while proccessing your request.";
            }//End using PolicyHolderRepository
            return feedback;
        }
        public List<ProfileApplicationDocuments> OutstandingDocList(string IDNum)
        {
            if (IDNum != null)
            {
                using (var Holder = new PolicyHolderRepository())
                {
                    PolicyHolder member = Holder.Find(x => x.IDNumber == IDNum).FirstOrDefault();
                    if (member != null)// if member was found
                    {
                        using (var padr = new ProfileApplicationDocumentsRepository())
                        {
                            return (from x in padr.GetAll()
                                           where x.PolicyHolderIDN == member.IDNumber
                                           select x).ToList();
                        }
                    }
                }                               
            }
            return new List<ProfileApplicationDocuments>();
            //list of supporting documents 
        }
        public int NumberOfOutstandingDocs(string id)
        {
            return (from x in OutstandingDocList(id)
                    where x.document == null
                    select x).Count();
        }        
        public void UploadDocument(DocumentViewModel model)
        {
            try
            {
                using (var padr = new ProfileApplicationDocumentsRepository())
                {
                    ProfileApplicationDocuments PADoc = padr.Find(x => x.IDNumber == model.IDNumber && x.fullname == model.fullname && x.documentName == model.documentName).SingleOrDefault();

                    PADoc.documentID = PADoc.documentID;
                    PADoc.PolicyHolderIDN = model.IDNumber;
                    PADoc.IDNumber = model.IDNumber;
                    PADoc.fullname = model.fullname;                   
                    PADoc.documentName = model.documentName;
                    PADoc.document = model.document;

                    padr.Update(PADoc);
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        public void replace(int idDoc)
        {
            using (var padr = new ProfileApplicationDocumentsRepository())
            {
                var doc = padr.GetById(idDoc);
                doc.document = null;
                padr.Update(doc);
            }
        }
        //Profile Activity Log
        public List<EventLogViewModel > eventLog(string IDNum)
        {
            using (var palr = new ProfileActivityLogRepository())
            {
                return palr.GetAll().FindAll(r=>r.IDNumber == IDNum).Select(x => new EventLogViewModel()
                {
                    eventID = x.eventID,
                    Activity = x.Activity,
                    EventDate = x.EventDate.DayOfWeek + " " + x.EventDate.Day + " " + x.EventDate.Month + " " + x.EventDate.Year,
                    Time = x.EventDate.TimeOfDay.ToString()
                }).ToList();
            }
        }
        public List<EventLogViewModel> getEventsByDate(string IDNum, string date)
        {
            return eventLog(IDNum).Where(x => x.EventDate == date).ToList();
        }

    }
}
