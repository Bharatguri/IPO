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
        public DbSet<Domain.Entities.IPO> IPOs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<IPOApplication> IPOApplications { get; set; }
        public DbSet<Watchlist> Watchlists { get; set; }
        public DbSet<Otp> Otps { get; set; }

    }
}
