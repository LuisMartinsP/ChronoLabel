using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ChronoLabel.Models;

namespace ChronoLabel.Data;

public partial class ChronoLabelContext : DbContext
{
    private readonly IConfiguration? _configuration;
    public ChronoLabelContext(DbContextOptions<ChronoLabelContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Produto> Produtos { get; set; }

    public virtual DbSet<Relatorio> Relatorios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.IdProduto).HasName("PRIMARY");

            entity.ToTable("PRODUTOS");

            entity.Property(e => e.IdProduto).HasColumnName("id_produto");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Peso).HasColumnName("peso");
            entity.Property(e => e.Quantidade).HasColumnName("quantidade");
        });

        modelBuilder.Entity<Relatorio>(entity =>
        {
            entity.HasKey(e => e.IdRelatorio).HasName("PRIMARY");

            entity.ToTable("RELATORIOS");

            entity.HasIndex(e => e.CpfUsuario, "idx_cpf_usuario");

            entity.HasIndex(e => e.IdProduto, "idx_id_produto");

            entity.Property(e => e.IdRelatorio).HasColumnName("id_relatorio");
            entity.Property(e => e.CpfUsuario)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("cpf_usuario");
            entity.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasColumnName("data_criacao");
            entity.Property(e => e.DataTermino)
                .HasColumnType("datetime")
                .HasColumnName("data_termino");
            entity.Property(e => e.Duracao)
                .HasColumnType("time")
                .HasColumnName("duracao");
            entity.Property(e => e.IdProduto).HasColumnName("id_produto");
            entity.Property(e => e.QuantidadeProdutoOperado).HasColumnName("quantidade_produto_operado");

            entity.HasOne(d => d.CpfUsuarioNavigation).WithMany(p => p.Relatorios)
                .HasForeignKey(d => d.CpfUsuario)
                .HasConstraintName("RELATORIOS_ibfk_1");

            entity.HasOne(d => d.IdProdutoNavigation).WithMany(p => p.Relatorios)
                .HasForeignKey(d => d.IdProduto)
                .HasConstraintName("RELATORIOS_ibfk_2");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Cpf).HasName("PRIMARY");

            entity.ToTable("USUARIOS");

            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("cpf");
            entity.Property(e => e.Nome)
                .HasMaxLength(20)
                .HasColumnName("nome");
            entity.Property(e => e.Senha)
                .HasMaxLength(60)
                .HasColumnName("senha")
                .IsRequired();
            entity.Property(e => e.Tipo)
                .HasColumnType("enum('operador','administrador')")
                .HasColumnName("tipo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
