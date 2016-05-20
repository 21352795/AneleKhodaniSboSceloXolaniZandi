using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Template.BusinessLogic.Interface;
using Template.Data;
using Template.Model.ViewModels;
using Template.Service.Implementation;

namespace Template.BusinessLogic.Implementation
{
    public class ServiceRepresentativeBusiness : IServiceRepresentativeBusiness
    {
        public UserManager<ApplicationUser> UserManager { get; set; }

        DataContext con = new DataContext();

        RoleBusiness rb = new RoleBusiness();

        public ServiceRepresentativeBusiness()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(con));
        }
        public List<ServiceRepresentativeView> GetAll()
        {
            using (var servicerreprepo = new ServiceRepresentativeRepository())
            {
                return servicerreprepo.GetAll().Select(x => new ServiceRepresentativeView() { ServiceRepIdNo = x.ServiceRepIdNo, IDNumber = x.IDNumber, Fullname = x.Fullname, Email = x.Email, ContactNo = x.ContactNo }).ToList();        
            }
        }        
        public string geneServRepNo(string id, string fname)
        {
            string servrepno = "";
            int num = 0;

            int space1 = fname.IndexOf(" ");
            int space2 = fname.LastIndexOf(" ");

            if(space2 < 0 || space1 == space2)
            {
                servrepno = fname.Substring(0, 1) + fname.Substring(space1 + 1, 1) + id.Substring(0, 4);
            }

            else
            {
                servrepno = fname.Substring(0, 1) + fname.Substring(space1 + 1, 1) + fname.Substring(space2 + 1, 1) + id.Substring(0, 4);
            }

            if (GetbyServRepNo(servrepno).ServiceRepIdNo != null)
            {
                num = 9;
                int rep = Convert.ToInt16(servrepno.Substring(3)) + num;
                servrepno = servrepno.Substring(0, 4) + rep;
            }

            return servrepno;
        }

        public string genePassword(string id)
        {
            return id.Substring(0, 6);
        }

        public void AddServiceRep(ServiceRepresentativeView objServRepView)
        {
            using (var servreprepo = new ServiceRepresentativeRepository())
            {
                var newuser = new ApplicationUser()
                {
                    Id = objServRepView.IDNumber,
                    UserName = objServRepView.IDNumber,
                    FullName = objServRepView.Fullname,
                    Email = objServRepView.Email,
                    PasswordHash = UserManager.PasswordHasher.HashPassword(genePassword(objServRepView.IDNumber))
                };

                var result = UserManager.CreateAsync(newuser, genePassword(objServRepView.IDNumber));

                var servrep = new ServiceRepresentative
                {
                    ServiceRepIdNo = geneServRepNo(objServRepView.IDNumber, objServRepView.Fullname),
                    IDNumber = objServRepView.IDNumber,
                    Fullname = objServRepView.Fullname,
                    Email = objServRepView.Email,
                    ContactNo = objServRepView.ContactNo,
                    AppUserId = newuser.Id
                };              

                servreprepo.Insert(servrep);

                rb.AddUserToRole(objServRepView.IDNumber, "Service Representative");
            }
        }

        public ServiceRepresentativeView GetbyServRepNo(string repno)
        {
            using (var sreprepo = new ServiceRepresentativeRepository())
            {
                ServiceRepresentative sr = sreprepo.GetByServRepNo(repno);
                var srep = new ServiceRepresentativeView();
                if (sr != null)
                {
                    srep.ServiceRepIdNo = sr.ServiceRepIdNo;
                    srep.IDNumber = sr.IDNumber;
                    srep.Fullname = sr.Fullname;
                    srep.Email = sr.Email;
                    srep.ContactNo = sr.ContactNo;
                }
                return srep;
            }
        }

        public void UpdateServiceRep(ServiceRepresentativeView model)
        {
            using (var servreprepo = new ServiceRepresentativeRepository())
            {
                ServiceRepresentative sr = servreprepo.GetByServRepNo(model.ServiceRepIdNo);

                if (sr != null)
                {
                    sr.IDNumber = model.IDNumber;
                    sr.Fullname = model.Fullname;
                    sr.Email = model.Email;
                    sr.ContactNo = model.ContactNo;

                    servreprepo.Update(sr);
                }
            }

            ApplicationUser user = UserManager.FindById(model.IDNumber);

            if (user != null)
            {
                user.Email = model.Email;
                user.FullName = model.Fullname;
            }
        }
    }
}
