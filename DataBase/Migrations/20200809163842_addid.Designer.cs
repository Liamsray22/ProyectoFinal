﻿// <auto-generated />
using System;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataBase.Migrations
{
    [DbContext(typeof(ItlaElectorDBContext))]
    [Migration("20200809163842_addid")]
    partial class addid
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataBase.Models.Candidatos", b =>
                {
                    b.Property<int>("IdCandidato")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apellido")
                        .HasColumnType("text");

                    b.Property<string>("Estado")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<string>("FotoPerfil")
                        .HasColumnType("text");

                    b.Property<int>("IdPartido");

                    b.Property<int>("IdPuestoElectivo");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.HasKey("IdCandidato");

                    b.HasIndex("IdPartido");

                    b.HasIndex("IdPuestoElectivo");

                    b.ToTable("Candidatos");
                });

            modelBuilder.Entity("DataBase.Models.Ciudadanos", b =>
                {
                    b.Property<string>("Cedula")
                        .HasMaxLength(15)
                        .IsUnicode(false);

                    b.Property<string>("Apellido")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Estado")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.HasKey("Cedula");

                    b.ToTable("Ciudadanos");
                });

            modelBuilder.Entity("DataBase.Models.Elecciones", b =>
                {
                    b.Property<int>("IdEleccion")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Estado")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<DateTime?>("Fecha")
                        .HasColumnType("datetime");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.HasKey("IdEleccion");

                    b.ToTable("Elecciones");
                });

            modelBuilder.Entity("DataBase.Models.Partidos", b =>
                {
                    b.Property<int>("IdPartido")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .HasColumnType("text");

                    b.Property<string>("Estado")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<string>("Logo")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.HasKey("IdPartido");

                    b.ToTable("Partidos");
                });

            modelBuilder.Entity("DataBase.Models.PuestoElectivo", b =>
                {
                    b.Property<int>("IdPuestoElectivo")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .HasColumnType("text");

                    b.Property<string>("Estado")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.HasKey("IdPuestoElectivo");

                    b.ToTable("PuestoElectivo");
                });

            modelBuilder.Entity("DataBase.Models.Votacion", b =>
                {
                    b.Property<int>("IdVotacion")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cedula")
                        .HasMaxLength(15)
                        .IsUnicode(false);

                    b.Property<int>("IdCandidato");

                    b.Property<int>("IdEleccion");

                    b.HasKey("IdVotacion");

                    b.HasIndex("Cedula");

                    b.HasIndex("IdCandidato");

                    b.HasIndex("IdEleccion");

                    b.ToTable("Votacion");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DataBase.Models.Candidatos", b =>
                {
                    b.HasOne("DataBase.Models.Partidos", "IdPartidoNavigation")
                        .WithMany("Candidatos")
                        .HasForeignKey("IdPartido")
                        .HasConstraintName("FK__Candidato__IdPar__182C9B23");

                    b.HasOne("DataBase.Models.PuestoElectivo", "IdPuestoElectivoNavigation")
                        .WithMany("Candidatos")
                        .HasForeignKey("IdPuestoElectivo")
                        .HasConstraintName("FK__Candidato__IdPue__1920BF5C");
                });

            modelBuilder.Entity("DataBase.Models.Votacion", b =>
                {
                    b.HasOne("DataBase.Models.Ciudadanos", "CedulaNavigation")
                        .WithMany("Votacion")
                        .HasForeignKey("Cedula")
                        .HasConstraintName("FK__Votacion__Cedula__1BFD2C07");

                    b.HasOne("DataBase.Models.Candidatos", "IdCandidatoNavigation")
                        .WithMany("Votacion")
                        .HasForeignKey("IdCandidato")
                        .HasConstraintName("FK_Votacion_IdCand_1EE45789");

                    b.HasOne("DataBase.Models.Elecciones", "IdEleccionNavigation")
                        .WithMany("Votacion")
                        .HasForeignKey("IdEleccion")
                        .HasConstraintName("FK__Votacion__IdElec__1CF15040");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
