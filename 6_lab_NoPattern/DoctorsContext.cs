using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace _6_lab_NoPattern;

public partial class DoctorsContext : DbContext
{
    public DoctorsContext()
    {
    }

    public DoctorsContext(DbContextOptions<DoctorsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<DoctorSpeciality> DoctorSpecialities { get; set; }

    public virtual DbSet<GeneralPage> GeneralPages { get; set; }

    public virtual DbSet<Hospital> Hospitals { get; set; }

    public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Requisite> Requisites { get; set; }

    public virtual DbSet<Speciality> Specialities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host = localhost; Username = postgres; Password = 5dartyr5; Database = doctors");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("addresses_pkey");

            entity.ToTable("addresses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address1)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.GeneralPageId).HasColumnName("general_page_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.GeneralPage).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.GeneralPageId)
                .HasConstraintName("addresses_general_page_id_foreign");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("departments_pkey");

            entity.ToTable("departments");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.HospitalId).HasColumnName("hospital_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Hospital).WithMany(p => p.Departments)
                .HasForeignKey(d => d.HospitalId)
                .HasConstraintName("departments_hospital_id_foreign");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Department)
                .HasForeignKey<Department>(d => d.Id)
                .HasConstraintName("departments_id_foreign");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("doctors_pkey");

            entity.ToTable("doctors");

            entity.HasIndex(e => e.PassportDetails, "doctors_passport_details_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DateBirth).HasColumnName("date_birth");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.PassportDetails)
                .HasMaxLength(11)
                .HasColumnName("passport_details");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<DoctorSpeciality>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("doctor_specialities_pkey");

            entity.ToTable("doctor_specialities");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DateEnd).HasColumnName("date_end");
            entity.Property(e => e.DateStart).HasColumnName("date_start");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.SpecialityId).HasColumnName("speciality_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Department).WithMany(p => p.DoctorSpecialities)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("doctor_specialities_department_id_foreign");

            entity.HasOne(d => d.Doctor).WithMany(p => p.DoctorSpecialities)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("doctor_specialities_doctor_id_foreign");

            entity.HasOne(d => d.Speciality).WithMany(p => p.DoctorSpecialities)
                .HasForeignKey(d => d.SpecialityId)
                .HasConstraintName("doctor_specialities_speciality_id_foreign");
        });

        modelBuilder.Entity<GeneralPage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("general_pages_pkey");

            entity.ToTable("general_pages");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Title)
                .HasMaxLength(250)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Website)
                .HasMaxLength(30)
                .HasColumnName("website");
        });

        modelBuilder.Entity<Hospital>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hospitals_pkey");

            entity.ToTable("hospitals");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.RegionId).HasColumnName("region_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Hospital)
                .HasForeignKey<Hospital>(d => d.Id)
                .HasConstraintName("hospitals_id_foreign");

            entity.HasOne(d => d.Region).WithMany(p => p.Hospitals)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("hospitals_region_id_foreign");
        });

        modelBuilder.Entity<PhoneNumber>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("phone_numbers_pkey");

            entity.ToTable("phone_numbers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactPhoneNumber)
                .HasMaxLength(11)
                .HasColumnName("contact_phone_number");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.GeneralPageId).HasColumnName("general_page_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.GeneralPage).WithMany(p => p.PhoneNumbers)
                .HasForeignKey(d => d.GeneralPageId)
                .HasConstraintName("phone_numbers_general_page_id_foreign");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("regions_pkey");

            entity.ToTable("regions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.RegionName)
                .HasMaxLength(50)
                .HasColumnName("region_name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Requisite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("requisites_pkey");

            entity.ToTable("requisites");

            entity.HasIndex(e => e.HospitalReduceName, "requisites_hospital_reduce_name_unique").IsUnique();

            entity.HasIndex(e => e.Inn, "requisites_inn_unique").IsUnique();

            entity.HasIndex(e => e.Kpp, "requisites_kpp_unique").IsUnique();

            entity.HasIndex(e => e.NameLegalFaces, "requisites_name_legal_faces_unique").IsUnique();

            entity.HasIndex(e => e.Ogrn, "requisites_ogrn_unique").IsUnique();

            entity.HasIndex(e => e.RegistrationDate, "requisites_registration_date_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.HospitalId).HasColumnName("hospital_id");
            entity.Property(e => e.HospitalReduceName)
                .HasMaxLength(75)
                .HasColumnName("hospital_reduce_name");
            entity.Property(e => e.Inn)
                .HasMaxLength(12)
                .HasColumnName("inn");
            entity.Property(e => e.Kpp)
                .HasMaxLength(9)
                .HasColumnName("kpp");
            entity.Property(e => e.NameLegalFaces)
                .HasMaxLength(150)
                .HasColumnName("name_legal_faces");
            entity.Property(e => e.Ogrn)
                .HasMaxLength(13)
                .HasColumnName("ogrn");
            entity.Property(e => e.RegistrationDate).HasColumnName("registration_date");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Hospital).WithMany(p => p.Requisites)
                .HasForeignKey(d => d.HospitalId)
                .HasConstraintName("requisites_hospital_id_foreign");
        });

        modelBuilder.Entity<Speciality>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("specialities_pkey");

            entity.ToTable("specialities");

            entity.HasIndex(e => e.Title, "specialities_title_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Title)
                .HasMaxLength(16)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("updated_at");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
