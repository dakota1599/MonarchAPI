﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MonarchAPI.Data;

namespace MonarchAPI.Migrations
{
    [DbContext(typeof(MonarchContext))]
    [Migration("20210708190718_AddOrgID")]
    partial class AddOrgID
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MonarchAPI.Models.CheckIn", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CheckedIn")
                        .HasColumnType("bit");

                    b.Property<int>("MeetingID")
                        .HasColumnType("int");

                    b.Property<string>("MeetingName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MemberID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MemberID");

                    b.ToTable("CheckIn");
                });

            modelBuilder.Entity("MonarchAPI.Models.Meeting", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MemberID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("OrgID")
                        .HasColumnType("int");

                    b.Property<string>("Owner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OwnerID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MemberID");

                    b.ToTable("Meeting");
                });

            modelBuilder.Entity("MonarchAPI.Models.Member", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Admin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Org")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrgID")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.ToTable("Member");
                });

            modelBuilder.Entity("MonarchAPI.Models.Organization", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Organization");
                });

            modelBuilder.Entity("MonarchAPI.Models.CheckIn", b =>
                {
                    b.HasOne("MonarchAPI.Models.Member", null)
                        .WithMany("CheckIns")
                        .HasForeignKey("MemberID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MonarchAPI.Models.Meeting", b =>
                {
                    b.HasOne("MonarchAPI.Models.Member", null)
                        .WithMany("Meetings")
                        .HasForeignKey("MemberID");
                });

            modelBuilder.Entity("MonarchAPI.Models.Member", b =>
                {
                    b.Navigation("CheckIns");

                    b.Navigation("Meetings");
                });
#pragma warning restore 612, 618
        }
    }
}
