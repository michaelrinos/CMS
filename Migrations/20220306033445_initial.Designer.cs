﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SportsStore.Models;

namespace CMS.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220306033445_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SportsStore.Models.RazerView", b =>
                {
                    b.Property<int>("RazerViewId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CSSContent");

                    b.Property<int>("CSSContentId");

                    b.Property<string>("HTMLContent");

                    b.Property<int>("HTMLContentId");

                    b.Property<string>("JSContent");

                    b.Property<int>("JSContentId");

                    b.Property<DateTimeOffset>("LastModified");

                    b.Property<DateTime?>("LastRequested");

                    b.Property<string>("Location");

                    b.Property<string>("Model");

                    b.HasKey("RazerViewId");

                    b.ToTable("Views");
                });
#pragma warning restore 612, 618
        }
    }
}
