using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Lis_Lib.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Lis_Lib.Models.Audiobook> Audiobooks { get; set; }

        public System.Data.Entity.DbSet<Lis_Lib.Models.Genre> Genres { get; set; }

        public System.Data.Entity.DbSet<Lis_Lib.Models.Author> Authors { get; set; }

        public System.Data.Entity.DbSet<Lis_Lib.Models.Narrator> Narrators { get; set; }

        public System.Data.Entity.DbSet<Lis_Lib.Models.Publisher> Publishers { get; set; }
    }
}