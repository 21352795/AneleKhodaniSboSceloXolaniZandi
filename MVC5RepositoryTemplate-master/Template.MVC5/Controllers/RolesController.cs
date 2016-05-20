using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Template.BusinessLogic;
using Template.Data;
using Template.Model;

namespace Template.MVC5.Controllers
{
    public class RolesController : Controller
    {
        RoleBusiness rb = new RoleBusiness();
        DataContext con = new DataContext();

        // GET: Roles
        public ActionResult Index()
        {
            return View(rb.AllRoles());
        }

        [HttpGet]
        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRole(string name)
        {
            bool found = rb.RoleExists(name);

            if (found == true)
            {
                ViewBag.Result = "Role name " + name + " already exists.";
            }

            else
            {
                rb.CreateRole(name);
                ViewBag.Result = "Role created successfully.";
            }

            return View();
        }

    }
}