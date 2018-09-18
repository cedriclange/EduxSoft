﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftinuxBase.WebApplication.Migrations
{
    [DbContext(typeof(ApplicationStorageContext))]
    [Migration("20180611125818_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799");

            modelBuilder.Entity("Chinook.Data.Entities.Album", b =>
                {
                    b.Property<int>("AlbumId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArtistId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(160);

                    b.HasKey("AlbumId");

                    b.HasIndex("ArtistId");

                    b.ToTable("Album");
                });

            modelBuilder.Entity("Chinook.Data.Entities.Artist", b =>
                {
                    b.Property<int>("ArtistId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120);

                    b.HasKey("ArtistId");

                    b.ToTable("Artist");
                });

            modelBuilder.Entity("Chinook.Data.Entities.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(70);

                    b.Property<string>("City")
                        .HasMaxLength(40);

                    b.Property<string>("Compagny")
                        .HasMaxLength(80);

                    b.Property<string>("Country")
                        .HasMaxLength(40);

                    b.Property<string>("Email")
                        .HasMaxLength(60);

                    b.Property<string>("Fax")
                        .HasMaxLength(24);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Phone")
                        .HasMaxLength(24);

                    b.Property<string>("PostalCode")
                        .HasMaxLength(10);

                    b.Property<string>("State")
                        .HasMaxLength(40);

                    b.Property<int?>("SupportRepId");

                    b.HasKey("CustomerId");

                    b.HasIndex("SupportRepId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Chinook.Data.Entities.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(70);

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("City")
                        .HasMaxLength(40);

                    b.Property<string>("Country")
                        .HasMaxLength(40);

                    b.Property<string>("Email")
                        .HasMaxLength(60);

                    b.Property<string>("Fax")
                        .HasMaxLength(24);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTime>("HireDate");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Phone")
                        .HasMaxLength(24);

                    b.Property<string>("PostalCode")
                        .HasMaxLength(10);

                    b.Property<int>("ReportsTo");

                    b.Property<string>("State")
                        .HasMaxLength(40);

                    b.Property<string>("Title")
                        .HasMaxLength(30);

                    b.HasKey("EmployeeId");

                    b.HasIndex("ReportsTo");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("Chinook.Data.Entities.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(120);

                    b.HasKey("GenreId");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("Chinook.Data.Entities.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BillingAddress")
                        .HasMaxLength(70);

                    b.Property<string>("BillingCity")
                        .HasMaxLength(40);

                    b.Property<string>("BillingCountry")
                        .HasMaxLength(40);

                    b.Property<string>("BillingPostalCode")
                        .HasMaxLength(10);

                    b.Property<string>("BillingState")
                        .HasMaxLength(40);

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("InvoiceDate");

                    b.Property<decimal>("Total");

                    b.HasKey("InvoiceId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("Chinook.Data.Entities.InvoiceLine", b =>
                {
                    b.Property<int>("InvoiceLineId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerId");

                    b.Property<int>("Quantity");

                    b.Property<int>("TrackId");

                    b.Property<decimal>("UnitPrice");

                    b.HasKey("InvoiceLineId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("TrackId");

                    b.ToTable("InvoiceLine");
                });

            modelBuilder.Entity("Chinook.Data.Entities.MediaType", b =>
                {
                    b.Property<int>("MediaTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(120);

                    b.HasKey("MediaTypeId");

                    b.ToTable("MediaType");
                });

            modelBuilder.Entity("Chinook.Data.Entities.Playlist", b =>
                {
                    b.Property<int>("PlaylistId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120);

                    b.HasKey("PlaylistId");

                    b.ToTable("Playlist");
                });

            modelBuilder.Entity("Chinook.Data.Entities.PlaylistTrack", b =>
                {
                    b.Property<int>("PlaylistId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("TrackId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("PlaylistId");

                    b.HasAlternateKey("TrackId");


                    b.HasAlternateKey("PlaylistId", "TrackId");

                    b.ToTable("PlaylistTrack");
                });

            modelBuilder.Entity("Chinook.Data.Entities.Track", b =>
                {
                    b.Property<int>("TrackId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AlbumId");

                    b.Property<int>("Bytes");

                    b.Property<string>("Composer")
                        .HasMaxLength(220);

                    b.Property<int>("GenreId");

                    b.Property<int>("MediaTypeId");

                    b.Property<int>("Miliseconds");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<decimal>("UnitPrice");

                    b.HasKey("TrackId");

                    b.HasIndex("AlbumId");

                    b.HasIndex("GenreId");

                    b.HasIndex("MediaTypeId");

                    b.ToTable("Track");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<string>", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Security.Data.Entities.Permission", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("Security.Data.Entities.RolePermission", b =>
                {
                    b.Property<string>("RoleId");

                    b.Property<string>("PermissionId");

                    b.Property<string>("Scope");

                    b.HasKey("RoleId", "PermissionId", "Scope");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermission");
                });

            modelBuilder.Entity("Security.Data.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<DateTime>("FirstConnection");

                    b.Property<string>("FirstName");

                    b.Property<DateTime>("LastConnection");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Security.Data.Entities.UserPermission", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("PermissionId");

                    b.Property<string>("Scope");

                    b.HasKey("UserId", "PermissionId", "Scope");

                    b.HasIndex("PermissionId");

                    b.ToTable("UserPermission");
                });

            modelBuilder.Entity("Chinook.Data.Entities.Album", b =>
                {
                    b.HasOne("Chinook.Data.Entities.Artist", "artist")
                        .WithMany()
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Chinook.Data.Entities.Customer", b =>
                {
                    b.HasOne("Chinook.Data.Entities.Employee", "SupportRep")
                        .WithMany()
                        .HasForeignKey("SupportRepId");
                });

            modelBuilder.Entity("Chinook.Data.Entities.Employee", b =>
                {
                    b.HasOne("Chinook.Data.Entities.Employee", "ReportsToManager")
                        .WithMany()
                        .HasForeignKey("ReportsTo")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Chinook.Data.Entities.Invoice", b =>
                {
                    b.HasOne("Chinook.Data.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Chinook.Data.Entities.InvoiceLine", b =>
                {
                    b.HasOne("Chinook.Data.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Chinook.Data.Entities.Track", "Track")
                        .WithMany()
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Chinook.Data.Entities.PlaylistTrack", b =>
                {
                    b.HasOne("Chinook.Data.Entities.Playlist", "Playlist")
                        .WithMany()
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Chinook.Data.Entities.Track", "Track")
                        .WithMany()
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Chinook.Data.Entities.Track", b =>
                {
                    b.HasOne("Chinook.Data.Entities.Album", "Album")
                        .WithMany()
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Chinook.Data.Entities.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Chinook.Data.Entities.MediaType", "MediaType")
                        .WithMany()
                        .HasForeignKey("MediaTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<string>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Security.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Security.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<string>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Security.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Security.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Security.Data.Entities.RolePermission", b =>
                {
                    b.HasOne("Security.Data.Entities.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<string>", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Security.Data.Entities.UserPermission", b =>
                {
                    b.HasOne("Security.Data.Entities.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Security.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
