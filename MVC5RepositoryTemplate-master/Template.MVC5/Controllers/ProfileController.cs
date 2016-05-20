using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Template.BusinessLogic.Implementation;
using Template.Data;
using Template.Model.ViewModels;

namespace Template.MVC5.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PersonalDetails()
        {
            ProfileBusiness pb = new ProfileBusiness();
            if(User.IsInRole("Policy Holder"))
            {
                return View(pb.getPersonalInfo(HttpContext.User.Identity.Name));
            }
            TempData["Error"] = "You are not recognized as a Policy Holder, Therefore some content was hiden for privacy";
            return View(); 
        }
        public ActionResult ContactDetails()
        {
            ProfileBusiness pb = new ProfileBusiness();
            if (User.IsInRole("Policy Holder"))
            {
                return View(pb.getContactDetails(HttpContext.User.Identity.Name));
            }
            TempData["Error"] = "You are not recognized as a Policy Holder, Therefore some content was hiden for privacy";
            return View();
        }
        public ActionResult PolicyDetails()
        {
            ProfileBusiness pb = new ProfileBusiness();
            if (User.IsInRole("Policy Holder"))
            {
                return View(pb.getPolicyDetails(HttpContext.User.Identity.Name));
            }
            TempData["Error"] = "You are not recognized as a Policy Holder, Therefore some content was hiden for privacy";
            return View();
        }
        public ActionResult Beneficiaries()
        {
            ProfileBusiness pb = new ProfileBusiness();
            if (User.IsInRole("Policy Holder"))
            {
                return View(pb.getBeneficiaries(HttpContext.User.Identity.Name));
            }
            TempData["Error"] = "You are not recognized as a Policy Holder, Therefore some content was hiden for privacy";
            return View();
        }
        public ActionResult PremiumPaymentHistory(string month)
        {
            ProfileBusiness pb = new ProfileBusiness();

            if (User.IsInRole("Policy Holder"))
            {
                if(!String.IsNullOrEmpty(month))
                {
                    if(pb.getPaymentByMonth(HttpContext.User.Identity.Name, month)!=null)
                    {
                        PersonalPaymentHistory found = pb.getPaymentByMonth(HttpContext.User.Identity.Name, month);
                        var toList = new List<PersonalPaymentHistory>();
                        toList.Add(found);
                        return View(toList);
                    }                        
                    else
                    {
                        TempData["Error"] = "No Premium payments were found under " + month;
                        return View(new List<PersonalPaymentHistory>());                        
                    }                       
                }
                return View(pb.getPaymentHistory(HttpContext.User.Identity.Name));
            }
            TempData["Error"] = "You are not recognized as a Policy Holder, Therefore some content was hiden for privacy";
            return View(new List<PersonalPaymentHistory>());
        }

        public ActionResult EditContactDetails()
        {
            ProfileBusiness pb = new ProfileBusiness();
            if (User.IsInRole("Policy Holder"))
            {
                return View(pb.getContactDetails(HttpContext.User.Identity.Name));
            }
            TempData["Error"] = "You are not recognized as a Policy Holder, Therefore some content was hiden for privacy";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditContactDetails(ContactDetailsViewModel model)
        {
            if(ModelState.IsValid)
            {
                ProfileBusiness pb = new ProfileBusiness();
                ViewBag.Feedback = pb.updateContactDetails(HttpContext.User.Identity.Name, model);
                return RedirectToAction("ContactDetails");
            }
            return View(model);
        }
        public ActionResult applyToRemoveBeneficiary(string benefIDNum)
        {
            ProfileBusiness pb = new ProfileBusiness();
            if (!String.IsNullOrEmpty(benefIDNum))
            {
                Session["benefIDNum"] = benefIDNum;
                return View(pb.getOneBeneficiary(benefIDNum));
            }
            ViewBag.Feedback = "Please select a beneficiary to remove by navigating to Benefiaries under your Profile";
            return View(new BeneficiariesViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult applyToRemoveBeneficiary(string reason, int x = 0)
        {
            if(!String.IsNullOrEmpty(Session["benefIDNum"].ToString()))
            {
                ProfileBusiness pb = new ProfileBusiness();
                ViewBag.Feedback = pb.applyToRemoveBen(HttpContext.User.Identity.Name, Session["benefIDNum"].ToString(), reason);
                var EB = new EmailBusiness();
                EB.to = new MailAddress(pb.getContactDetails(HttpContext.User.Identity.Name).emailAdress);
                EB.sub = "Application Recieved - Mpiti Funeral Undertakers";
                EB.body = "This is to confirm that your application for uncovering one beneficiary was recieved for our kind consideration."
                    + "<br/>Look forward to recieve a report in 3 to 5 working days.. "
                    + "<br/><br/> Regards<br/><b>Mpiti Funeral Undertakers - Communications Team</b>";
                EB.Notification();
                return RedirectToAction("Feedback", new { message = ViewBag.Feedback });
            }
            ViewBag.Feedback = "Please select a beneficiary to remove by navigating to Benefiaries under your Profile";
            return View();
        }
        public ActionResult applyToAddBeneficiary()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult applyToAddBeneficiary(BeneficiaryViewModel model, string reason)
        {
            if (ModelState.IsValid)
            {
                var pb = new PackageBusiness();
                var profileB = new ProfileBusiness();
                for (int x = 0; x < pb.GetAll().Count; x++)
                {
                    //find the Policy Holder's package
                    if (pb.GetAll()[x].PackageId == pb.GetByName(profileB.getPolicyDetails(HttpContext.User.Identity.Name).packageName).PackageId)
                    {
                        //if we haven't reached the maximum number the package can cover, add the beneficiary
                        if (profileB.getBeneficiaries(HttpContext.User.Identity.Name).Count < pb.GetByName(profileB.getPolicyDetails(HttpContext.User.Identity.Name).packageName).maxBeneficiary)
                        {                            
                            TempData["Error"] = profileB.applyToAddBen(HttpContext.User.Identity.Name, model, reason);
                            break;
                        }
                        else //otherwise return an alert to the user
                            TempData["Error"] = pb.GetAll()[x].Name + " package can cover up to " + pb.GetAll()[x].maxBeneficiary + " beneficiaries";
                    }
                }
                return RedirectToAction("attachDocuments");
            }
            return View(model);
        }
        public ActionResult attachDocuments()
        {
            var pb = new ProfileBusiness();
            if (User.IsInRole("Policy Holder"))
            {
                return View(pb.OutstandingDocList(HttpContext.User.Identity.Name));
            }
            return View(new List<ProfileApplicationDocuments>());
        }
        public ActionResult AddDocuments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pb = new ProfileBusiness();
            return View(pb.getPersonIn_Docs((int)id));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddDocuments(DocumentViewModel model, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                byte[] uploadedFile = new byte[upload.InputStream.Length];
                upload.InputStream.Read(uploadedFile, 0, uploadedFile.Length);
                model.document = uploadedFile;
                var pb = new ProfileBusiness();
                pb.UploadDocument(model);
                if (pb.NumberOfOutstandingDocs(HttpContext.User.Identity.Name) == 0)
                {
                    var EB = new EmailBusiness();
                    EB.to = new MailAddress(pb.getContactDetails(HttpContext.User.Identity.Name).emailAdress);
                    EB.sub = "Application Recieved - Mpiti Funeral Undertakers";
                    EB.body = "This is to confirm that your application for adding one more beneficiary was recieved for our kind consideration."
                        + "<br/>Look forward to recieve a report in 3 to 5 working days.. "
                        + "<br/><br/> Regards<br/><b>Mpiti Funeral Undertakers - Communications Team</b>";
                    EB.Notification();
                    ViewBag.Feedback = "Application submitted, look forward to recieve a report in 3 to 5 working days..";
                    return RedirectToAction("Feedback", new { message = ViewBag.Feedback });
                }
            }
            return RedirectToAction("attachDocuments");
        }
        public ActionResult ReplaceDoc(int? idDoc)
        {
            if (idDoc == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pb = new ProfileBusiness();
            pb.replace((int)idDoc);
            return RedirectToAction("AddDocuments", new { id = idDoc });
        }
        public ActionResult Feedback(string message)
        {
            ViewBag.Message = message;
            return View();
        }
        public ActionResult EventViewer(string date)
        {
            if (User.IsInRole("Policy Holder"))
            {
                var pb = new ProfileBusiness();
                if(!String.IsNullOrEmpty(date))
                {
                    return View(pb.getEventsByDate(HttpContext.User.Identity.Name, date));
                }
                return View(pb.eventLog(HttpContext.User.Identity.Name));
            }
            return View(new List<EventLogViewModel>());
        }
    }
}