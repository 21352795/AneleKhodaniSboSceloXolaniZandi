using Microsoft.AspNet.Identity.EntityFramework;

namespace Template.Data
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {

        }

        public ApplicationRole(string roleName)
            : base(roleName)
        {
        }
    }

}
