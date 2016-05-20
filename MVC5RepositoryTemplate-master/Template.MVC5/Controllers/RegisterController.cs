using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Template.BusinessLogic;
using Template.BusinessLogic.Implementation;
using Template.Model.RegisterNlogin;
using Template.Model.ViewModels;

namespace Template.MVC5.Controllers
{
    public class RegisterController : Controller
    {
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        public static string password;
        // GET: Register
        public ActionResult Index()
        {
            if(HttpContext.User.Identity.IsAuthenticated)
            {
                if(HttpContext.User.Identity.Name!=null)
                {
                    RegisterBusiness rb = new RegisterBusiness();
                    rb.viewApplicant(HttpContext.User.Identity.Name).ConfirmPassword = password;
                    rb.viewApplicant(HttpContext.User.Identity.Name).Password = password;
                    return View(rb.viewApplicant(HttpContext.User.Identity.Name));
                }
            }
            return View(new RegisterModel());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Index(RegisterModel objRegisterModel)
        {
            var registerbusiness = new RegisterBusiness();
            if (ModelState.IsValid)
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.User.Identity.Name != null)
                    {
                        RegisterBusiness rb = new RegisterBusiness();
                        rb.UpdateApplication(objRegisterModel,password);                        
                        return RedirectToAction("AddPackage");
                    }
                    else
                        return View(objRegisterModel);
                }
                else
                {
                    if (registerbusiness.FindUser(objRegisterModel.identityNumber, AuthenticationManager))
                    {
                        @TempData["Error"] = "User already exists";
                        return View(objRegisterModel);
                    }
                    if (registerbusiness.calcAge(objRegisterModel.identityNumber) < 18)
                    {
                        @TempData["Error"] = "Applicants must be atleast 18 years of age to be a Policy Holder";
                        return View(objRegisterModel);
                    }
                    if (registerbusiness.calcAge(objRegisterModel.identityNumber) > 65)
                    {
                        @TempData["Error"] = "Applicants must be atmost 65 years of age to be a Policy Holder";
                        return View(objRegisterModel);
                    }
                    var result = await registerbusiness.RegisterUser(objRegisterModel, AuthenticationManager);
                    password = objRegisterModel.Password;
                    if (result)
                    {
                        return RedirectToAction("AddPackage");
                    }
                    else
                    {
                        ModelState.AddModelError("", result.ToString());
                        return View(objRegisterModel);
                    }
                }                
            }
            return View(objRegisterModel);
        }

        // GET: Beneficiary
        public ActionResult AddBeneficiary()
        {
            var objRB = new RegisterBusiness();
            ViewBag.List = objRB.getBeneficiariesByClientID(HttpContext.User.Identity.Name);
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddBeneficiary(BeneficiaryViewModel model)
        {
            if(ModelState.IsValid)
            {
                var objRB = new RegisterBusiness();
                ViewBag.List = objRB.getBeneficiariesByClientID(HttpContext.User.Identity.Name);
                var pb = new PackageBusiness();
                for(int x=0;x<pb.GetAll().Count;x++)
                {
                    //find the Applicant's package
                    if(pb.GetAll()[x].PackageId == pb.GetById(objRB.getApplicant(HttpContext.User.Identity.Name).packageID).PackageId)
                    {
                        //if we haven't reached the maximum number the package can cover, add the beneficiary
                        if(objRB.getBeneficiariesByClientID(HttpContext.User.Identity.Name).Count < pb.GetById(objRB.getApplicant(HttpContext.User.Identity.Name).packageID).maxBeneficiary)
                        {
                            objRB.addBeneficiary(model, HttpContext.User.Identity.Name);
                            TempData["Error"] = RegisterBusiness.feedback;
                            break;
                        }
                        else //otherwise return an alert to the user
                            TempData["Error"] = pb.GetAll()[x].Name+ " package can cover up to " + pb.GetAll()[x].maxBeneficiary + " beneficiaries";                        
                    }
                }
                return RedirectToAction("ReloadBen");
            }
            return View(model);
        }
        public ActionResult AddPackage()
        {
            var pb = new PackageBusiness();
            return View(pb.GetPackagesWithBenefits());
        }
        public ActionResult ChosenPackage(int? packID)
        {
            if (packID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var objRB = new RegisterBusiness();
            objRB.ChoosePackage((int)packID, HttpContext.User.Identity.Name);
            var EB = new EmailBusiness();
            EB.to = new MailAddress(objRB.getApplicant(HttpContext.User.Identity.Name).emailAdress);
            EB.sub = "Proof Of Application - Mpiti Funeral Undertakers";
            EB.body = " You Were Successfully Registered To Mpiti Funeral Undertakers  " 
                + "<br/>To complete your application, please upload <b>all</b> your supporting documents (certified), including the attached document (signed)."
                + "<br/><br/>Please Note: Your policy won't be in force until all supporting documents are recieved"
                + "<br/><br/> Regards<br/><b>Mpiti Funeral Undertakers - Communications Team</b>";
            EB.NotificationWithAttachment();
            return RedirectToAction("AddBeneficiary");
        }
        public ActionResult Beneficiaries()
        {
            var objRB = new RegisterBusiness();
            return View(objRB.getBeneficiariesByClientID(HttpContext.User.Identity.Name));
        }
        // GET: Supporting Documents
        public ActionResult outstandingDocument()
        {
            var objRB = new RegisterBusiness();
            return View(objRB.OutstandingDocList(HttpContext.User.Identity.Name));
        }                
        public ActionResult AddDocuments(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var objRB = new RegisterBusiness();
            return View(objRB.getPersonIn_Docs((int)id));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddDocuments(DocumentViewModel model, HttpPostedFileBase upload)
        {
            if(ModelState.IsValid)
            {
                byte[] uploadedFile = new byte[upload.InputStream.Length];
                upload.InputStream.Read(uploadedFile, 0, uploadedFile.Length);
                model.document = uploadedFile;
                var objRB = new RegisterBusiness();
                objRB.UploadDocument(model);
                if(objRB.NumberOfOutstandingDocs(HttpContext.User.Identity.Name)==0)
                {
                    objRB.AppStatusUpdate(HttpContext.User.Identity.Name);
                    var EB = new EmailBusiness();
                    EB.to = new MailAddress(objRB.getApplicant(HttpContext.User.Identity.Name).emailAdress);
                    EB.sub = "Application Recieved - Mpiti Funeral Undertakers";
                    EB.body = " We recieved your application for our kind consideration"
                        + "<br/>You may track your application on our site. "
                        + "<br/><br/> Regards<br/><b>Mpiti Funeral Undertakers - Communications Team</b>";
                    EB.Notification();
                    return RedirectToAction("Submitted");
                }
            }
            return RedirectToAction("outstandingDocument");
        }

        public ActionResult ReplaceDoc(int? idDoc)
        {
            if (idDoc == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var objRB = new RegisterBusiness();
            objRB.replace((int)idDoc);
            return RedirectToAction("AddDocuments", new { id = idDoc });
        }
        public ActionResult Submitted()
        {
            TempData["Success"] = "We recieved your application and is now taken into consideration.";
            TempData["Incomplete"] = "";
            var objRB = new RegisterBusiness();
            if (objRB.NumberOfOutstandingDocs(HttpContext.User.Identity.Name) != 0)
            {
                TempData["Incomplete"] = "There were still outstanding documents, your applicattion will not be considered complete until all documents are recieved.";
                return View();
            }
            

            return View();
        }
        public ActionResult ReloadBen()
        {
            return RedirectToAction("AddBeneficiary");
        }

        //public ActionResult TestInsert()
        //{
        //    var clinicbusiness = new ClinicBusiness();

        //    clinicbusiness.AddClinic(
        //        new ClinicView()
        //        {
        //            ClinicId = 0,
        //            ClinicName = "Test",
        //            User = new RegisterModel() { Email = "cassimv", Password = "password"}
        //        }, AuthenticationManager);

        //    return RedirectToAction("Index");
        //}
    }
}