using BasicWebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.Dal;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Contact>  Contacts { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Country> Countries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>()
            .HasOne(c => c.Company)
            .WithMany(c => c.Contacts)
            .HasForeignKey(c => c.CompanyId);

        modelBuilder.Entity<Contact>()
            .HasOne(c => c.Country)
            .WithMany(c => c.Contacts)
            .HasForeignKey(c => c.CountryId);
    }

}