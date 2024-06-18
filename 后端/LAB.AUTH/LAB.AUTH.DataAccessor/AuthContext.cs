using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace LAB.AUTH.DataAccessor
{
    public class AuthContext:DbContext
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
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<CQIE.Books.Models.Book>().ToTable("TB_Book");
            //modelBuilder.Entity<CQIE.Books.Models.BookInventory>().ToTable("TB_BookInventory");
            modelBuilder.Entity<LAB.AUTH.Models.SysUser>(entity =>
            {
                entity.HasKey(c => c.Id);
            });

            modelBuilder.Entity<LAB.AUTH.Models.Roles>(entity =>
            {
                entity.HasKey(c => c.Id);
            });

            modelBuilder.Entity<LAB.AUTH.Models.UserRoles>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.HasOne(c => c.sysUser).WithMany(c => c.user_roles).HasForeignKey(c => c.UID);
                entity.HasOne(c => c.Role).WithMany(c => c.user_roles).HasForeignKey(c => c.RID);
            });

            modelBuilder.Entity<LAB.AUTH.Models.Semesters>(entity =>
            {
                entity.HasKey(c => c.Id);
            });

            modelBuilder.Entity<LAB.AUTH.Models.Laboratories>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.HasOne(c => c.Academy).WithMany(c => c.laboratories).HasForeignKey(c => c.AcademyId);
            });

            modelBuilder.Entity<LAB.AUTH.Models.LabInclidentHanding>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.HasOne(c => c.dailySafetyChecks).WithMany(c => c.labInclidentHanding).HasForeignKey(c => c.DLabID);
            });

            modelBuilder.Entity<LAB.AUTH.Models.LabEquipmentRepairs>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.HasOne(c => c.dailySafetyChecks).WithMany(c => c.labEquipmentRepairs).HasForeignKey(c => c.DLabID);
            });

            modelBuilder.Entity<LAB.AUTH.Models.LabAssignments>(entity =>
            {
                entity.HasKey(c => c.Id);



                entity.HasOne(c => c.Laboratories).WithMany(c => c.LabAssignments).HasForeignKey(c => c.LabID);

                entity.HasOne(c => c.SysUser).WithMany(c => c.LabAssignments).HasForeignKey(c => c.UserID);
            });

            modelBuilder.Entity<LAB.AUTH.Models.DailySafetyCheck>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.HasOne(c => c.SysUser).WithMany(c => c.DailySafetyCheck).HasForeignKey(c => c.UID);
                entity.HasOne(c => c.Semesters).WithMany(c => c.dailySafetyChecks).HasForeignKey(c => c.SemesterID);
                entity.HasOne(c => c.Laboratories).WithMany(c => c.dailySafetyChecks).HasForeignKey(c => c.LabID);
            });

            modelBuilder.Entity<LAB.AUTH.Models.Academy>(entity =>
            {
                entity.HasKey(c => c.Id);


            });

            base.OnModelCreating(modelBuilder);
        }

        //public Microsoft.EntityFrameworkCore.DbSet<CQIE.Books.Models.Book> Books { get; set; }
        //public Microsoft.EntityFrameworkCore.DbSet<CQIE.Books.Models.BookInventory> BooksInventory { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<LAB.AUTH.Models.Academy> Academys { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<LAB.AUTH.Models.DailySafetyCheck> DailySafetyChecks { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<LAB.AUTH.Models.LabAssignments> LabAssignments { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<LAB.AUTH.Models.LabEquipmentRepairs> LabEquipmentRepairs { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<LAB.AUTH.Models.LabInclidentHanding> labInclidentHandings { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<LAB.AUTH.Models.Laboratories> Laboratories { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<LAB.AUTH.Models.Roles> Roles { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<LAB.AUTH.Models.Semesters> Semesters { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<LAB.AUTH.Models.SysUser> SysUsers { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<LAB.AUTH.Models.UserRoles> UserRoles { get; set; }
    }
}
