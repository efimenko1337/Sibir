﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sibir.DAL;

#nullable disable

namespace Sibir.DAL.Migrations
{
    [DbContext(typeof(SqlServerContext))]
    partial class SqlServerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ExecutersProjects", b =>
                {
                    b.Property<Guid>("ExecutableProjectsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ExecutersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ExecutableProjectsId", "ExecutersId");

                    b.HasIndex("ExecutersId");

                    b.ToTable("ExecutersProjects");
                });

            modelBuilder.Entity("Sibir.Domain.Models.EntityObject.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.ComplexProperty<Dictionary<string, object>>("Email", "Sibir.Domain.Models.EntityObject.Employee.Email#Email", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("Email");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Name", "Sibir.Domain.Models.EntityObject.Employee.Name#Name", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("MiddleName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("SecondName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Role", "Sibir.Domain.Models.EntityObject.Employee.Role#Role", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(15)
                                .HasColumnType("nvarchar(15)")
                                .HasColumnName("Role");
                        });

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Sibir.Domain.Models.EntityObject.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<Guid?>("ManagerId")
                        .HasColumnType("uniqueidentifier");

                    b.ComplexProperty<Dictionary<string, object>>("Company", "Sibir.Domain.Models.EntityObject.Project.Company#Company", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Consumer")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("Executer")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("DevelopmentTime", "Sibir.Domain.Models.EntityObject.Project.DevelopmentTime#DevelopmentTime", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<DateOnly>("FinishDate")
                                .HasColumnType("date");

                            b1.Property<DateOnly>("StartDate")
                                .HasColumnType("date");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Priority", "Sibir.Domain.Models.EntityObject.Project.Priority#Priority", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Value")
                                .HasColumnType("int")
                                .HasColumnName("Priority");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Title", "Sibir.Domain.Models.EntityObject.Project.Title#Title", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("Title");
                        });

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Sibir.Domain.Models.EntityObject.Task", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<Guid>("CreaterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ExecuterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.ComplexProperty<Dictionary<string, object>>("Comment", "Sibir.Domain.Models.EntityObject.Task.Comment#Comment", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .HasMaxLength(500)
                                .HasColumnType("nvarchar(500)")
                                .HasColumnName("Comment");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Priority", "Sibir.Domain.Models.EntityObject.Task.Priority#Priority", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Value")
                                .HasColumnType("int")
                                .HasColumnName("Priority");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Status", "Sibir.Domain.Models.EntityObject.Task.Status#Status", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(15)
                                .HasColumnType("nvarchar(15)")
                                .HasColumnName("Status");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Title", "Sibir.Domain.Models.EntityObject.Task.Title#Title", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("Title");
                        });

                    b.HasKey("Id");

                    b.HasIndex("CreaterId");

                    b.HasIndex("ExecuterId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("ExecutersProjects", b =>
                {
                    b.HasOne("Sibir.Domain.Models.EntityObject.Project", null)
                        .WithMany()
                        .HasForeignKey("ExecutableProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sibir.Domain.Models.EntityObject.Employee", null)
                        .WithMany()
                        .HasForeignKey("ExecutersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sibir.Domain.Models.EntityObject.Project", b =>
                {
                    b.HasOne("Sibir.Domain.Models.EntityObject.Employee", "Manager")
                        .WithMany("ManagedProjects")
                        .HasForeignKey("ManagerId");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("Sibir.Domain.Models.EntityObject.Task", b =>
                {
                    b.HasOne("Sibir.Domain.Models.EntityObject.Employee", "Creater")
                        .WithMany("CreatedTasks")
                        .HasForeignKey("CreaterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sibir.Domain.Models.EntityObject.Employee", "Executer")
                        .WithMany("ExecutableTasks")
                        .HasForeignKey("ExecuterId");

                    b.HasOne("Sibir.Domain.Models.EntityObject.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creater");

                    b.Navigation("Executer");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Sibir.Domain.Models.EntityObject.Employee", b =>
                {
                    b.Navigation("CreatedTasks");

                    b.Navigation("ExecutableTasks");

                    b.Navigation("ManagedProjects");
                });

            modelBuilder.Entity("Sibir.Domain.Models.EntityObject.Project", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
