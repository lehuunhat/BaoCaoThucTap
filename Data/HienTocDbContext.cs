using HienTangToc.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace HienTangToc.Data
{
    public class HientocDbcontext : DbContext
    {
        public HientocDbcontext(DbContextOptions<HientocDbcontext> options) : base(options) { }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<TocModel> TocModels { get; set; } 
        public DbSet<NguoihienModel> NguoihienModels { get; set; }
        public DbSet<NguoimuonModel> NguoimuonModels { get; set; }
        public DbSet<SalonModel> SalonModels { get; set; }
        public DbSet<HSalonModel> HSalonModels { get; set; }
        public DbSet<MSalonModel> MSalonModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //người hiến vs salon
            modelBuilder.Entity<HSalonModel>()
                .HasOne(hs => hs.NguoihienModel)
                .WithMany(nh => nh.HSalonModels)
                .HasForeignKey(hs => hs.Idnh);

            modelBuilder.Entity<HSalonModel>()
                .HasOne(hs => hs.SalonModel)
                .WithMany(s => s.HSalonModels)
                .HasForeignKey(hs => hs.Idsl);
            //người mượn vs salon
            modelBuilder.Entity<MSalonModel>()
                .HasOne(ms => ms.NguoimuonModel)
                .WithMany(nm => nm.MSalonModels)
                .HasForeignKey(ms => ms.Idnm);

            modelBuilder.Entity<MSalonModel>()
                .HasOne(ms => ms.SalonModel)
                .WithMany(s => s.MSalonModels)
                .HasForeignKey(ms => ms.Idsl);
        }
    }
}
