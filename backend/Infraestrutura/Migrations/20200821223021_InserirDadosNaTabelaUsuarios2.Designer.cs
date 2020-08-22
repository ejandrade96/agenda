﻿// <auto-generated />
using System;
using Agenda.Infraestrutura.Contextos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infraestrutura.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20200821223021_InserirDadosNaTabelaUsuarios2")]
    partial class InserirDadosNaTabelaUsuarios2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4");

            modelBuilder.Entity("Agenda.Dominio.Modelos.Contato", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Celular")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefone")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UsuarioId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Contatos");
                });

            modelBuilder.Entity("Agenda.Dominio.Modelos.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Login")
                        .HasColumnType("TEXT");

                    b.Property<string>("Senha")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Agenda.Dominio.Modelos.Contato", b =>
                {
                    b.HasOne("Agenda.Dominio.Modelos.Usuario", "Usuario")
                        .WithMany("Contatos")
                        .HasForeignKey("UsuarioId");
                });
#pragma warning restore 612, 618
        }
    }
}
