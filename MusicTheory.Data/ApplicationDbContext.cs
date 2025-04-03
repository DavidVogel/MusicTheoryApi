using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicTheory.Domain; // Namespace where ApplicationUser is defined

namespace MusicTheory.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    // This context will manage Identity tables (AspNetUsers, AspNetRoles, etc.) as well as any additional entities. The call to base.OnModelCreating(builder) ensures the default Identity schema is applied. By using IdentityDbContext<TUser>, you automatically get all the necessary mappings for users, roles, and their relationships.
}
