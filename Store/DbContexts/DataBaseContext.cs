﻿using Microsoft.EntityFrameworkCore;
using StoreServiceAPI.Entities;

namespace StoreServiceAPI.DbContexts
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Store> Stores { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
