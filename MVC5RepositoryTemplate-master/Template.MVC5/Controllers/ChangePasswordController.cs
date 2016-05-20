using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Template.BusinessLogic.Implementation;
using Template.Data;
using Template.Model.ViewModels;

namespace Template.MVC5.Controllers
{
    public class ChangePasswordController : Controller
    {
        private DataContext db = new DataContext();
        public ChangePasswordController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DataContext())))
        {
        }

        public ChangePasswordController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        // GET: ChangePassword
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = UserManager.FindByName(HttpContext.User.Identity.Name);
                IdentityResult result = UserManager.ChangePassword(user.Id, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    //Record this in the event log
                    try
                    {
                            
                            ProfileActivityLog pal = new ProfileActivityLog()
                            {
                                IDNumber = HttpContext.User.Identity.Name,
                                EventDate = DateTime.Now,
                                Activity = "Changed your password"
                            };
                            db.ProfileActivityLogs.Add(pal);
                            db.SaveChanges();
                        var pb = new ProfileBusiness();
                        var EB = new EmailBusiness();
                        EB.to = new MailAddress(pb.getContactDetails(HttpContext.User.Identity.Name).emailAdress);
                        EB.sub = "Password Changed";
                        EB.body = "This is to alert you that your password was recently changed."
                            + "<br/><br/> Regards<br/><b>Mpiti Funeral Undertakers - Security Team</b>";
                        EB.Notification();

                    }
                    catch (Exception ex) { }
                    authenticationManager.SignOut();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "changing the password.");
                }
            }
            return View(model);
        }

    }
}