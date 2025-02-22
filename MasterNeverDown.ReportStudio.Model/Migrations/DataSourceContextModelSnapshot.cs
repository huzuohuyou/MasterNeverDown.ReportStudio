﻿// <auto-generated />
using CommunityToolkit.ReportEditor.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CommunityToolkit.ReportEditor.Model.Migrations
{
    [DbContext(typeof(DataSourceContext))]
    partial class DataSourceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.33");

            modelBuilder.Entity("CommunityToolkit.ReportEditor.Model.Models.Connection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreateDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Database")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Port")
                        .HasColumnType("INTEGER");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Connections");
                });

            modelBuilder.Entity("CommunityToolkit.ReportEditor.Model.Models.DataSource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Connection")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreateDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastResult")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Script")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("DataSources");
                });

            modelBuilder.Entity("CommunityToolkit.ReportEditor.Model.Models.Parameter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Connection")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreateDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("DataSourceType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DisplayName")
                        .HasColumnType("TEXT");

                    b.Property<int?>("InputType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Script")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Parameters");
                });

            modelBuilder.Entity("CommunityToolkit.ReportEditor.Model.Models.Template", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ColumnCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("DataSource")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RowCount")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Templates");
                });
#pragma warning restore 612, 618
        }
    }
}
