using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookStoreDbFirst.Models;

public partial class CompaniesContext : DbContext
{
    public CompaniesContext()
    {
    }

    public CompaniesContext(DbContextOptions<CompaniesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Butiker> Butikers { get; set; }

    public virtual DbSet<Böcker> Böckers { get; set; }

    public virtual DbSet<Författare> Författares { get; set; }

    public virtual DbSet<Förlag> Förlags { get; set; }

    public virtual DbSet<Kunder> Kunders { get; set; }

    public virtual DbSet<LagerSaldo> LagerSaldos { get; set; }

    public virtual DbSet<OrderDetaljer> OrderDetaljers { get; set; }

    public virtual DbSet<Ordrar> Ordrars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-F0BT43V\\SQLEXPRESS;Initial Catalog=JackBookStoreDB1;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Butiker>(entity =>
        {
            entity.HasKey(e => e.ButikId).HasName("PK__Butiker__B5D66BFA5724855C");

            entity.ToTable("Butiker");

            entity.Property(e => e.ButikId).HasColumnName("ButikID");
            entity.Property(e => e.Adress).HasMaxLength(300);
            entity.Property(e => e.Namn).HasMaxLength(200);
            entity.Property(e => e.Postnummer).HasMaxLength(20);
            entity.Property(e => e.Stad).HasMaxLength(100);
        });

        modelBuilder.Entity<Böcker>(entity =>
        {
            entity.HasKey(e => e.Isbn13).HasName("PK__Böcker__3BF79E030472FEED");

            entity.ToTable("Böcker");

            entity.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .HasColumnName("ISBN13");
            entity.Property(e => e.FörfattarId).HasColumnName("FörfattarID");
            entity.Property(e => e.Titel).HasMaxLength(200);

            entity.HasOne(d => d.Författar).WithMany(p => p.Böckers)
                .HasForeignKey(d => d.FörfattarId)
                .HasConstraintName("FK__Böcker__Författa__4BAC3F29");
        });

        modelBuilder.Entity<Författare>(entity =>
        {
            entity.HasKey(e => e.FörfattarId).HasName("PK__Författa__C726F5D30EDAB6F2");

            entity.ToTable("Författare");

            entity.Property(e => e.FörfattarId).HasColumnName("FörfattarID");
            entity.Property(e => e.Efternamn).HasMaxLength(100);
            entity.Property(e => e.Förnamn).HasMaxLength(100);
        });

        modelBuilder.Entity<Förlag>(entity =>
        {
            entity.HasKey(e => e.FörlagId).HasName("PK__Förlag__DE6A852C444ED5EB");

            entity.ToTable("Förlag");

            entity.Property(e => e.FörlagId).HasColumnName("FörlagID");
            entity.Property(e => e.Adress).HasMaxLength(300);
            entity.Property(e => e.Namn).HasMaxLength(200);
            entity.Property(e => e.Postnummer).HasMaxLength(20);
            entity.Property(e => e.Stad).HasMaxLength(100);
        });

        modelBuilder.Entity<Kunder>(entity =>
        {
            entity.HasKey(e => e.KundId).HasName("PK__Kunder__F2B5DEAC19997BFB");

            entity.ToTable("Kunder");

            entity.HasIndex(e => e.Email, "UQ__Kunder__A9D10534693874F3").IsUnique();

            entity.Property(e => e.KundId).HasColumnName("KundID");
            entity.Property(e => e.Efternamn).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Förnamn).HasMaxLength(100);
        });

        modelBuilder.Entity<LagerSaldo>(entity =>
        {
            entity.HasKey(e => e.LagerSaldoId).HasName("PK__LagerSal__188D26996B6504D2");

            entity.ToTable("LagerSaldo");

            entity.Property(e => e.LagerSaldoId).HasColumnName("LagerSaldoID");
            entity.Property(e => e.ButikId).HasColumnName("ButikID");
            entity.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .HasColumnName("ISBN13");

            entity.HasOne(d => d.Butik).WithMany(p => p.LagerSaldos)
                .HasForeignKey(d => d.ButikId)
                .HasConstraintName("FK__LagerSald__Butik__5070F446");

            entity.HasOne(d => d.Bok).WithMany(p => p.LagerSaldos)
                .HasForeignKey(d => d.Isbn13)
                .HasConstraintName("FK__LagerSald__ISBN1__5165187F");
        });

        modelBuilder.Entity<OrderDetaljer>(entity =>
        {
            entity.HasKey(e => e.OrderDetaljId).HasName("PK__OrderDet__94F5EAB5BB46ED9B");

            entity.ToTable("OrderDetaljer");

            entity.Property(e => e.OrderDetaljId).HasColumnName("OrderDetaljID");
            entity.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .HasColumnName("ISBN13");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Pris).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Bok).WithMany(p => p.OrderDetaljers)
                .HasForeignKey(d => d.Isbn13)
                .HasConstraintName("FK__OrderDeta__ISBN1__5BE2A6F2");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetaljers)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__5AEE82B9");
        });

        modelBuilder.Entity<Ordrar>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Ordrar__C3905BAFD6FB2C38");

            entity.ToTable("Ordrar");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.KundId).HasColumnName("KundID");
            entity.Property(e => e.OrderDatum)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Kund).WithMany(p => p.Ordrars)
                .HasForeignKey(d => d.KundId)
                .HasConstraintName("FK__Ordrar__KundID__5812160E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
