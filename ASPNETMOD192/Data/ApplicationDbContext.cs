using ASPNETMOD192.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASPNETMOD192.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; } = default!;

        public DbSet<Staff> Staff {  get; set; } = default!;

        public DbSet<Appointment> Appointments { get; set; } = default!;

        public DbSet<MedicalOperation> MedicalOperation { get; set; } = default!;
    }



}
