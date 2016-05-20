using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Owin.Security;
using Template.BusinessLogic;
using Template.Model.RegisterNlogin;

namespace Template.MVC5.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {
            var loginview = new LoginModel();
            return View(loginview);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Index(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var loginbusiness = new LoginBusiness();
                var result = await loginbusiness.LogUserIn(model, AuthenticationManager);
                if (result)
                {
                    //if (User.IsInRole("Admin"))
                    //{
                    //    return RedirectToAction("AddRole", "Roles");
                    //}
                    //if (User.IsInRole("Service Representative"))
                    //{
                    //    return RedirectToAction("Index", "Applications");
                    //}
                    //if (User.IsInRole("Policy Holder"))
                    //{
                    //    return RedirectToAction("ApplicationStatus", "Applications");
                    //} 
                    return RedirectToLocal(returnUrl);
                }
              
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}