﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RepositorioDeConhecimento.Infrastructure.Context;

#nullable disable

namespace RepositorioDeConhecimento.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230225200028_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RepositorioDeConhecimento.Models.Domain.Entities.Autor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("FotoId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("Telefone")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("FotoId");

                    b.ToTable("Autores");
                });

            modelBuilder.Entity("RepositorioDeConhecimento.Models.Domain.Entities.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descricao")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("IconeId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("IconeId");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("RepositorioDeConhecimento.Models.Domain.Entities.Conhecimento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AutorId")
                        .HasColumnType("int");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("Conteudo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("AutorId");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Conhecimentos");
                });

            modelBuilder.Entity("RepositorioDeConhecimento.Models.Domain.Entities.Imagem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ConhecimentoId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<byte[]>("Conteudo")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TamanhoArquivo")
                        .HasColumnType("int");

                    b.Property<string>("TipoArquivo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("ConhecimentoId");

                    b.ToTable("Imagens");
                });

            modelBuilder.Entity("RepositorioDeConhecimento.Models.Domain.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ConhecimentoId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ConhecimentoId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("RepositorioDeConhecimento.Models.Domain.Entities.Autor", b =>
                {
                    b.HasOne("RepositorioDeConhecimento.Models.Domain.Entities.Imagem", "Foto")
                        .WithMany()
                        .HasForeignKey("FotoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Foto");
                });

            modelBuilder.Entity("RepositorioDeConhecimento.Models.Domain.Entities.Categoria", b =>
                {
                    b.HasOne("RepositorioDeConhecimento.Models.Domain.Entities.Imagem", "Icone")
                        .WithMany()
                        .HasForeignKey("IconeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Icone");
                });

            modelBuilder.Entity("RepositorioDeConhecimento.Models.Domain.Entities.Conhecimento", b =>
                {
                    b.HasOne("RepositorioDeConhecimento.Models.Domain.Entities.Autor", "Autor")
                        .WithMany()
                        .HasForeignKey("AutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RepositorioDeConhecimento.Models.Domain.Entities.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Autor");

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("RepositorioDeConhecimento.Models.Domain.Entities.Imagem", b =>
                {
                    b.HasOne("RepositorioDeConhecimento.Models.Domain.Entities.Conhecimento", "Conhecimento")
                        .WithMany()
                        .HasForeignKey("ConhecimentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conhecimento");
                });

            modelBuilder.Entity("RepositorioDeConhecimento.Models.Domain.Entities.Tag", b =>
                {
                    b.HasOne("RepositorioDeConhecimento.Models.Domain.Entities.Conhecimento", "Conhecimento")
                        .WithMany()
                        .HasForeignKey("ConhecimentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conhecimento");
                });
#pragma warning restore 612, 618
        }
    }
}
