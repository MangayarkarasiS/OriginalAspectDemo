﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentService.Data;

#nullable disable

namespace StudentService.Migrations
{
    [DbContext(typeof(StudentServiceContext))]
    partial class StudentServiceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StudentService.Models.Stud", b =>
                {
                    b.Property<int>("studId")
                        .HasColumnType("int");

                    b.Property<DateTime>("studDOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("studGender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("studName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("studTotalMarks")
                        .HasColumnType("int");

                    b.HasKey("studId");

                    b.ToTable("Stud");
                });

            modelBuilder.Entity("StudentService.Models.UserCredentials", b =>
                {
                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserName");

                    b.ToTable("UserCredentials");
                });
#pragma warning restore 612, 618
        }
    }
}
