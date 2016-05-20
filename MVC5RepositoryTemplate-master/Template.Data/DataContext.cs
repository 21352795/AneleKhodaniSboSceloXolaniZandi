using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Template.Data
{
        public class DataContext : IdentityDbContext, IDbContext
       {
        
        public DataContext()
            : base("conn")
        {
       
        }

        //#region Properties
        public DbSet<ClientApplication> ClientApplications { get; set; }
        public DbSet<ArchivedClientApplication> ArchivedClientApplications { get; set; }
        public DbSet<ClientApplicationBeneficiary> ClientApplicationBeneficiaries { get; set; }
        public DbSet<ClientApplicationDocument> ClientApplicationDocuments { get; set; }
        //---------------
        public DbSet<PolicyHolder> PolicyHolders { get; set; }
        public DbSet<PolicyBeneficiary> PolicyBeneficiaries { get; set; }
        public DbSet<PolicyDocument> PolicyDocuments { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageBenefit> PackageBenefits { get; set; }
        public DbSet<Benefit> Benefits { get; set; }
        //
        public DbSet<ProfileActivityLog> ProfileActivityLogs { get; set; }
        public DbSet<ProfileApplicationBeneficiary> ProfileApplicationBeneficiaries { get; set; }
        public DbSet<ProfileApplicationDocuments> ProfileApplicationDocuments { get; set; }


        //#endregion

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<IdentityUser>().ToTable("AspNetUsers");

            EntityTypeConfiguration<ApplicationUser> table =
                modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");

            table.Property((ApplicationUser u) => u.UserName).IsRequired();

            modelBuilder.Entity<ApplicationUser>().HasMany<IdentityUserRole>((ApplicationUser u) => u.Roles);
            modelBuilder.Entity<IdentityUserRole>().HasKey((IdentityUserRole r) =>
                new { UserId = r.UserId, RoleId = r.RoleId }).ToTable("AspNetUserRoles");

            EntityTypeConfiguration<IdentityUserLogin> entityTypeConfiguration =
                modelBuilder.Entity<IdentityUserLogin>().HasKey((IdentityUserLogin l) =>
                    new
                    {
                        UserId = l.UserId,
                        LoginProvider = l.LoginProvider,
                        ProviderKey
                            = l.ProviderKey
                    }).ToTable("AspNetUserLogins");

            modelBuilder.Entity<IdentityRole>().ToTable("AspNetRoles");

            EntityTypeConfiguration<ApplicationRole> entityTypeConfiguration1 = modelBuilder.Entity<ApplicationRole>().ToTable("AspNetRoles");

            entityTypeConfiguration1.Property((ApplicationRole r) => r.Name).IsRequired();

            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Rental>().ToTable("Rental");
            modelBuilder.Entity<RentalItem>().ToTable("RentalItem");
            modelBuilder.Entity<Payment>().ToTable("Payment");
            modelBuilder.Entity<RentalPayment>().ToTable("RentalPayment");
          
            modelBuilder.Entity<PremiumPayment>().ToTable("PremiumPayment");
            modelBuilder.Entity<ServiceRepresentative>().ToTable("ServiceRepresentative");
            modelBuilder.Entity<ArchiveServiceRepresentative>().ToTable("ArchiveServiceRepresentative");
        }        
    }
}


