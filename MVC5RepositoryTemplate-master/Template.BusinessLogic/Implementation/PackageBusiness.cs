using System.Collections.Generic;
using System.Linq;
using Template.BusinessLogic.Interface;
using Template.Data;
using Template.Model.ViewModels;
using Template.Service.Implementation;

namespace Template.BusinessLogic.Implementation
{
    public class PackageBusiness : IPackageBusiness
    {
        //Packages
        public void AddPackage(PackageView model)
        {
            using (var prepo = new PackageRepository())
            {
                var pack = new Package();
                {
                    if (GetByName(model.Name).Name == null)
                    {
                        pack.Name = model.Name;
                        pack.PremiumAmount = model.PremiumAmount;
                        pack.maxBeneficiary = model.maxBeneficiary;
                        prepo.Insert(pack);
                    }
                };                
            }
        }
        public List<PackageView> GetAll()
        {
            using (var bus = new PackageRepository())
            {
                return bus.GetAll().Select(x => new PackageView()
                {
                    PackageId = x.packageID,
                    Name = x.Name,
                    PremiumAmount = x.PremiumAmount,
                    maxBeneficiary = x.maxBeneficiary
                }).ToList();
            }
        }
        public PackageView GetByName(string name)
        {
            using (var b = new PackageRepository())
            {
                var model = new PackageView();

                foreach(Package pack in b.GetAll())
                {
                    if (pack.Name == name)
                    {
                        model.PackageId = pack.packageID;
                        model.Name = pack.Name;
                        model.PremiumAmount = pack.PremiumAmount;
                        model.maxBeneficiary = pack.maxBeneficiary;
                    }
                }

                return model;
            }
        }
        public PackageView GetById(int id)
        {
            using (var b = new PackageRepository())
            {
                Package pack = b.GetById(id);
                var model = new PackageView();

                if (pack != null)
                {
                    model.PackageId = pack.packageID;
                    model.Name = pack.Name;
                    model.PremiumAmount = pack.PremiumAmount;
                    model.maxBeneficiary = pack.maxBeneficiary;
                }
                return model;
            }
        }
        public void UpdatePackage(PackageView model)
        {
            using (var prepo = new PackageRepository())
            {
                Package p = prepo.GetById(model.PackageId);
                if (p != null)
                {
                    p.Name = model.Name;
                    p.PremiumAmount = model.PremiumAmount;
                    p.maxBeneficiary = model.maxBeneficiary;
                    prepo.Update(p);
                }
            }
        }
        public PackageView Details(int id)
        {
            PackageView pv = GetById(id);

            if (pv.Name == null)
            {
                
            }

            return pv;
        }

        //Package Benefits
        public void AddPackageBenefit(List<BenefitView> model, int packID)
        {
            using (var prepo = new PackageBenefitRepository())
            {
                List<BenefitView> m = new List<BenefitView>();
                m = model;                
                var ben = new PackageBenefit();
                {
                    foreach (var x in m)
                    {
                        //if (prepo.GetAll().Where(q => q.benefitID == x.benefitID && q.packageID == packID) == null)
                        //{
                            if (x.isBenefitSelected == true)
                            {
                                ben.benefitID = x.benefitID;
                                ben.packageID = packID;
                                prepo.Insert(ben);
                            }
                        //}
                    }
                };
            }
        }
        public List<PackageBenefitView> GetAllPackageBenefits()
        {
            using (var bus = new PackageBenefitRepository())
            {
                return bus.GetAll().Select(x => new PackageBenefitView()
                {
                    pbID = x.pbID,
                    //Name = x.
                    packageID = x.packageID
                }).ToList();
            }
        }
        public List<PackageBenefitView> GetPackagesWithBenefits()
        {
            using (var pbRep = new PackageBenefitRepository())
            {
                return GetAll().Select(x => new PackageBenefitView()
                {
                    packageID = x.PackageId,
                    Name = x.Name,
                    PremiumAmount = x.PremiumAmount,
                    maxBeneficiary = x.maxBeneficiary,
                    benefit = pbRep.GetAll().Where(q => q.packageID == x.PackageId).Select(bv => new BenefitView()
                    {
                        benefitID = bv.benefitID,
                        Name = GetBenefitById(bv.benefitID).Name
                    }).ToList()
                }).ToList();
            }            
        }
        
        //benefits
        public void AddBenefit(BenefitView model)
        {
            using (var prepo = new BenefitRepository())
            {
                var ben = new Benefit();
                {
                    if (GetByName(model.Name).Name == null)
                    {
                        ben.Name = model.Name;
                        prepo.Insert(ben);
                    }
                };
            }
        }

        public List<BenefitView> GetAllBenefits()
        {
            using (var bus = new BenefitRepository())
            {
                return bus.GetAll().Select(x => new BenefitView()
                {
                    benefitID = x.benefitID,
                    Name = x.Name,
                }).ToList();
            }
        }

        public BenefitView GetBenefitByName(string name)
        {
            using (var b = new BenefitRepository())
            {
                var model = new BenefitView();

                foreach (Benefit ben in b.GetAll())
                {
                    if (ben.Name == name)
                    {
                        model.benefitID = ben.benefitID;
                        model.Name = ben.Name;
                    }
                }
                return model;
            }
        }
        public BenefitView GetBenefitByID_And_PackageID(int benID, int packID)
        {
            using (var pac = new PackageBenefitRepository())
            {
                return pac.GetAll().Where(x => x.benefitID == benID && x.packageID == packID).Select(p => new BenefitView()
                {
                    benefitID = p.benefitID,
                    Name = GetBenefitById(p.benefitID).Name
                }).FirstOrDefault();
            }
        }
        public BenefitView GetBenefitById(int id)
        {
            using (var b = new BenefitRepository())
            {
                Benefit ben = b.GetById(id);
                var model = new BenefitView();

                if (ben != null)
                {
                    model.benefitID = ben.benefitID;
                    model.Name = ben.Name;
                }
                return model;
            }
        }
        public void UpdateBenefit(BenefitView model)
        {
            using (var prepo = new BenefitRepository())
            {
                Benefit p = prepo.GetById(model.benefitID);
                if (p != null)
                {
                    p.Name = model.Name;
                    prepo.Update(p);
                }
            }
        }

    }
}
