using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataBase.Models
{
    public partial class ItlaElectorDBContext : IdentityDbContext
    {
        public ItlaElectorDBContext()
        {
        }

        public ItlaElectorDBContext(DbContextOptions<ItlaElectorDBContext> options)
            : base(options)
        {
        }

        
        public virtual DbSet<Candidatos> Candidatos { get; set; }
        public virtual DbSet<Ciudadanos> Ciudadanos { get; set; }
        public virtual DbSet<Elecciones> Elecciones { get; set; }
        public virtual DbSet<Partidos> Partidos { get; set; }
        public virtual DbSet<PuestoElectivo> PuestoElectivo { get; set; }
        public virtual DbSet<Votacion> Votacion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-DK0MCFF;Database=ItlaElectorDB;Trusted_Connection=True; persist security info=True;Integrated Security =SSPI");

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Candidatos>(entity =>
            {
                entity.HasKey(e => e.IdCandidato);

                entity.HasIndex(e => e.IdPartido);

                entity.HasIndex(e => e.IdPuestoElectivo);

                entity.Property(e => e.Apellido).HasColumnType("text");

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FotoPerfil).HasColumnType("text");

                entity.Property(e => e.Nombre).HasColumnType("text");

                entity.HasOne(d => d.IdPartidoNavigation)
                    .WithMany(p => p.Candidatos)
                    .HasForeignKey(d => d.IdPartido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Candidato__IdPar__182C9B23");

                entity.HasOne(d => d.IdPuestoElectivoNavigation)
                    .WithMany(p => p.Candidatos)
                    .HasForeignKey(d => d.IdPuestoElectivo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Candidato__IdPue__1920BF5C");
            });

            modelBuilder.Entity<Ciudadanos>(entity =>
            {
                entity.HasKey(e => e.Cedula);

                entity.Property(e => e.Cedula)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Apellido).HasColumnType("text");

                entity.Property(e => e.Email).HasColumnType("text");

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre).HasColumnType("text");
            });

            modelBuilder.Entity<Elecciones>(entity =>
            {
                entity.HasKey(e => e.IdEleccion);

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Nombre).HasColumnType("text");
            });

            modelBuilder.Entity<Partidos>(entity =>
            {
                entity.HasKey(e => e.IdPartido);

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Logo).HasColumnType("text");

                entity.Property(e => e.Nombre).HasColumnType("text");
            });

            modelBuilder.Entity<PuestoElectivo>(entity =>
            {
                entity.HasKey(e => e.IdPuestoElectivo);

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre).HasColumnType("text");
            });

            modelBuilder.Entity<Votacion>(entity =>
            {
                entity.HasKey(e => e.IdVotacion);

                entity.HasIndex(e => e.Cedula);

                entity.HasIndex(e => e.IdCandidato);

                entity.HasIndex(e => e.IdEleccion);

                entity.Property(e => e.Cedula)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.CedulaNavigation)
                    .WithMany(p => p.Votacion)
                    .HasForeignKey(d => d.Cedula)
                    .HasConstraintName("FK__Votacion__Cedula__1BFD2C07");

                entity.HasOne(d => d.IdCandidatoNavigation)
                    .WithMany(p => p.Votacion)
                    .HasForeignKey(d => d.IdCandidato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Votacion_IdCand_1EE45789");

                entity.HasOne(d => d.IdEleccionNavigation)
                    .WithMany(p => p.Votacion)
                    .HasForeignKey(d => d.IdEleccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Votacion__IdElec__1CF15040");
            });
        }
    }
}
