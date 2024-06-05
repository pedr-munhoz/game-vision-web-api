﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using game_vision_web_api.Infrastructure.Database;

#nullable disable

namespace game_vision_web_api.Migrations
{
    [DbContext(typeof(GameVisionDbContext))]
    [Migration("20240605032853_AddTeamConcept")]
    partial class AddTeamConcept
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("game_vision_web_api.Models.Entities.Game", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("TeamId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("game_vision_web_api.Models.Entities.Play", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Defense")
                        .HasColumnType("text");

                    b.Property<string>("DefensiveFormation")
                        .HasColumnType("text");

                    b.Property<string>("DefensiveNotes")
                        .HasColumnType("text");

                    b.Property<string>("DefensivePlay")
                        .HasColumnType("text");

                    b.Property<string>("DefensiveTarget")
                        .HasColumnType("text");

                    b.Property<int?>("Distance")
                        .HasColumnType("integer");

                    b.Property<int?>("Down")
                        .HasColumnType("integer");

                    b.Property<string>("FileId")
                        .HasColumnType("text");

                    b.Property<bool>("FirstDown")
                        .HasColumnType("boolean");

                    b.Property<long>("GameId")
                        .HasColumnType("bigint");

                    b.Property<string>("Goal")
                        .HasColumnType("text");

                    b.Property<string>("Interceptor")
                        .HasColumnType("text");

                    b.Property<string>("OfensiveNotes")
                        .HasColumnType("text");

                    b.Property<string>("Offense")
                        .HasColumnType("text");

                    b.Property<string>("OffensiveFormation")
                        .HasColumnType("text");

                    b.Property<string>("OffensivePlay")
                        .HasColumnType("text");

                    b.Property<string>("Passer")
                        .HasColumnType("text");

                    b.Property<string>("Penalty")
                        .HasColumnType("text");

                    b.Property<int?>("PlayNumber")
                        .HasColumnType("integer");

                    b.Property<string>("Result")
                        .HasColumnType("text");

                    b.Property<string>("Runner")
                        .HasColumnType("text");

                    b.Property<bool>("Safety")
                        .HasColumnType("boolean");

                    b.Property<string>("Situation")
                        .HasColumnType("text");

                    b.Property<string>("Tackler")
                        .HasColumnType("text");

                    b.Property<string>("Target")
                        .HasColumnType("text");

                    b.Property<string>("TargetPosition")
                        .HasColumnType("text");

                    b.Property<bool>("Touchdown")
                        .HasColumnType("boolean");

                    b.Property<int?>("Yards")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Plays");
                });

            modelBuilder.Entity("game_vision_web_api.Models.Entities.Team", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Prefix")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("game_vision_web_api.Models.Entities.Game", b =>
                {
                    b.HasOne("game_vision_web_api.Models.Entities.Team", "Team")
                        .WithMany("Games")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("game_vision_web_api.Models.Entities.Play", b =>
                {
                    b.HasOne("game_vision_web_api.Models.Entities.Game", "Game")
                        .WithMany("Plays")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("game_vision_web_api.Models.Entities.Game", b =>
                {
                    b.Navigation("Plays");
                });

            modelBuilder.Entity("game_vision_web_api.Models.Entities.Team", b =>
                {
                    b.Navigation("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
