﻿// <auto-generated />
using System;
using DenticaDentistry.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DenticaDentistry.Infrastructure.DAL.Migrations
{
    [DbContext(typeof(DenticaDentistryDbContext))]
    partial class DenticaDentistryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.1.23111.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DenticaDentistry.Core.Entities.Dentist", b =>
                {
                    b.Property<Guid>("DentistId")
                        .HasColumnType("uuid");

                    b.Property<string>("LicenseNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("DentistId");

                    b.HasIndex("LicenseNumber")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Dentists");
                });

            modelBuilder.Entity("DenticaDentistry.Core.Entities.DentistIndustry", b =>
                {
                    b.Property<int>("DentistIndustryId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<int>("ServiceTypeId")
                        .HasColumnType("integer");

                    b.HasKey("DentistIndustryId");

                    b.HasIndex("ServiceTypeId");

                    b.ToTable("DentistIndustries");
                });

            modelBuilder.Entity("DenticaDentistry.Core.Entities.Reservation", b =>
                {
                    b.Property<Guid>("ReservationId")
                        .HasColumnType("uuid");

                    b.Property<string>("BookerName")
                        .HasColumnType("text");

                    b.Property<Guid?>("DentistId")
                        .HasColumnType("uuid");

                    b.Property<int>("DentistIndustryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("ReservationId");

                    b.HasIndex("DentistId");

                    b.HasIndex("DentistIndustryId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("DenticaDentistry.Core.Entities.ServiceType", b =>
                {
                    b.Property<int>("ServiceTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("ServiceTypeName")
                        .HasColumnType("text");

                    b.HasKey("ServiceTypeId");

                    b.ToTable("ServiceTypes");
                });

            modelBuilder.Entity("DenticaDentistry.Core.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DenticaDentistry.Core.Entities.Dentist", b =>
                {
                    b.HasOne("DenticaDentistry.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DenticaDentistry.Core.Entities.DentistIndustry", b =>
                {
                    b.HasOne("DenticaDentistry.Core.Entities.ServiceType", null)
                        .WithMany()
                        .HasForeignKey("ServiceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DenticaDentistry.Core.Entities.Reservation", b =>
                {
                    b.HasOne("DenticaDentistry.Core.Entities.Dentist", null)
                        .WithMany()
                        .HasForeignKey("DentistId");

                    b.HasOne("DenticaDentistry.Core.Entities.DentistIndustry", null)
                        .WithMany("Reservations")
                        .HasForeignKey("DentistIndustryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DenticaDentistry.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DenticaDentistry.Core.Entities.DentistIndustry", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
