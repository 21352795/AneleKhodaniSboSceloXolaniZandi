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
    public class ArchiveServiceRepresentativeBusiness : IArchiveServiceRepresentativeBusiness
    {
        public UserManager<ApplicationUser> UserManager { get; set; }

        DataContext con = new DataContext();

        RoleBusiness rb = new RoleBusiness();
        public ArchiveServiceRepresentativeBusiness()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(con));
        }
        public void ArchiveServiceRep(string id)
        {
            var ArchiveRep = new ServiceRepresentativeRepository();
            var ar = new ArchiveServiceRepresentativeRepository();

            ServiceRepresentative e = ArchiveRep.GetByServRepNo(id);
            var user = con.Users.Find(e.IDNumber);

            if (e != null || user != null)
            {
                var arch = new ArchiveServiceRepresentative()
                {
                    ServiceRepIdNo = e.ServiceRepIdNo,
                    IDNumber = e.IDNumber,
                    Fullname = e.Fullname,
                    Email = e.Email,
                    ContactNo = e.ContactNo
                };

                con.Users.Remove(user);                

                ar.Insert(arch);
                ArchiveRep.Delete(e);
            }
        }

        public void DeleteArchive(string id)
        {
            using (var archrepo = new ArchiveServiceRepresentativeRepository())
            {
                ArchiveServiceRepresentative arch = archrepo.GetById(id);
                if (arch != null)
                {
                    archrepo.Delete(arch);
                }
            }
        }

        public List<ArchiveServiceRepresentativeView> GetAllArchServReps()
        {
            using (var archrepo = new ArchiveServiceRepresentativeRepository())
            {
                return archrepo.GetAll().Select(x => new ArchiveServiceRepresentativeView() { ServiceRepIdNo = x.ServiceRepIdNo, IDNumber = x.IDNumber, Fullname = x.Fullname, Email = x.Email, ContactNo = x.ContactNo }).ToList();
            }
        }

        public ArchiveServiceRepresentativeView GetArchServRep(string id)
        {
            using (var sreprepo = new ArchiveServiceRepresentativeRepository())
            {
                ArchiveServiceRepresentative sr = sreprepo.GetById(id);
                var srep = new ArchiveServiceRepresentativeView();
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

        public string genePassword(string id)
        {
            return id.Substring(0, 6);
        }

        public void Restore(string id)
        {
            var ArchiveRep = new ServiceRepresentativeRepository();
            var ar = new ArchiveServiceRepresentativeRepository();

            ArchiveServiceRepresentative arch = ar.GetById(id);

            if (arch != null)
            {
                var mm = new ServiceRepresentative()
                {
                    ServiceRepIdNo = arch.ServiceRepIdNo,
                    IDNumber = arch.IDNumber,
                    Fullname = arch.Fullname,
                    Email = arch.Email,
                    ContactNo = arch.ContactNo
                };

                var newuser = new ApplicationUser()
                {
                    Id = mm.IDNumber,
                    UserName = mm.IDNumber,
                    FullName = mm.Fullname,
                    Email = mm.Email,
                    PasswordHash = UserManager.PasswordHasher.HashPassword(genePassword(mm.IDNumber))
                };

                var result = UserManager.CreateAsync(
                newuser, genePassword(mm.IDNumber));

                ar.Delete(arch);
                ArchiveRep.Insert(mm);

                rb.AddUserToRole(newuser.Id, "Service Representative");
                
            }

        }

    }
}
