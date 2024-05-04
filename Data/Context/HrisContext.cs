using System;
using System.Collections.Generic;
using ATEC_API.DomainModels.HRISDomainModel;
using Microsoft.EntityFrameworkCore;

namespace ATEC_API.Data.Context;

public partial class HrisContext : DbContext
{
    public HrisContext()
    {
    }

    public HrisContext(DbContextOptions<HrisContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblCert> TblCerts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCert>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_Cert");

            entity.Property(e => e.CertQualDate)
                .HasColumnType("date")
                .HasColumnName("Cert_Qual_Date");
            entity.Property(e => e.CertRequalDate)
                .HasColumnType("date")
                .HasColumnName("Cert_Requal_Date");
            entity.Property(e => e.Certification).HasColumnName("certification");
            entity.Property(e => e.ContractQualDate)
                .HasColumnType("date")
                .HasColumnName("Contract_Qual_Date");
            entity.Property(e => e.ContractRequalDate)
                .HasColumnType("date")
                .HasColumnName("Contract_Requal_Date");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(50)
                .HasColumnName("CustomerID");
            entity.Property(e => e.DateHired).HasColumnType("date");
            entity.Property(e => e.Department).HasMaxLength(100);
            entity.Property(e => e.Destination).HasMaxLength(100);
            entity.Property(e => e.EmpNo)
                .HasMaxLength(100)
                .HasColumnName("Emp_No");
            entity.Property(e => e.Employer).HasMaxLength(100);
            entity.Property(e => e.Exam).HasMaxLength(100);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.IntialQualDate).HasMaxLength(100);
            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Operation).HasMaxLength(100);
            entity.Property(e => e.QualDate)
                .HasColumnType("date")
                .HasColumnName("Qual_Date");
            entity.Property(e => e.Quality).HasMaxLength(100);
            entity.Property(e => e.RecipeDescription).HasMaxLength(100);
            entity.Property(e => e.RecipeId)
                .HasMaxLength(100)
                .HasColumnName("RecipeID");
            entity.Property(e => e.RequalDate)
                .HasColumnType("date")
                .HasColumnName("Requal_Date");
            entity.Property(e => e.Superior).HasMaxLength(100);
            entity.Property(e => e.Trainer).HasMaxLength(100);
            entity.Property(e => e.TrainingFee)
                .HasMaxLength(100)
                .HasColumnName("Training_fee");
            entity.Property(e => e.TrainingTitle).HasColumnName("Training_Title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
