﻿// <auto-generated />
using System;
using ManyEvents.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ManyEvents.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20230408154403_MEvent.person.0")]
    partial class MEventperson0
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("MEventMPerson", b =>
                {
                    b.Property<int>("EventsListId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MPersonsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("EventsListId", "MPersonsId");

                    b.HasIndex("MPersonsId");

                    b.ToTable("MEventMPerson");
                });

            modelBuilder.Entity("ManyEvents.Models.MEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Place")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MEvent");
                });

            modelBuilder.Entity("ManyEvents.Models.MFeeType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("Remarks")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MFeeType");
                });

            modelBuilder.Entity("ManyEvents.Models.MPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("FeeTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("PersonalCodeCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasMaxLength(1500)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FeeTypeId");

                    b.HasIndex("PersonalCodeCode");

                    b.ToTable("MPerson");
                });

            modelBuilder.Entity("ManyEvents.Models.PersonalCode", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(11)
                        .HasColumnType("TEXT");

                    b.HasKey("Code");

                    b.ToTable("PersonalCode");
                });

            modelBuilder.Entity("MEventMPerson", b =>
                {
                    b.HasOne("ManyEvents.Models.MEvent", null)
                        .WithMany()
                        .HasForeignKey("EventsListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManyEvents.Models.MPerson", null)
                        .WithMany()
                        .HasForeignKey("MPersonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ManyEvents.Models.MPerson", b =>
                {
                    b.HasOne("ManyEvents.Models.MFeeType", "FeeType")
                        .WithMany()
                        .HasForeignKey("FeeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManyEvents.Models.PersonalCode", "PersonalCode")
                        .WithMany()
                        .HasForeignKey("PersonalCodeCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FeeType");

                    b.Navigation("PersonalCode");
                });
#pragma warning restore 612, 618
        }
    }
}
