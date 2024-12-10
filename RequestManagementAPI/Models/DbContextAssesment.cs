using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RequestManagementAPI.Models
{
    public partial class DbContextAssesment : DbContext
    {
        public DbContextAssesment()
        {
        }

        public DbContextAssesment(DbContextOptions<DbContextAssesment> options)
            : base(options)
        {
        }

        public virtual DbSet<MstCategoryRequestManagement> MstCategoryRequestManagements { get; set; } = null!;
        public virtual DbSet<MstDivision> MstDivisions { get; set; } = null!;
        public virtual DbSet<MstOrganization> MstOrganizations { get; set; } = null!;
        public virtual DbSet<MstPermission> MstPermissions { get; set; } = null!;
        public virtual DbSet<MstRole> MstRoles { get; set; } = null!;
        public virtual DbSet<MstStatusRequestManagement> MstStatusRequestManagements { get; set; } = null!;
        public virtual DbSet<MstSubCategoryRequestManagement> MstSubCategoryRequestManagements { get; set; } = null!;
        public virtual DbSet<MstTypeRequestManagement> MstTypeRequestManagements { get; set; } = null!;
        public virtual DbSet<MstUser> MstUsers { get; set; } = null!;
        public virtual DbSet<TrxExpensesRequestsManagement> TrxExpensesRequestsManagements { get; set; } = null!;
        public virtual DbSet<TrxRequestsManagement> TrxRequestsManagements { get; set; } = null!;
        public virtual DbSet<VwExpensesRequestsManagement> VwExpensesRequestsManagements { get; set; } = null!;
        public virtual DbSet<VwRequestsManagement> VwRequestsManagements { get; set; } = null!;
        public virtual DbSet<VwUser> VwUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=GapTechIng;Database=assesment;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MstCategoryRequestManagement>(entity =>
            {
                entity.ToTable("mstCategoryRequestManagement");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MstDivision>(entity =>
            {
                entity.ToTable("mstDivision");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Division)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MstOrganization>(entity =>
            {
                entity.ToTable("mstOrganization");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Organization)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MstPermission>(entity =>
            {
                entity.ToTable("mstPermission");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Permission)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MstRole>(entity =>
            {
                entity.ToTable("mstRole");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ParentRoleId).HasColumnName("ParentRoleID");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MstStatusRequestManagement>(entity =>
            {
                entity.ToTable("mstStatusRequestManagement");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StatusName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MstSubCategoryRequestManagement>(entity =>
            {
                entity.ToTable("mstSubCategoryRequestManagement");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.SubCategoryName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MstTypeRequestManagement>(entity =>
            {
                entity.ToTable("mstTypeRequestManagement");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MstUser>(entity =>
            {
                entity.ToTable("mstUser");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Password)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TrxExpensesRequestsManagement>(entity =>
            {
                entity.ToTable("trxExpensesRequestsManagement");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TrxRequestsManagement>(entity =>
            {
                entity.ToTable("trxRequestsManagement");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Applicant)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Category)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Expenses).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Number)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Owner)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SubCategory)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwExpensesRequestsManagement>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwExpensesRequestsManagement");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ReqNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TypeName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwRequestsManagement>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwRequestsManagement");

                entity.Property(e => e.Applicant)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Category)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedByUser)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Division)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Expenses).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Number)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Organization)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Owner)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ParentRoleIdcreatedBy).HasColumnName("ParentRoleIDCreatedBy");

                entity.Property(e => e.ParentRoleIdupdatedBy).HasColumnName("ParentRoleIDUpdatedBy");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StatusName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SubCategory)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SubCategoryName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwUsers");

                entity.Property(e => e.Division)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Organization)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ParentRoleId).HasColumnName("ParentRoleID");

                entity.Property(e => e.Password)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
