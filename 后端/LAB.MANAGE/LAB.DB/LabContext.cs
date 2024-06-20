using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;
namespace LAB.DB
{
    public class LabContext : DbContext
    {

        //public LabContext()
        //{

        //}
        //// 数据库配置文件
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=DESKTOP-VCSEMTQ\\KKKMSSQLSERVER;Database=LABMANAGE;Trusted_Connection=True;");


        //    base.OnConfiguring(optionsBuilder);
        //}
        public LabContext(DbContextOptions<LabContext> options) : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<CQIE.Books.Models.Book>().ToTable("TB_Book");
            //modelBuilder.Entity<CQIE.Books.Models.BookInventory>().ToTable("TB_BookInventory");
            modelBuilder.Entity<LAB.MODEL.SysUser>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.HasOne(c => c.Academy).WithMany(c => c.sysUsers).HasForeignKey(c => c.CID);
            });

            modelBuilder.Entity<LAB.MODEL.Roles>(entity =>
            {
                entity.HasKey(c => c.Id);
            });

            modelBuilder.Entity<LAB.MODEL.UserRoles>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.HasOne(c => c.sysUser).WithMany(c => c.user_roles).HasForeignKey(c => c.UID);
                entity.HasOne(c => c.Role).WithMany(c => c.user_roles).HasForeignKey(c => c.RID);
            });

            modelBuilder.Entity<LAB.MODEL.Semesters>(entity =>
            {
                entity.HasKey(c => c.Id);
            });

            modelBuilder.Entity<LAB.MODEL.Laboratories>(entity =>
            {
                entity.HasKey(c => c.Id);
                //entity.HasOne(c => c.Academy).WithMany(c => c.laboratories).HasForeignKey(c => c.AcademyId);
            });

            modelBuilder.Entity<LAB.MODEL.LabInclidentHanding>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.HasOne(c => c.dailySafetyChecks).WithMany(c => c.labInclidentHanding).HasForeignKey(c => c.DLabID);
            });

            modelBuilder.Entity<LAB.MODEL.LabEquipmentRepairs>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.HasOne(c => c.dailySafetyChecks).WithMany(c => c.labEquipmentRepairs).HasForeignKey(c => c.DLabID);
            });

            modelBuilder.Entity<LAB.MODEL.LabAssignments>(entity =>
            {
                entity.HasKey(c => c.Id);



                entity.HasOne(c => c.Laboratories).WithMany(c => c.LabAssignments).HasForeignKey(c => c.LabID);

                entity.HasOne(c => c.SysUser).WithMany(c => c.LabAssignments).HasForeignKey(c => c.UserID);
            });

            modelBuilder.Entity<LAB.MODEL.DailySafetyCheck>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.HasOne(c => c.SysUser).WithMany(c => c.DailySafetyCheck).HasForeignKey(c => c.UID);
                entity.HasOne(c => c.Semesters).WithMany(c => c.dailySafetyChecks).HasForeignKey(c => c.SemesterID);
                entity.HasOne(c => c.Laboratories).WithMany(c => c.dailySafetyChecks).HasForeignKey(c => c.LabID);
            });

            modelBuilder.Entity<LAB.MODEL.Academy>(entity =>
            {
                entity.HasKey(c => c.Id);
            });

            modelBuilder.Entity<LAB.MODEL.SingleBuilding>(entity =>
            {
                entity.HasKey(c => c.Id);
            });
            modelBuilder.Entity<LAB.MODEL.Floor>(entity =>
            {
                entity.HasKey(c => c.Id);
            });

            base.OnModelCreating(modelBuilder);
        }

        //public Microsoft.EntityFrameworkCore.DbSet<CQIE.Books.Models.Book> Books { get; set; }
        //public Microsoft.EntityFrameworkCore.DbSet<CQIE.Books.Models.BookInventory> BooksInventory { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<LAB.MODEL.Academy> Academys { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<LAB.MODEL.DailySafetyCheck> DailySafetyChecks { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<LAB.MODEL.LabAssignments> LabAssignments { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<LAB.MODEL.LabEquipmentRepairs> LabEquipmentRepairs { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<LAB.MODEL.LabInclidentHanding> labInclidentHandings { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<LAB.MODEL.Laboratories> Laboratories { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<LAB.MODEL.Roles> Roles { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<LAB.MODEL.Semesters> Semesters { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<LAB.MODEL.SysUser> SysUsers {  get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<LAB.MODEL.UserRoles> UserRoles { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<LAB.MODEL.Floor> Floor { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<LAB.MODEL.SingleBuilding> SingleBuilding { get; set; }
    }
}
