﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using _6_lab_NoPattern;

#nullable disable

namespace _6_lab_NoPattern.Migrations
{
    [DbContext(typeof(DoctorsContext))]
    partial class DoctorsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("_6_lab_NoPattern.Address", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("address");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("created_at");

                    b.Property<long>("GeneralPageId")
                        .HasColumnType("bigint")
                        .HasColumnName("general_page_id");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("addresses_pkey");

                    b.HasIndex("GeneralPageId");

                    b.ToTable("addresses", (string)null);
                });

            modelBuilder.Entity("_6_lab_NoPattern.Department", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("created_at");

                    b.Property<long>("HospitalId")
                        .HasColumnType("bigint")
                        .HasColumnName("hospital_id");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("departments_pkey");

                    b.HasIndex("HospitalId");

                    b.ToTable("departments", (string)null);
                });

            modelBuilder.Entity("_6_lab_NoPattern.Doctor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("address");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("created_at");

                    b.Property<DateOnly>("DateBirth")
                        .HasColumnType("date")
                        .HasColumnName("date_birth");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("name");

                    b.Property<string>("PassportDetails")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)")
                        .HasColumnName("passport_details");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("doctors_pkey");

                    b.HasIndex(new[] { "PassportDetails" }, "doctors_passport_details_unique")
                        .IsUnique();

                    b.ToTable("doctors", (string)null);
                });

            modelBuilder.Entity("_6_lab_NoPattern.DoctorSpeciality", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("created_at");

                    b.Property<DateOnly?>("DateEnd")
                        .HasColumnType("date")
                        .HasColumnName("date_end");

                    b.Property<DateOnly>("DateStart")
                        .HasColumnType("date")
                        .HasColumnName("date_start");

                    b.Property<long>("DepartmentId")
                        .HasColumnType("bigint")
                        .HasColumnName("department_id");

                    b.Property<long>("DoctorId")
                        .HasColumnType("bigint")
                        .HasColumnName("doctor_id");

                    b.Property<long>("SpecialityId")
                        .HasColumnType("bigint")
                        .HasColumnName("speciality_id");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("doctor_specialities_pkey");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("SpecialityId");

                    b.ToTable("doctor_specialities", (string)null);
                });

            modelBuilder.Entity("_6_lab_NoPattern.GeneralPage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("title");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("website");

                    b.HasKey("Id")
                        .HasName("general_pages_pkey");

                    b.ToTable("general_pages", (string)null);
                });

            modelBuilder.Entity("_6_lab_NoPattern.Hospital", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("created_at");

                    b.Property<long>("RegionId")
                        .HasColumnType("bigint")
                        .HasColumnName("region_id");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("hospitals_pkey");

                    b.HasIndex("RegionId");

                    b.ToTable("hospitals", (string)null);
                });

            modelBuilder.Entity("_6_lab_NoPattern.PhoneNumber", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("ContactPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)")
                        .HasColumnName("contact_phone_number");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("created_at");

                    b.Property<long>("GeneralPageId")
                        .HasColumnType("bigint")
                        .HasColumnName("general_page_id");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("phone_numbers_pkey");

                    b.HasIndex("GeneralPageId");

                    b.ToTable("phone_numbers", (string)null);
                });

            modelBuilder.Entity("_6_lab_NoPattern.Region", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("RegionName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("region_name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("regions_pkey");

                    b.ToTable("regions", (string)null);
                });

            modelBuilder.Entity("_6_lab_NoPattern.Requisite", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("created_at");

                    b.Property<long>("HospitalId")
                        .HasColumnType("bigint")
                        .HasColumnName("hospital_id");

                    b.Property<string>("HospitalReduceName")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("character varying(75)")
                        .HasColumnName("hospital_reduce_name");

                    b.Property<string>("Inn")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)")
                        .HasColumnName("inn");

                    b.Property<string>("Kpp")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("character varying(9)")
                        .HasColumnName("kpp");

                    b.Property<string>("NameLegalFaces")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("name_legal_faces");

                    b.Property<string>("Ogrn")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)")
                        .HasColumnName("ogrn");

                    b.Property<DateOnly>("RegistrationDate")
                        .HasColumnType("date")
                        .HasColumnName("registration_date");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("requisites_pkey");

                    b.HasIndex("HospitalId");

                    b.HasIndex(new[] { "HospitalReduceName" }, "requisites_hospital_reduce_name_unique")
                        .IsUnique();

                    b.HasIndex(new[] { "Inn" }, "requisites_inn_unique")
                        .IsUnique();

                    b.HasIndex(new[] { "Kpp" }, "requisites_kpp_unique")
                        .IsUnique();

                    b.HasIndex(new[] { "NameLegalFaces" }, "requisites_name_legal_faces_unique")
                        .IsUnique();

                    b.HasIndex(new[] { "Ogrn" }, "requisites_ogrn_unique")
                        .IsUnique();

                    b.HasIndex(new[] { "RegistrationDate" }, "requisites_registration_date_unique")
                        .IsUnique();

                    b.ToTable("requisites", (string)null);
                });

            modelBuilder.Entity("_6_lab_NoPattern.Speciality", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)")
                        .HasColumnName("title");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp(0) without time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("specialities_pkey");

                    b.HasIndex(new[] { "Title" }, "specialities_title_unique")
                        .IsUnique();

                    b.ToTable("specialities", (string)null);
                });

            modelBuilder.Entity("_6_lab_NoPattern.Address", b =>
                {
                    b.HasOne("_6_lab_NoPattern.GeneralPage", "GeneralPage")
                        .WithMany("Addresses")
                        .HasForeignKey("GeneralPageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("addresses_general_page_id_foreign");

                    b.Navigation("GeneralPage");
                });

            modelBuilder.Entity("_6_lab_NoPattern.Department", b =>
                {
                    b.HasOne("_6_lab_NoPattern.Hospital", "Hospital")
                        .WithMany("Departments")
                        .HasForeignKey("HospitalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("departments_hospital_id_foreign");

                    b.HasOne("_6_lab_NoPattern.GeneralPage", "IdNavigation")
                        .WithOne("Department")
                        .HasForeignKey("_6_lab_NoPattern.Department", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("departments_id_foreign");

                    b.Navigation("Hospital");

                    b.Navigation("IdNavigation");
                });

            modelBuilder.Entity("_6_lab_NoPattern.DoctorSpeciality", b =>
                {
                    b.HasOne("_6_lab_NoPattern.Department", "Department")
                        .WithMany("DoctorSpecialities")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("doctor_specialities_department_id_foreign");

                    b.HasOne("_6_lab_NoPattern.Doctor", "Doctor")
                        .WithMany("DoctorSpecialities")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("doctor_specialities_doctor_id_foreign");

                    b.HasOne("_6_lab_NoPattern.Speciality", "Speciality")
                        .WithMany("DoctorSpecialities")
                        .HasForeignKey("SpecialityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("doctor_specialities_speciality_id_foreign");

                    b.Navigation("Department");

                    b.Navigation("Doctor");

                    b.Navigation("Speciality");
                });

            modelBuilder.Entity("_6_lab_NoPattern.Hospital", b =>
                {
                    b.HasOne("_6_lab_NoPattern.GeneralPage", "IdNavigation")
                        .WithOne("Hospital")
                        .HasForeignKey("_6_lab_NoPattern.Hospital", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("hospitals_id_foreign");

                    b.HasOne("_6_lab_NoPattern.Region", "Region")
                        .WithMany("Hospitals")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("hospitals_region_id_foreign");

                    b.Navigation("IdNavigation");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("_6_lab_NoPattern.PhoneNumber", b =>
                {
                    b.HasOne("_6_lab_NoPattern.GeneralPage", "GeneralPage")
                        .WithMany("PhoneNumbers")
                        .HasForeignKey("GeneralPageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("phone_numbers_general_page_id_foreign");

                    b.Navigation("GeneralPage");
                });

            modelBuilder.Entity("_6_lab_NoPattern.Requisite", b =>
                {
                    b.HasOne("_6_lab_NoPattern.Hospital", "Hospital")
                        .WithMany("Requisites")
                        .HasForeignKey("HospitalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("requisites_hospital_id_foreign");

                    b.Navigation("Hospital");
                });

            modelBuilder.Entity("_6_lab_NoPattern.Department", b =>
                {
                    b.Navigation("DoctorSpecialities");
                });

            modelBuilder.Entity("_6_lab_NoPattern.Doctor", b =>
                {
                    b.Navigation("DoctorSpecialities");
                });

            modelBuilder.Entity("_6_lab_NoPattern.GeneralPage", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Department");

                    b.Navigation("Hospital");

                    b.Navigation("PhoneNumbers");
                });

            modelBuilder.Entity("_6_lab_NoPattern.Hospital", b =>
                {
                    b.Navigation("Departments");

                    b.Navigation("Requisites");
                });

            modelBuilder.Entity("_6_lab_NoPattern.Region", b =>
                {
                    b.Navigation("Hospitals");
                });

            modelBuilder.Entity("_6_lab_NoPattern.Speciality", b =>
                {
                    b.Navigation("DoctorSpecialities");
                });
#pragma warning restore 612, 618
        }
    }
}
