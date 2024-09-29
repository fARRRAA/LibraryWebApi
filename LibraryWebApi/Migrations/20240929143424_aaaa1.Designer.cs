﻿// <auto-generated />
using System;
using LibraryWebApi.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibraryWebApi.Migrations
{
    [DbContext(typeof(LibraryWebApiDb))]
    [Migration("20240929143424_aaaa1")]
    partial class aaaa1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LibraryWebApi.Model.BookExemplar", b =>
                {
                    b.Property<int>("Exemplar_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Exemplar_Id"));

                    b.Property<int>("Book_Id")
                        .HasColumnType("int");

                    b.Property<int>("Books_Count")
                        .HasColumnType("int");

                    b.Property<int>("Exemplar_Price")
                        .HasColumnType("int");

                    b.HasKey("Exemplar_Id");

                    b.ToTable("BookExemplar");
                });

            modelBuilder.Entity("LibraryWebApi.Model.Books", b =>
                {
                    b.Property<int>("Id_Book")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Book"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id_Genre")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Year")
                        .HasColumnType("datetime2");

                    b.HasKey("Id_Book");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("LibraryWebApi.Model.Genre", b =>
                {
                    b.Property<int>("Id_Genre")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Genre"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Genre");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("LibraryWebApi.Model.Readers", b =>
                {
                    b.Property<int>("Id_User")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_User"));

                    b.Property<DateTime>("Date_Birth")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id_Role")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_User");

                    b.ToTable("Readers");
                });

            modelBuilder.Entity("LibraryWebApi.Model.RentHistory", b =>
                {
                    b.Property<int>("id_Rent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_Rent"));

                    b.Property<int>("Id_Book")
                        .HasColumnType("int");

                    b.Property<int>("Id_Reader")
                        .HasColumnType("int");

                    b.Property<DateTime>("Rental_Start")
                        .HasColumnType("datetime2");

                    b.Property<int>("Rental_Time")
                        .HasColumnType("int");

                    b.HasKey("id_Rent");

                    b.ToTable("RentHistory");
                });

            modelBuilder.Entity("LibraryWebApi.Model.Roles", b =>
                {
                    b.Property<int>("Id_Role")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Role"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Role");

                    b.ToTable("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}