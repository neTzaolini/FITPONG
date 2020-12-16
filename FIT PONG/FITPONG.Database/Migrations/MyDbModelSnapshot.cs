﻿// <auto-generated />
using System;
using FIT_PONG.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FIT_PONG.Migrations
{
    [DbContext(typeof(MyDb))]
    partial class MyDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Attachment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumUnosa");

                    b.Property<string>("Path");

                    b.Property<int?>("ReportID");

                    b.HasKey("ID");

                    b.HasIndex("ReportID");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.BlokLista", b =>
                {
                    b.Property<int>("IgracID");

                    b.Property<int>("TakmicenjeID");

                    b.HasKey("IgracID", "TakmicenjeID");

                    b.HasIndex("TakmicenjeID");

                    b.ToTable("BlokListe");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Bracket", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .HasMaxLength(110);

                    b.Property<int>("TakmicenjeID");

                    b.HasKey("ID");

                    b.HasIndex("TakmicenjeID");

                    b.ToTable("Brackets");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.BrojKorisnikaLog", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrojKorisnika");

                    b.Property<DateTime>("Datum");

                    b.Property<int>("MaxBrojKorisnika");

                    b.HasKey("ID");

                    b.ToTable("BrojKorisnikaLog");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Favoriti", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UserID");

                    b.Property<int>("UtakmicaId");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.HasIndex("UtakmicaId");

                    b.ToTable("Favoriti");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Feed", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumModifikacije");

                    b.Property<string>("Naziv");

                    b.HasKey("ID");

                    b.ToTable("Feeds");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.FeedObjava", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FeedID");

                    b.Property<int>("ObjavaID");

                    b.HasKey("ID");

                    b.HasIndex("FeedID");

                    b.HasIndex("ObjavaID");

                    b.ToTable("FeedsObjave");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Grad", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.HasKey("ID");

                    b.ToTable("Gradovi");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Igrac", b =>
                {
                    b.Property<int>("ID");

                    b.Property<int>("BrojPosjetaNaProfil");

                    b.Property<int>("ELO");

                    b.Property<int?>("GradID");

                    b.Property<string>("JacaRuka")
                        .HasMaxLength(8);

                    b.Property<string>("PrikaznoIme")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("ProfileImagePath");

                    b.Property<string>("Spol")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)));

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<double?>("Visina");

                    b.HasKey("ID");

                    b.HasIndex("GradID");

                    b.ToTable("Igraci");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Igrac_Utakmica", b =>
                {
                    b.Property<int>("IgID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("IgracID");

                    b.Property<int?>("OsvojeniSetovi");

                    b.Property<int?>("PristupniElo");

                    b.Property<int?>("TipRezultataID");

                    b.Property<int>("UtakmicaID");

                    b.HasKey("IgID");

                    b.HasIndex("IgracID");

                    b.HasIndex("TipRezultataID");

                    b.HasIndex("UtakmicaID");

                    b.ToTable("IgraciUtakmice");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Kategorija", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Opis")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Kategorije");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Objava", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("DatumIzmjene");

                    b.Property<DateTime>("DatumKreiranja");

                    b.Property<string>("Naziv");

                    b.HasKey("ID");

                    b.ToTable("Objave");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Postovanje", b =>
                {
                    b.Property<int>("PostivalacID");

                    b.Property<int>("PostovaniID");

                    b.HasKey("PostivalacID", "PostovaniID");

                    b.HasIndex("PostovaniID");

                    b.ToTable("Postovanja");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Prijava", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumPrijave");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("TakmicenjeID");

                    b.Property<bool>("isTim");

                    b.HasKey("ID");

                    b.HasIndex("TakmicenjeID");

                    b.ToTable("Prijave");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Prijava_igrac", b =>
                {
                    b.Property<int>("IgracID");

                    b.Property<int>("PrijavaID");

                    b.HasKey("IgracID", "PrijavaID");

                    b.HasIndex("PrijavaID");

                    b.ToTable("PrijaveIgraci");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Report", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumKreiranja");

                    b.Property<string>("Email");

                    b.Property<string>("Naslov");

                    b.Property<string>("Sadrzaj");

                    b.HasKey("ID");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Runda", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BracketID");

                    b.Property<int>("BrojRunde");

                    b.Property<DateTime>("DatumPocetka");

                    b.HasKey("ID");

                    b.HasIndex("BracketID");

                    b.ToTable("Runde");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Sistem_Takmicenja", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Opis")
                        .HasMaxLength(40);

                    b.HasKey("ID");

                    b.ToTable("SistemiTakmicenja");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Stanje_Prijave", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrojBodova");

                    b.Property<int>("BrojIzgubljenih");

                    b.Property<int>("BrojOdigranihMeceva");

                    b.Property<int>("BrojPobjeda");

                    b.Property<int>("PrijavaID");

                    b.HasKey("ID");

                    b.HasIndex("PrijavaID")
                        .IsUnique();

                    b.ToTable("StanjaPrijave");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Statistika", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AkademskaGodina");

                    b.Property<int>("BrojOdigranihMeceva");

                    b.Property<int>("BrojOsvojenihLiga");

                    b.Property<int>("BrojOsvojenihTurnira");

                    b.Property<int>("BrojSinglePobjeda");

                    b.Property<int>("BrojTimskihPobjeda");

                    b.Property<int>("IgracID");

                    b.Property<int>("NajveciPobjednickiNiz");

                    b.HasKey("ID");

                    b.HasIndex("IgracID");

                    b.ToTable("Statistike");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Status_Takmicenja", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Opis")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("StatusiTakmicenja");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Status_Utakmice", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Opis")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("StatusiUtakmice");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Suspenzija", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumPocetka");

                    b.Property<DateTime>("DatumZavrsetka");

                    b.Property<int>("IgracID");

                    b.Property<int>("VrstaSuspenzijeID");

                    b.HasKey("ID");

                    b.HasIndex("IgracID");

                    b.HasIndex("VrstaSuspenzijeID");

                    b.ToTable("Suspenzije");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Takmicenje", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BrojRundi");

                    b.Property<DateTime>("DatumKreiranja");

                    b.Property<DateTime?>("DatumPocetka");

                    b.Property<DateTime?>("DatumZavrsetka");

                    b.Property<int>("FeedID");

                    b.Property<bool>("Inicirano");

                    b.Property<int>("KategorijaID");

                    b.Property<int>("KreatorID");

                    b.Property<int>("MinimalniELO");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime?>("RokPocetkaPrijave");

                    b.Property<DateTime?>("RokZavrsetkaPrijave");

                    b.Property<bool>("Seeded");

                    b.Property<int>("SistemID");

                    b.Property<int>("StatusID");

                    b.Property<int>("VrstaID");

                    b.HasKey("ID");

                    b.HasIndex("FeedID");

                    b.HasIndex("KategorijaID");

                    b.HasIndex("KreatorID");

                    b.HasIndex("SistemID");

                    b.HasIndex("StatusID");

                    b.HasIndex("VrstaID");

                    b.ToTable("Takmicenja");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Tip_Rezultata", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("TipoviRezultata");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Tip_Utakmice", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("TipoviUtakmica");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Utakmica", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrojUtakmice");

                    b.Property<DateTime>("DatumVrijeme");

                    b.Property<bool>("IsEvidentirana");

                    b.Property<string>("Rezultat")
                        .HasMaxLength(10);

                    b.Property<int>("RundaID");

                    b.Property<int>("StatusID");

                    b.Property<int>("TipUtakmiceID");

                    b.HasKey("ID");

                    b.HasIndex("RundaID");

                    b.HasIndex("StatusID");

                    b.HasIndex("TipUtakmiceID");

                    b.ToTable("Utakmice");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.VrstaSuspenzije", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Opis");

                    b.HasKey("ID");

                    b.ToTable("VrsteSuspenzije");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Vrsta_Takmicenja", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("VrsteTakmicenja");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

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
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Attachment", b =>
                {
                    b.HasOne("FIT_PONG.Database.DTOs.Report")
                        .WithMany("Prilozi")
                        .HasForeignKey("ReportID");
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.BlokLista", b =>
                {
                    b.HasOne("FIT_PONG.Database.DTOs.Igrac", "Igrac")
                        .WithMany()
                        .HasForeignKey("IgracID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FIT_PONG.Database.DTOs.Takmicenje", "Takmicenje")
                        .WithMany()
                        .HasForeignKey("TakmicenjeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Bracket", b =>
                {
                    b.HasOne("FIT_PONG.Database.DTOs.Takmicenje", "Takmicenje")
                        .WithMany("Bracketi")
                        .HasForeignKey("TakmicenjeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Favoriti", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<int>", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FIT_PONG.Database.DTOs.Utakmica", "Utakmica")
                        .WithMany()
                        .HasForeignKey("UtakmicaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.FeedObjava", b =>
                {
                    b.HasOne("FIT_PONG.Database.DTOs.Feed", "Feed")
                        .WithMany()
                        .HasForeignKey("FeedID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FIT_PONG.Database.DTOs.Objava", "Objava")
                        .WithMany()
                        .HasForeignKey("ObjavaID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Igrac", b =>
                {
                    b.HasOne("FIT_PONG.Database.DTOs.Grad", "Grad")
                        .WithMany()
                        .HasForeignKey("GradID");

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<int>", "User")
                        .WithOne()
                        .HasForeignKey("FIT_PONG.Database.DTOs.Igrac", "ID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Igrac_Utakmica", b =>
                {
                    b.HasOne("FIT_PONG.Database.DTOs.Igrac", "Igrac")
                        .WithMany()
                        .HasForeignKey("IgracID");

                    b.HasOne("FIT_PONG.Database.DTOs.Tip_Rezultata", "TipRezultata")
                        .WithMany()
                        .HasForeignKey("TipRezultataID");

                    b.HasOne("FIT_PONG.Database.DTOs.Utakmica", "Utakmica")
                        .WithMany("UcescaNaUtakmici")
                        .HasForeignKey("UtakmicaID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Postovanje", b =>
                {
                    b.HasOne("FIT_PONG.Database.DTOs.Igrac", "Postivalac")
                        .WithMany()
                        .HasForeignKey("PostivalacID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FIT_PONG.Database.DTOs.Igrac", "Postovani")
                        .WithMany()
                        .HasForeignKey("PostovaniID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Prijava", b =>
                {
                    b.HasOne("FIT_PONG.Database.DTOs.Takmicenje", "Takmicenje")
                        .WithMany("Prijave")
                        .HasForeignKey("TakmicenjeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Prijava_igrac", b =>
                {
                    b.HasOne("FIT_PONG.Database.DTOs.Igrac", "Igrac")
                        .WithMany()
                        .HasForeignKey("IgracID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FIT_PONG.Database.DTOs.Prijava", "Prijava")
                        .WithMany()
                        .HasForeignKey("PrijavaID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Runda", b =>
                {
                    b.HasOne("FIT_PONG.Database.DTOs.Bracket", "Bracket")
                        .WithMany("Runde")
                        .HasForeignKey("BracketID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Stanje_Prijave", b =>
                {
                    b.HasOne("FIT_PONG.Database.DTOs.Prijava", "Prijava")
                        .WithOne("StanjePrijave")
                        .HasForeignKey("FIT_PONG.Database.DTOs.Stanje_Prijave", "PrijavaID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Statistika", b =>
                {
                    b.HasOne("FIT_PONG.Database.DTOs.Igrac", "Igrac")
                        .WithMany()
                        .HasForeignKey("IgracID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Suspenzija", b =>
                {
                    b.HasOne("FIT_PONG.Database.DTOs.Igrac", "Igrac")
                        .WithMany()
                        .HasForeignKey("IgracID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FIT_PONG.Database.DTOs.VrstaSuspenzije", "VrstaSuspenzije")
                        .WithMany()
                        .HasForeignKey("VrstaSuspenzijeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Takmicenje", b =>
                {
                    b.HasOne("FIT_PONG.Database.DTOs.Feed", "Feed")
                        .WithMany()
                        .HasForeignKey("FeedID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FIT_PONG.Database.DTOs.Kategorija", "Kategorija")
                        .WithMany()
                        .HasForeignKey("KategorijaID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FIT_PONG.Database.DTOs.Igrac", "Kreator")
                        .WithMany()
                        .HasForeignKey("KreatorID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FIT_PONG.Database.DTOs.Sistem_Takmicenja", "Sistem")
                        .WithMany()
                        .HasForeignKey("SistemID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FIT_PONG.Database.DTOs.Status_Takmicenja", "Status")
                        .WithMany()
                        .HasForeignKey("StatusID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FIT_PONG.Database.DTOs.Vrsta_Takmicenja", "Vrsta")
                        .WithMany()
                        .HasForeignKey("VrstaID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FIT_PONG.Database.DTOs.Utakmica", b =>
                {
                    b.HasOne("FIT_PONG.Database.DTOs.Runda", "Runda")
                        .WithMany("Utakmice")
                        .HasForeignKey("RundaID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FIT_PONG.Database.DTOs.Status_Utakmice", "Status")
                        .WithMany()
                        .HasForeignKey("StatusID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FIT_PONG.Database.DTOs.Tip_Utakmice", "TipUtakmice")
                        .WithMany()
                        .HasForeignKey("TipUtakmiceID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<int>")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<int>")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<int>")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<int>")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
