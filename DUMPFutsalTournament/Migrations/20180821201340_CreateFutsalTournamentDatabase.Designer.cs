﻿// <auto-generated />
using System;
using DUMPFutsalTournament.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DUMPFutsalTournament.Migrations
{
    [DbContext(typeof(FutsalContext))]
    [Migration("20180821201340_CreateFutsalTournamentDatabase")]
    partial class CreateFutsalTournamentDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DUMPFutsalTournament.Data.Entities.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("Size");

                    b.HasKey("GroupId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("DUMPFutsalTournament.Data.Entities.Match", b =>
                {
                    b.Property<int>("MatchId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AwayGoals");

                    b.Property<int?>("AwayTeamTeamId");

                    b.Property<int>("HomeGoals");

                    b.Property<int?>("HomeTeamTeamId");

                    b.Property<int>("MatchType");

                    b.Property<DateTime>("TimeOfMatch");

                    b.HasKey("MatchId");

                    b.HasIndex("AwayTeamTeamId");

                    b.HasIndex("HomeTeamTeamId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("DUMPFutsalTournament.Data.Entities.MatchEvent", b =>
                {
                    b.Property<int>("MatchEventId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EventMinute");

                    b.Property<int>("EventType");

                    b.Property<int?>("MatchId");

                    b.Property<int?>("PlayerId");

                    b.HasKey("MatchEventId");

                    b.HasIndex("MatchId");

                    b.HasIndex("PlayerId");

                    b.ToTable("MatchEvents");
                });

            modelBuilder.Entity("DUMPFutsalTournament.Data.Entities.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int?>("TeamId");

                    b.HasKey("PlayerId");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("DUMPFutsalTournament.Data.Entities.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GroupId");

                    b.Property<string>("Name");

                    b.HasKey("TeamId");

                    b.HasIndex("GroupId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("DUMPFutsalTournament.Data.Entities.Match", b =>
                {
                    b.HasOne("DUMPFutsalTournament.Data.Entities.Team", "AwayTeam")
                        .WithMany("AwayMatches")
                        .HasForeignKey("AwayTeamTeamId");

                    b.HasOne("DUMPFutsalTournament.Data.Entities.Team", "HomeTeam")
                        .WithMany("HomeMatches")
                        .HasForeignKey("HomeTeamTeamId");
                });

            modelBuilder.Entity("DUMPFutsalTournament.Data.Entities.MatchEvent", b =>
                {
                    b.HasOne("DUMPFutsalTournament.Data.Entities.Match", "Match")
                        .WithMany()
                        .HasForeignKey("MatchId");

                    b.HasOne("DUMPFutsalTournament.Data.Entities.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("DUMPFutsalTournament.Data.Entities.Player", b =>
                {
                    b.HasOne("DUMPFutsalTournament.Data.Entities.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("DUMPFutsalTournament.Data.Entities.Team", b =>
                {
                    b.HasOne("DUMPFutsalTournament.Data.Entities.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");
                });
#pragma warning restore 612, 618
        }
    }
}
