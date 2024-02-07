using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TP3Console.Models.EntityFramework;

public partial class FilmsDbContext : DbContext
{
    public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

    public FilmsDbContext()
    {
    }

    public FilmsDbContext(DbContextOptions<FilmsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Avi> Avis { get; set; }

    public virtual DbSet<Categorie> Categories { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        { 
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            optionsBuilder.UseLoggerFactory(MyLoggerFactory)
                          .EnableSensitiveDataLogging()
                          .UseNpgsql("Server=localhost;port=5432;Database=FilmsDB;uid=postgres;password=postgres;");
            //optionsBuilder.UseLazyLoadingProxies();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Avi>(entity =>
        {
            entity.HasKey(e => new { e.Film, e.Utilisateur }).HasName("pk_avis");

            entity.HasOne(d => d.FilmNavigation).WithMany(p => p.Avis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_avis_film");

            entity.HasOne(d => d.UtilisateurNavigation).WithMany(p => p.Avis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_avis_utilisateur");
        });

        modelBuilder.Entity<Categorie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_categorie");
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_film");

            entity.HasOne(d => d.CategorieNavigation).WithMany(p => p.Films)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_film_categorie");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_utilisateur");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
