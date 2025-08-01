﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RSQR.Data;

#nullable disable

namespace RSQR.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250723161955_thirdStepData")]
    partial class thirdStepData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("RSQR.Models.ConsecutivoArchivo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Anio")
                        .HasColumnType("int");

                    b.Property<string>("CodigoNegocio")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("Consecutivo")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("UnidadNegocio")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("ConsecutivosArchivos");
                });

            modelBuilder.Entity("RSQR.Models.PpmReport", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Comentarios")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CuantosP")
                        .HasColumnType("int");

                    b.Property<string>("Customer")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("CustomerClaimNumber")
                        .HasColumnType("int");

                    b.Property<string>("CustomerPartNumber")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("DateOfClose")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<bool>("ImpactoPPM")
                        .HasColumnType("bit");

                    b.Property<string>("InvestigationReport")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Linea")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mileage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotherFactory")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NumParteAfectado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Responsabilidad")
                        .HasColumnType("int");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.Property<string>("TituloProblema")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("qcPpmReport", (string)null);
                });

            modelBuilder.Entity("RSQR.Models.Reporte", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AP_NPR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("AlertaCalidad")
                        .HasColumnType("bit");

                    b.Property<string>("Approval")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CincoM")
                        .HasColumnType("int");

                    b.Property<string>("Comentarios")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ComoP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContencionActivity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContencionConsiderar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContencionEffectiveness")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContencionItems")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContencionNgParts")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContencionOkParts")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContencionResponsable")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContencionTotalSuspeciousParts")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ControlesEstablecidos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CuandoP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CuantosP")
                        .HasColumnType("int");

                    b.Property<string>("Customer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerClaimNumber")
                        .HasColumnType("int");

                    b.Property<string>("CustomerPartNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerReport")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("D7D8ActualClosingDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("D7D8CodeDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("D7D8Comments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("D7D8Deadline")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("D7D8Documentation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("D7D8Responsible")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("D7D8Sn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfClose")
                        .HasColumnType("datetime2");

                    b.Property<string>("DefectoDuplicado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescripcionProblema")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Deteccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetectionAction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetectionAmef")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetectionCEnvironment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetectionCEnvironmentC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetectionCMachinary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetectionCMachinaryC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetectionCManpower")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetectionCManpowerC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetectionCMaterial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetectionCMaterialC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetectionCMeasurement")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetectionCMeasurementC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetectionCMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetectionCMethodC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetectionCProcessName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetectionCProcessNameC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetectionCRank")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetectionCRankC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetectionCloseDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetectionCp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetectionDepartment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetectionItems")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetectionOpeningDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DetectionResponsable")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Disposicion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DondeP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EntrevistaInvolucrados")
                        .HasColumnType("bit");

                    b.Property<string>("EvidenciaFotografica")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactorDos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactorDosCorrectiveActions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactorDosCuartoWhy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactorDosPrimerWhy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactorDosQuintoWhy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactorDosSegundoWhy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactorDosTercerWhy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactorTres")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactorTresCorrectiveActions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactorTresCuartoWhy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactorTresPrimerWhy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactorTresQuintoWhy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactorTresSegundoWhy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactorTresTercerWhy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactorUno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactorUnoCorrectiveActions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactorUnoCuartoWhy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactorUnoPrimerWhy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactorUnoQuintoWhy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactorUnoSegundoWhy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FactorUnoTercerWhy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Fm6Ms")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FmContentionActions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FmFactorDos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FmFactorTres")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FmFactorUno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FmMode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FmProcessName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FmRelated")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ImpactoPPM")
                        .HasColumnType("bit");

                    b.Property<string>("InvestigationReport")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Linea")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mileage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModoFalla")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotherFactory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreCar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumParteAfectado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OccurrenceAction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OccurrenceAmef")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OccurrenceCloseDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OccurrenceCp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OccurrenceDepartment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OccurrenceItems")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OccurrenceOpeningDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OccurrenceResponsable")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ocurrencia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PorqueP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreventiveCEnvironment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreventiveCEnvironmentC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreventiveCMachinary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreventiveCMachinaryC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreventiveCManpower")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreventiveCManpowerC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreventiveCMaterial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreventiveCMaterialC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreventiveCMeasurement")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreventiveCMeasurementC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreventiveCMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreventiveCMethodC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreventiveCProcessName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreventiveCProcessNameC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreventiveCRank")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreventiveCRankC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProblemRank")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QueP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuienP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Responsabilidad")
                        .HasColumnType("int");

                    b.Property<string>("Severidad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.Property<string>("TituloProblema")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerDetectionAction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerDetectionAmef")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerDetectionCloseDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerDetectionCp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerDetectionDepartment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerDetectionItems")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerDetectionOpeningDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerDetectionResponsable")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerOccurrenceAction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerOccurrenceAmef")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerOccurrenceCloseDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerOccurrenceCp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerOccurrenceDepartment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerOccurrenceItems")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerOccurrenceOpeningDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerOccurrenceResponsable")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("qcReport", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RSQR.Models.PpmReport", b =>
                {
                    b.HasOne("RSQR.Models.Reporte", null)
                        .WithOne("PpmReport")
                        .HasForeignKey("RSQR.Models.PpmReport", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RSQR.Models.Reporte", b =>
                {
                    b.Navigation("PpmReport");
                });
#pragma warning restore 612, 618
        }
    }
}
