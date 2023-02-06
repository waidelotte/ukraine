﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Ukraine.Services.Example.Infrastructure.EfCore;

#nullable disable

namespace Ukraine.Services.Example.Infrastructure.EfCore.Migrations
{
    [DbContext(typeof(ExampleContext))]
    [Migration("20230206194745_TableRename")]
    partial class TableRename
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("example_schema")
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Ukraine.Services.Example.Domain.Models.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int?>("Age")
                        .HasColumnType("integer")
                        .HasColumnName("age");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("full_name");

                    b.Property<Guid>("SuperSecretKey")
                        .HasColumnType("uuid")
                        .HasColumnName("super_secret_key");

                    b.HasKey("Id")
                        .HasName("pk_authors");

                    b.ToTable("authors", "example_schema");
                });

            modelBuilder.Entity("Ukraine.Services.Example.Domain.Models.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid")
                        .HasColumnName("author_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("Rating")
                        .HasColumnType("integer")
                        .HasColumnName("rating");

                    b.HasKey("Id")
                        .HasName("pk_books");

                    b.HasIndex("AuthorId")
                        .HasDatabaseName("ix_books_author_id");

                    b.ToTable("books", "example_schema");
                });

            modelBuilder.Entity("Ukraine.Services.Example.Domain.Models.Book", b =>
                {
                    b.HasOne("Ukraine.Services.Example.Domain.Models.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_books_authors_author_id");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Ukraine.Services.Example.Domain.Models.Author", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
