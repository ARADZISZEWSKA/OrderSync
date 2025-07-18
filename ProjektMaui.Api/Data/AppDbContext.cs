﻿using Microsoft.EntityFrameworkCore;
using ProjektMaui.Api.Models;

namespace ProjektMaui.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<Attachment> Attachments { get; set; }

    }
}
