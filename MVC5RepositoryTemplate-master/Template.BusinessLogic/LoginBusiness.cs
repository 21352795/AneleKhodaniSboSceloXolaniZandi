using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Template.Data;
using Template.Model.RegisterNlogin;

namespace Template.BusinessLogic
{
    public class LoginBusiness
    {
        public UserManager<ApplicationUser> UserManager { get; set; }

        public LoginBusiness()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DataContext()));
        }

        public async Task<bool> LogUserIn(LoginModel objLoginModel, IAuthenticationManager authenticationManager)
        {
            var user = await UserManager.FindAsync(objLoginModel.UserName, objLoginModel.Password);
            if (user != null)
            {
                await SignInAsync(user, objLoginModel.RememberMe, authenticationManager);
                return true;
            }
            return false;
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent, IAuthenticationManager authenticationManager)
        {
            authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }
    }
}
