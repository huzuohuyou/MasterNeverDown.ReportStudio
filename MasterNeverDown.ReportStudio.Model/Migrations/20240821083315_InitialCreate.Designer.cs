﻿// <auto-generated />
using CommunityToolkit.ReportEditor.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CommunityToolkit.ReportEditor.Model.Migrations
{
    [DbContext(typeof(DataSourceContext))]
    [Migration("20240821083315_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("CommunityToolkit.MasterNeverDown.ReportStudio.Model.Models.DataScript", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreateDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Script")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("DataScripts");
                });
#pragma warning restore 612, 618
        }
    }
}
