using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Template.Data;
using Template.Model;

namespace Template.BusinessLogic
{
    public class RoleBusiness
    {
        DataContext con = new DataContext();
        private RoleManager<ApplicationRole> RoleManager { get; set; }
        private UserManager<ApplicationUser> UserManager { get; set; }

        public RoleBusiness()
        {
            RoleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new DataContext()));
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DataContext()));
        }

        public bool RoleExists(string name)
        {
            return RoleManager.RoleExists(name);
        }

        public bool CreateRole(string name)
        {
            var idResult = RoleManager.Create(new ApplicationRole(name));            
            return idResult.Succeeded;            
        }

        public List<RolesView> AllRoles()
        {
            var list = con.Roles;

            var view = new List<RolesView>();

            foreach(var role in list)
            {
                view.Add(new RolesView()
                {
                    RoleId = role.Id,
                    Name = role.Name
                });
            }

            return view;
        }

        public void AddUserToRole(string userid, string rolename)
        {
            UserManager.AddToRole(userid, rolename);
        }
    }
}
