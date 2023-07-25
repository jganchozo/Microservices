﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ServiceShop.Api.Author.Persistence;

#nullable disable

namespace ServiceShop.Api.Author.Migrations
{
    [DbContext(typeof(AuthorContext))]
    partial class AuthorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ServiceShop.Api.Author.Model.BookAuthor", b =>
                {
                    b.Property<int>("BookAuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BookAuthorId"));

                    b.Property<string>("BookAuthorGuid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("BookAuthorId");

                    b.ToTable("BookAuthors");
                });

            modelBuilder.Entity("ServiceShop.Api.Author.Model.Degrees", b =>
                {
                    b.Property<int>("DegreesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("DegreesId"));

                    b.Property<string>("AcademicInstitution")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("BookAuthorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DegreesGuid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("DegreesId");

                    b.HasIndex("BookAuthorId");

                    b.ToTable("Degrees");
                });

            modelBuilder.Entity("ServiceShop.Api.Author.Model.Degrees", b =>
                {
                    b.HasOne("ServiceShop.Api.Author.Model.BookAuthor", "BookAuthor")
                        .WithMany("DegreeList")
                        .HasForeignKey("BookAuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookAuthor");
                });

            modelBuilder.Entity("ServiceShop.Api.Author.Model.BookAuthor", b =>
                {
                    b.Navigation("DegreeList");
                });
#pragma warning restore 612, 618
        }
    }
}