using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Final_IngenieriaSoftware.Models;

public partial class SistemaDeVotacionContext : DbContext
{
    public SistemaDeVotacionContext()
    {
    }

    public SistemaDeVotacionContext(DbContextOptions<SistemaDeVotacionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Candidato> Candidatos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<StateApp> StateApps { get; set; }

    public virtual DbSet<Votacione> Votaciones { get; set; }

    public virtual DbSet<Votante> Votantes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=10.20.1.9;userid=kromero;password=kHvilla31;database=sistema_de_votacion;TreatTinyAsBoolean=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidato>(entity =>
        {
            entity.HasKey(e => e.Idcandidatos).HasName("PRIMARY");

            entity.ToTable("candidatos");

            entity.Property(e => e.Idcandidatos).HasColumnName("idcandidatos");
            entity.Property(e => e.Afiliados).HasColumnName("afiliados");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
            entity.Property(e => e.Partido)
                .HasMaxLength(45)
                .HasColumnName("partido");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PRIMARY");

            entity.ToTable("rol");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Tipo)
                .HasMaxLength(45)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<StateApp>(entity =>
        {
            entity.HasKey(e => e.IdstateApp).HasName("PRIMARY");

            entity.ToTable("state_app");

            entity.Property(e => e.IdstateApp).HasColumnName("idstate_app");
            entity.Property(e => e.State).HasColumnName("state");
        });

        modelBuilder.Entity<Votacione>(entity =>
        {
            entity.HasKey(e => e.IdnewTable).HasName("PRIMARY");

            entity.ToTable("votaciones");

            entity.HasIndex(e => e.CandidatosIdcandidatos, "fk_Votaciones_candidatos1_idx");

            entity.HasIndex(e => e.VotanteIdvotante, "fk_Votaciones_votante_idx");

            entity.Property(e => e.IdnewTable).HasColumnName("idnew_table");
            entity.Property(e => e.CandidatosIdcandidatos).HasColumnName("candidatos_idcandidatos");
            entity.Property(e => e.VotanteIdvotante).HasColumnName("votante_idvotante");

            entity.HasOne(d => d.CandidatosIdcandidatosNavigation).WithMany(p => p.Votaciones)
                .HasForeignKey(d => d.CandidatosIdcandidatos)
                .HasConstraintName("fk_Votaciones_candidatos1");

            entity.HasOne(d => d.VotanteIdvotanteNavigation).WithMany(p => p.Votaciones)
                .HasForeignKey(d => d.VotanteIdvotante)
                .HasConstraintName("fk_Votaciones_votante");
        });

        modelBuilder.Entity<Votante>(entity =>
        {
            entity.HasKey(e => e.Idvotante).HasName("PRIMARY");

            entity.ToTable("votante");

            entity.HasIndex(e => e.RolIdRol, "fk_votante_Rol1_idx");

            entity.Property(e => e.Idvotante).HasColumnName("idvotante");
            entity.Property(e => e.Dpi).HasColumnName("DPI");
            entity.Property(e => e.Nombre).HasMaxLength(45);
            entity.Property(e => e.RolIdRol).HasColumnName("Rol_idRol");

            entity.HasOne(d => d.RolIdRolNavigation).WithMany(p => p.Votantes)
                .HasForeignKey(d => d.RolIdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_votante_Rol1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
