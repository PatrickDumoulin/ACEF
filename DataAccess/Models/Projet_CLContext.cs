﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

public partial class Projet_CLContext : DbContext
{
    public Projet_CLContext(DbContextOptions<Projet_CLContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }

    public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }

    public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }

    public virtual DbSet<Clients> Clients { get; set; }

    public virtual DbSet<ClientsAttachments> ClientsAttachments { get; set; }

    public virtual DbSet<ClientsIncomeTypes> ClientsIncomeTypes { get; set; }

    public virtual DbSet<ClientsNotes> ClientsNotes { get; set; }

    public virtual DbSet<CowMilkIngredientPriceListMonths> CowMilkIngredientPriceListMonths { get; set; }

    public virtual DbSet<CowMilkIngredientPriceLists> CowMilkIngredientPriceLists { get; set; }

    public virtual DbSet<CowMilkIngredients> CowMilkIngredients { get; set; }

    public virtual DbSet<CowMilkProductionTypes> CowMilkProductionTypes { get; set; }

    public virtual DbSet<CowMilkSalesGeneralInfoProductions> CowMilkSalesGeneralInfoProductions { get; set; }

    public virtual DbSet<CowMilkSalesGeneralInfos> CowMilkSalesGeneralInfos { get; set; }

    public virtual DbSet<EmployeePermissions> EmployeePermissions { get; set; }

    public virtual DbSet<Employees> Employees { get; set; }

    public virtual DbSet<Interventions> Interventions { get; set; }

    public virtual DbSet<InterventionsAttachments> InterventionsAttachments { get; set; }

    public virtual DbSet<InterventionsInterventionSolutions> InterventionsInterventionSolutions { get; set; }

    public virtual DbSet<InterventionsNotes> InterventionsNotes { get; set; }

    public virtual DbSet<MdBank> MdBank { get; set; }

    public virtual DbSet<MdEmploymentSituation> MdEmploymentSituation { get; set; }

    public virtual DbSet<MdFamilySituation> MdFamilySituation { get; set; }

    public virtual DbSet<MdGenderDenomination> MdGenderDenomination { get; set; }

    public virtual DbSet<MdHabitationType> MdHabitationType { get; set; }

    public virtual DbSet<MdIncomeType> MdIncomeType { get; set; }

    public virtual DbSet<MdInterventionSolution> MdInterventionSolution { get; set; }

    public virtual DbSet<MdInterventionStatusTypes> MdInterventionStatusTypes { get; set; }

    public virtual DbSet<MdInterventionType> MdInterventionType { get; set; }

    public virtual DbSet<MdLoanReason> MdLoanReason { get; set; }

    public virtual DbSet<MdMaritalStatus> MdMaritalStatus { get; set; }

    public virtual DbSet<MdReferenceSource> MdReferenceSource { get; set; }

    public virtual DbSet<MdScholarshipType> MdScholarshipType { get; set; }

    public virtual DbSet<MdSeminarThemes> MdSeminarThemes { get; set; }

    public virtual DbSet<ProductionTypeDepartments> ProductionTypeDepartments { get; set; }

    public virtual DbSet<ProductionTypes> ProductionTypes { get; set; }

    public virtual DbSet<Seminars> Seminars { get; set; }

    public virtual DbSet<SeminarsEmployees> SeminarsEmployees { get; set; }

    public virtual DbSet<SeminarsParticipants> SeminarsParticipants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AS");

        modelBuilder.Entity<AspNetRoleClaims>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.Property(e => e.RoleId).IsRequired();

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetRoles>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetUserClaims>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.Property(e => e.UserId).IsRequired();

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogins>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);
            entity.Property(e => e.UserId).IsRequired();

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserTokens>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUsers>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasDefaultValueSql("(N'')");
            entity.Property(e => e.IsFirstLogin)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasDefaultValueSql("(N'')");
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Role).WithMany(p => p.User)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRoles",
                    r => r.HasOne<AspNetRoles>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUsers>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<Clients>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clients__3214EC2745F912E5");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Birthdate).HasColumnType("datetime");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.LastModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.ZipCode).HasMaxLength(20);

            entity.HasOne(d => d.IdBankNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.IdBank)
                .HasConstraintName("FK_Bank");

            entity.HasOne(d => d.IdEmploymentSituationNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.IdEmploymentSituation)
                .HasConstraintName("FK_EmploymentSituation");

            entity.HasOne(d => d.IdFamilySituationNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.IdFamilySituation)
                .HasConstraintName("FK_FamilySituation");

            entity.HasOne(d => d.IdGenderDenominationNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.IdGenderDenomination)
                .HasConstraintName("FK_Gender");

            entity.HasOne(d => d.IdHabitationTypeNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.IdHabitationType)
                .HasConstraintName("FK_HomeType");

            entity.HasOne(d => d.IdMaritalStatusNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.IdMaritalStatus)
                .HasConstraintName("FK_MaritalStatus");

            entity.HasOne(d => d.IdScholarshipTypeNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.IdScholarshipType)
                .HasConstraintName("FK_Scholarship");
        });

        modelBuilder.Entity<ClientsAttachments>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clients___3214EC2728DC0100");

            entity.ToTable("Clients_Attachments");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FileName).HasMaxLength(255);

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.ClientsAttachments)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK_Clients_Attachments_Client");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.ClientsAttachments)
                .HasForeignKey(d => d.IdEmployee)
                .HasConstraintName("FK_Clients_Attachments_Employee");
        });

        modelBuilder.Entity<ClientsIncomeTypes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clients___3214EC27B1B50510");

            entity.ToTable("Clients_IncomeTypes");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.ClientsIncomeTypes)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK_Clients_IncomeTypes_Client");

            entity.HasOne(d => d.IdIncomeTypeNavigation).WithMany(p => p.ClientsIncomeTypes)
                .HasForeignKey(d => d.IdIncomeType)
                .HasConstraintName("FK_Clients_IncomeTypes_IncomeType");
        });

        modelBuilder.Entity<ClientsNotes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clients___3214EC27CBCE2B4A");

            entity.ToTable("Clients_Notes");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.ClientsNotes)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK_Clients_Notes_Client");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.ClientsNotes)
                .HasForeignKey(d => d.IdEmployee)
                .HasConstraintName("FK_Clients_Notes_Employee");
        });

        modelBuilder.Entity<CowMilkIngredientPriceListMonths>(entity =>
        {
            entity.HasIndex(e => e.IdCowMilkIngredient, "IX_IdCowMilkIngredient");

            entity.HasIndex(e => new { e.IdCowMilkIngredientPriceList, e.EffectiveMonthDate }, "IX_IdCowMilkIngredientPriceListMonthDate");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [seqCowMilkIngredientPriceListMonth])")
                .HasColumnName("ID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Dcpercentage)
                .HasDefaultValueSql("((100))")
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("DCPercentage");
            entity.Property(e => e.EffectiveMonthDate).HasColumnType("datetime");
            entity.Property(e => e.LastModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdCowMilkIngredientNavigation).WithMany(p => p.CowMilkIngredientPriceListMonths)
                .HasForeignKey(d => d.IdCowMilkIngredient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CowMilkIngredientPriceListMonths_CowMilkIngredients");

            entity.HasOne(d => d.IdCowMilkIngredientPriceListNavigation).WithMany(p => p.CowMilkIngredientPriceListMonths)
                .HasForeignKey(d => d.IdCowMilkIngredientPriceList)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CowMilkIngredientPriceListMonths_CowMilkIngredientPriceLists");
        });

        modelBuilder.Entity<CowMilkIngredientPriceLists>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LastModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<CowMilkIngredients>(entity =>
        {
            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[seqCowMilkIngredients])")
                .HasColumnName("ID");
            entity.Property(e => e.DefaultDcperTon)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("DefaultDCPerTon");
            entity.Property(e => e.DisplayName)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.UnitDisplayName)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasOne(d => d.IdProductionTypeDeparmentNavigation).WithMany(p => p.CowMilkIngredients)
                .HasForeignKey(d => d.IdProductionTypeDeparment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CowMilkIngredients_ProductionTypeDepartments");
        });

        modelBuilder.Entity<CowMilkProductionTypes>(entity =>
        {
            entity.HasIndex(e => e.IdCowMilkIngredientPriceList, "IX_IdCowMilkIngredientPriceList");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.DisplayName)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.IdCowMilkIngredientPriceListNavigation).WithMany(p => p.CowMilkProductionTypes)
                .HasForeignKey(d => d.IdCowMilkIngredientPriceList)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CowMilkProductionTypes_CowMilkIngredientPriceLists");
        });

        modelBuilder.Entity<CowMilkSalesGeneralInfoProductions>(entity =>
        {
            entity.HasIndex(e => e.IdCowMilkProductionType, "IX_IdCowMilkProductionType");

            entity.HasIndex(e => e.IdCowMilkSalesGeneralInfo, "IX_IdCowMilkSalesGeneralInfo");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [seqCowMilkSalesGeneralInfoProduction])")
                .HasColumnName("ID");
            entity.Property(e => e.ProductionBonus).HasColumnType("decimal(12, 2)");

            entity.HasOne(d => d.IdCowMilkProductionTypeNavigation).WithMany(p => p.CowMilkSalesGeneralInfoProductions)
                .HasForeignKey(d => d.IdCowMilkProductionType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CowMilkSalesGeneralInfoProductions_CowMilkProductionTypes");

            entity.HasOne(d => d.IdCowMilkSalesGeneralInfoNavigation).WithMany(p => p.CowMilkSalesGeneralInfoProductions)
                .HasForeignKey(d => d.IdCowMilkSalesGeneralInfo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CowMilkSalesGeneralInfoProductions_CowMilkSalesGeneralInfos");
        });

        modelBuilder.Entity<CowMilkSalesGeneralInfos>(entity =>
        {
            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [seqCowMilkSalesGeneralInfo])")
                .HasColumnName("ID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EffectiveMonthDate).HasColumnType("datetime");
            entity.Property(e => e.FatOnNonFatRatio).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.FatPremium).HasColumnType("decimal(12, 4)");
            entity.Property(e => e.FatPrice).HasColumnType("decimal(12, 4)");
            entity.Property(e => e.LactosePrice).HasColumnType("decimal(12, 4)");
            entity.Property(e => e.LastModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProteinPrice).HasColumnType("decimal(12, 4)");
        });

        modelBuilder.Entity<EmployeePermissions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC27AC693430");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AllowSu).HasColumnName("AllowSU");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeePermissions)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__EmployeeP__Emplo__345EC57D");
        });

        modelBuilder.Entity<Employees>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC2721C5503C");

            entity.HasIndex(e => e.UserName, "UQ__Employee__C9F28456D83DC9B1").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValueSql("((1))");
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.LastLoginDate).HasColumnType("datetime");
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(255);
        });

        modelBuilder.Entity<Interventions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Interven__3214EC274C0A8048");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateIntervention).HasColumnType("datetime");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Interventions)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK__Intervent__IdCli__5006DFF2");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.Interventions)
                .HasForeignKey(d => d.IdEmployee)
                .HasConstraintName("FK__Intervent__IdEmp__4F12BBB9");

            entity.HasOne(d => d.IdInterventionTypeNavigation).WithMany(p => p.Interventions)
                .HasForeignKey(d => d.IdInterventionType)
                .HasConstraintName("FK__Intervent__IdInt__52E34C9D");

            entity.HasOne(d => d.IdLoanReasonNavigation).WithMany(p => p.Interventions)
                .HasForeignKey(d => d.IdLoanReason)
                .HasConstraintName("FK__Intervent__IdLoa__53D770D6");

            entity.HasOne(d => d.IdReferenceTypeNavigation).WithMany(p => p.Interventions)
                .HasForeignKey(d => d.IdReferenceType)
                .HasConstraintName("FK__Intervent__IdRef__50FB042B");

            entity.HasOne(d => d.IdStatusTypeNavigation).WithMany(p => p.Interventions)
                .HasForeignKey(d => d.IdStatusType)
                .HasConstraintName("FK_Intervent_IdSta_51EF2864");
        });

        modelBuilder.Entity<InterventionsAttachments>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Interven__3214EC27A7A99609");

            entity.ToTable("Interventions_Attachments");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FileName).HasMaxLength(255);

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.InterventionsAttachments)
                .HasForeignKey(d => d.IdEmployee)
                .HasConstraintName("FK__Intervent__IdEmp__5E54FF49");

            entity.HasOne(d => d.IdInterventionNavigation).WithMany(p => p.InterventionsAttachments)
                .HasForeignKey(d => d.IdIntervention)
                .HasConstraintName("FK__Intervent__IdInt__5D60DB10");
        });

        modelBuilder.Entity<InterventionsInterventionSolutions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Interven__3214EC27C6DE253E");

            entity.ToTable("Interventions_InterventionSolutions");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.IdInterventionNavigation).WithMany(p => p.InterventionsInterventionSolutions)
                .HasForeignKey(d => d.IdIntervention)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Intervent__IdInt__6225902D");

            entity.HasOne(d => d.IdInterventionSolutionNavigation).WithMany(p => p.InterventionsInterventionSolutions)
                .HasForeignKey(d => d.IdInterventionSolution)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Intervent__IdInt__6319B466");
        });

        modelBuilder.Entity<InterventionsNotes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Interven__3214EC27DBCB752F");

            entity.ToTable("Interventions_Notes");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.InterventionsNotes)
                .HasForeignKey(d => d.IdEmployee)
                .HasConstraintName("FK__Intervent__IdEmp__589C25F3");

            entity.HasOne(d => d.IdInterventionNavigation).WithMany(p => p.InterventionsNotes)
                .HasForeignKey(d => d.IdIntervention)
                .HasConstraintName("FK__Intervent__IdInt__57A801BA");
        });

        modelBuilder.Entity<MdBank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MdBank__3214EC279312F2A5");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[seqMasterData])")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<MdEmploymentSituation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MdEmploy__3214EC2740F2CEF4");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[seqMasterData])")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<MdFamilySituation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MdFamily__3214EC2783C35569");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[seqMasterData])")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<MdGenderDenomination>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MdGender__3214EC277DB1AE4F");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[seqMasterData])")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<MdHabitationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MdHabita__3214EC27379BE18A");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[seqMasterData])")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<MdIncomeType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MdIncome__3214EC27F30190D5");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[seqMasterData])")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<MdInterventionSolution>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MdInterv__3214EC277273918E");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[seqMasterData])")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<MdInterventionStatusTypes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MdInterv__3214EC27F8D9CDF6");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[seqMasterData])")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<MdInterventionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MdInterv__3214EC27ACFACB88");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[seqMasterData])")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<MdLoanReason>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MdLoanRe__3214EC27AD519D6E");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[seqMasterData])")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<MdMaritalStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MdMarita__3214EC270E4D72BA");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[seqMasterData])")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<MdReferenceSource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MdRefere__3214EC273E0E7858");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[seqMasterData])")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<MdScholarshipType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MdScolar__3214EC270859C866");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[seqMasterData])")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<MdSeminarThemes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MdSemina__3214EC27BDA44CB0");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(NEXT VALUE FOR [dbo].[seqMasterData])")
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<ProductionTypeDepartments>(entity =>
        {
            entity.HasIndex(e => e.IdProductionType, "IX_IdProductionType");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AmoutUnitDisplayName).HasMaxLength(200);
            entity.Property(e => e.DisplayName)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.ShortDisplayName).HasMaxLength(200);

            entity.HasOne(d => d.IdProductionTypeNavigation).WithMany(p => p.ProductionTypeDepartments)
                .HasForeignKey(d => d.IdProductionType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductionTypeDepartments_ProductionTypes");
        });

        modelBuilder.Entity<ProductionTypes>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Seminars>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Seminars__3214EC276F0378DD");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateSeminar).HasColumnType("datetime");

            entity.HasOne(d => d.IdSeminarThemeNavigation).WithMany(p => p.Seminars)
                .HasForeignKey(d => d.IdSeminarTheme)
                .HasConstraintName("FK__Seminars__IdSemi__67DE6983");
        });

        modelBuilder.Entity<SeminarsEmployees>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Seminars__3214EC27C43B5C71");

            entity.ToTable("Seminars_Employees");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.SeminarsEmployees)
                .HasForeignKey(d => d.IdEmployee)
                .HasConstraintName("FK__Seminars___IdEmp__6CA31EA0");

            entity.HasOne(d => d.IdSeminarNavigation).WithMany(p => p.SeminarsEmployees)
                .HasForeignKey(d => d.IdSeminar)
                .HasConstraintName("FK__Seminars___IdSem__6BAEFA67");
        });

        modelBuilder.Entity<SeminarsParticipants>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Seminars__3214EC27C311BA95");

            entity.ToTable("Seminars_Participants");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FirstName).HasMaxLength(200);
            entity.Property(e => e.LastName).HasMaxLength(200);

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.SeminarsParticipants)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK__Seminars___IdCli__7167D3BD");

            entity.HasOne(d => d.IdSeminarNavigation).WithMany(p => p.SeminarsParticipants)
                .HasForeignKey(d => d.IdSeminar)
                .HasConstraintName("FK__Seminars___IdSem__7073AF84");
        });
        modelBuilder.HasSequence("Clients_IncomeTypes_Seq");
        modelBuilder.HasSequence("Clients_Seq");
        modelBuilder.HasSequence("EmployeePermissions_Seq");
        modelBuilder.HasSequence("Employees_Seq");
        modelBuilder.HasSequence("Interventions_Attachments_Seq");
        modelBuilder.HasSequence("Interventions_InterventionSolutions_Seq");
        modelBuilder.HasSequence("Seminars_Employees_Seq");
        modelBuilder.HasSequence("Seminars_Participants_Seq");
        modelBuilder.HasSequence("Seminars_Seq");
        modelBuilder.HasSequence("seqCowMilkIngredientPriceListMonth");
        modelBuilder.HasSequence("seqCowMilkIngredients").StartsAt(12L);
        modelBuilder.HasSequence("seqCowMilkSalesGeneralInfo").StartsAt(100L);
        modelBuilder.HasSequence("seqCowMilkSalesGeneralInfoProduction").StartsAt(100L);
        modelBuilder.HasSequence("seqMasterData");

        OnModelCreatingGeneratedProcedures(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}