﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrainTicketProjectwithBlazor.Shared.Models;

#nullable disable

namespace TrainTicketProjectwithBlazor.Server.Migrations
{
    [DbContext(typeof(TrainDbContext))]
    partial class TrainDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TrainTicketProjectwithBlazor.Shared.Models.Passenger", b =>
                {
                    b.Property<int>("PassengerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PassengerId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PassengerName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PassengerId");

                    b.ToTable("Passengers");
                });

            modelBuilder.Entity("TrainTicketProjectwithBlazor.Shared.Models.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketId"));

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("FromStation")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<DateTime?>("JourneyDate")
                        .IsRequired()
                        .HasColumnType("date");

                    b.Property<int>("PassengerId")
                        .HasColumnType("int");

                    b.Property<string>("ToStation")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("TicketId");

                    b.HasIndex("PassengerId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("TrainTicketProjectwithBlazor.Shared.Models.TicketItem", b =>
                {
                    b.Property<int>("TicketId")
                        .HasColumnType("int");

                    b.Property<int>("TrainId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("TicketId", "TrainId");

                    b.HasIndex("TrainId");

                    b.ToTable("TicketItems");
                });

            modelBuilder.Entity("TrainTicketProjectwithBlazor.Shared.Models.Train", b =>
                {
                    b.Property<int>("TrainId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TrainId"));

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<decimal>("TicketPrice")
                        .HasColumnType("money");

                    b.Property<string>("TrainName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TrainId");

                    b.ToTable("Trains");
                });

            modelBuilder.Entity("TrainTicketProjectwithBlazor.Shared.Models.Ticket", b =>
                {
                    b.HasOne("TrainTicketProjectwithBlazor.Shared.Models.Passenger", "Passenger")
                        .WithMany("Tickets")
                        .HasForeignKey("PassengerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Passenger");
                });

            modelBuilder.Entity("TrainTicketProjectwithBlazor.Shared.Models.TicketItem", b =>
                {
                    b.HasOne("TrainTicketProjectwithBlazor.Shared.Models.Ticket", "Ticket")
                        .WithMany("TicketItems")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrainTicketProjectwithBlazor.Shared.Models.Train", "Train")
                        .WithMany("TicketItems")
                        .HasForeignKey("TrainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ticket");

                    b.Navigation("Train");
                });

            modelBuilder.Entity("TrainTicketProjectwithBlazor.Shared.Models.Passenger", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("TrainTicketProjectwithBlazor.Shared.Models.Ticket", b =>
                {
                    b.Navigation("TicketItems");
                });

            modelBuilder.Entity("TrainTicketProjectwithBlazor.Shared.Models.Train", b =>
                {
                    b.Navigation("TicketItems");
                });
#pragma warning restore 612, 618
        }
    }
}
