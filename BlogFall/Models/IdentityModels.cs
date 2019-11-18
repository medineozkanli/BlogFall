using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BlogFall.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser//Identitinin senin için kullancı clası oluşturmak için miras aldık .Bunlara ek propert ekleyebiliriz ad soyad gibi.
    {
        public ApplicationUser()
        {
            //Varsayılan değer aksi belirtilmediği sürece true olacak.
            IsEnabled = true;
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        [StringLength(100)]
        public string Photo { get; set; }

        public bool IsEnabled { get; set; }  //Aktif olup olmaması ekleyip update database yaptık.
        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>//Application user identityden miras alır.Ekstra kendi tablolarımızı ekleyebiliriz.
    {
        public ApplicationDbContext()
            : base("name=ApplicationDbContext", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();//cascade delete kaldırıyoruz.Bu sayade default olarak bağlı tablolar için çok ilişkilerde otomatik silmeyi kaldır.

            //her yorumun zorunlu olarak bir yazısı vardır.
            // o yazının da yorumları vardır.
            //yazı silindiğinde yorumlarıda otomatik olarak sil.
            modelBuilder.Entity<Comment>()
                .HasRequired(x => x.Post)
                .WithMany(x => x.Comments)
                .WillCascadeOnDelete();
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}