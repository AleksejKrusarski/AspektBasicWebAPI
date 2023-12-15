using Microsoft.EntityFrameworkCore;

namespace AspektBasicWebAPI.Models
{
    public class StructureContext : DbContext
    {
        public StructureContext (DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set;}
        public DbSet<Company> Companies { get; set;}
        public DbSet<Country> Countries { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //configure one-to-many relationship
            modelBuilder.Entity<Company>()
                .HasMany(c => c.Contacts)
                .WithOne(contact => contact.Company)
                .HasForeignKey(contact => contact.CompanyId);

            //configure one-to-many relationship
            modelBuilder.Entity<Country>()
                .HasMany(c => c.Contacts)
                .WithOne(contact => contact.Country)
                .HasForeignKey(contact => contact.CountryId);
        }
    }
}
