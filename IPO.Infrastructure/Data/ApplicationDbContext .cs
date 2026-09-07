using IPO.Domain.Entities;
using IPO.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyFinancial> CompanyFinancials { get; set; }
        public DbSet<IPODocument> IPODocuments { get; set; }
        public DbSet<IPOs> IPOs { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<Registrar> Registrars { get; set; }
        public DbSet<Allotment> Allotments { get; set; }
        public DbSet<IPOApplication> IPOApplications { get; set; }
        public DbSet<Watchlist> Watchlists { get; set; }
        public DbSet<Otp> Otps { get; set; }

    }
}
