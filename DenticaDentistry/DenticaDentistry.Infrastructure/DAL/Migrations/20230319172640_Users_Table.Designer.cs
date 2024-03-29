﻿// <auto-generated />
using System;
using DenticaDentistry.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dentica_Dentistry.Infrastructure.DAL.Migrations
{
    [DbContext(typeof(DenticaDentistryDbContext))]
    [Migration("20230319172640_Users_Table")]
    partial class Users_Table
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.1.23111.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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

                    b.HasKey("DentistIndustryId");

                    b.ToTable("DentistIndustries");
                });

            modelBuilder.Entity("DenticaDentistry.Core.Entities.Reservation", b =>
                {
                    b.Property<Guid>("ReservationId")
                        .HasColumnType("uuid");

                    b.Property<string>("BookerName")
                        .HasColumnType("text");

                    b.Property<int>("DentistIndustryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ReservationId");

                    b.HasIndex("DentistIndustryId");

                    b.ToTable("Reservations");
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

            modelBuilder.Entity("DenticaDentistry.Core.Entities.Reservation", b =>
                {
                    b.HasOne("DenticaDentistry.Core.Entities.DentistIndustry", null)
                        .WithMany("Reservations")
                        .HasForeignKey("DentistIndustryId")
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
