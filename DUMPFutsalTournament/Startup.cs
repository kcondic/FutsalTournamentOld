using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using DUMPFutsalTournament.Data;
using DUMPFutsalTournament.Data.Entities;
using DUMPFutsalTournament.Data.Enums;
using DUMPFutsalTournament.Domain.Implementations;
using DUMPFutsalTournament.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;

namespace DUMPFutsalTournament
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public bool ReseedDb { get; set; } = true;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "Web/dist";
            });

            services.AddCors();


            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling =
                        ReferenceLoopHandling.Ignore;
                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = ConfigurationManager.AppSettings["as:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = ConfigurationManager.AppSettings["as:AudienceId"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["as:AudienceSecret"]))
                    };
                });

            services.AddDbContext<FutsalContext>(options => options.UseSqlServer(Configuration.GetConnectionString("FutsalConnection")));
            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();

            Seed();
        }

        private void Seed()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FutsalContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("FutsalConnection"));
            using (var context = new FutsalContext(optionsBuilder.Options))
            {
                if (ReseedDb)
                {
                    context.Users.RemoveRange(context.Users);
                    context.MatchEvents.RemoveRange(context.MatchEvents);
                    context.Players.RemoveRange(context.Players);
                    context.Matches.RemoveRange(context.Matches);
                    context.Teams.RemoveRange(context.Teams);
                    context.Groups.RemoveRange(context.Groups);

                    context.SaveChanges();

                    if (context.Groups.Any())
                        return;

                    var user = new User()
                    {
                        Password = "pPXzAgghcGOTapR9X4btr1mUGDLB25CLLfI52C7+SWmv8vF3",
                        Username = "dump"
                    };
                    context.Users.Add(user);

                    var groups = new List<Group>()
                    {
                        new Group() {Name = "Skupina 1", Size = 4},
                        new Group() {Name = "Skupina 2", Size = 4},
                        new Group() {Name = "Skupina 3", Size = 4},
                    };

                    var teams = new List<Team>()
                    {
                        new Team() { Name = "DUMP", Group = groups[1], Players = new List<Player>()
                        {
                            new Player() { FirstName = "Luka", LastName = "Ivanović", DateOfBirth = new DateTime(2000, 11, 24)},
                            new Player() { FirstName = "Krešimir", LastName = "Čondić", DateOfBirth = new DateTime(1996, 08, 18)},
                            new Player() { FirstName = "Toma", LastName = "Puljak", DateOfBirth = new DateTime(2000, 06, 11)},
                            new Player() { FirstName = "Duje", LastName = "Đaković", DateOfBirth = new DateTime(1995, 06, 04)},
                            new Player() { FirstName = "Noa", LastName = "Barić", DateOfBirth = new DateTime(1997, 08, 31)},
                            new Player() { FirstName = "Josip", LastName = "Svalina", DateOfBirth = new DateTime(1999, 04, 07)},
                            new Player() { FirstName = "Mario", LastName = "Čeprnja", DateOfBirth = new DateTime(1994, 04, 12)},
                            new Player() { FirstName = "Bruno", LastName = "Radan", DateOfBirth = new DateTime(1993, 04, 26)}
                        }}, //0
                        new Team() { Name = "HR Cloud", Group = groups[0], Players = new List<Player>()
                        {
                            new Player() { FirstName = "Marko", LastName = "Gojević", DateOfBirth = new DateTime(1993, 10, 17)},
                            new Player() { FirstName = "Mirko", LastName = "Bojčić", DateOfBirth = new DateTime(1994, 02, 03)},
                            new Player() { FirstName = "Marin", LastName = "Radmilović", DateOfBirth = new DateTime(1991, 06, 21)},
                            new Player() { FirstName = "Dinko", LastName = "Šimunović", DateOfBirth = new DateTime(1991, 05, 12)},
                            new Player() { FirstName = "Dujam", LastName = "Krstulović", DateOfBirth = new DateTime(1995, 03, 27)},
                            new Player() { FirstName = "Gabrijel", LastName = "Boduljak", DateOfBirth = new DateTime(1999, 04, 27)},
                            new Player() { FirstName = "Ivica", LastName = "Mihaljević", DateOfBirth = new DateTime(1994, 07, 05)},
                            new Player() { FirstName = "Antonio", LastName = "Botić", DateOfBirth = new DateTime(1992, 05, 02)},
                            new Player() { FirstName = "Matej", LastName = "Ivandić", DateOfBirth = new DateTime(1995, 11, 12)},
                            new Player() { FirstName = "Martin", LastName = "Peša", DateOfBirth = new DateTime(1981, 12, 19)},
                            new Player() { FirstName = "Antonio", LastName = "Pavlinović", DateOfBirth = new DateTime(1991, 05, 01)},
                            new Player() { FirstName = "Luka", LastName = "Vidačak", DateOfBirth = new DateTime(1989, 05, 23)}
                        }}, //1
                        new Team() { Name = "Manas", Group = groups[2], Players = new List<Player>()
                        {
                            new Player() { FirstName = "Josip", LastName = "Bilić", DateOfBirth = new DateTime(1991, 02, 16)},
                            new Player() { FirstName = "Ljubomir", LastName = "Dujmić", DateOfBirth = new DateTime(1988, 08, 11)},
                            new Player() { FirstName = "Nikola", LastName = "Jurjević-Skopinić", DateOfBirth = new DateTime(1989, 12, 21)},
                            new Player() { FirstName = "Ivan", LastName = "Čurković", DateOfBirth = new DateTime(1987, 10, 20)},
                            new Player() { FirstName = "Toni", LastName = "Erceg", DateOfBirth = new DateTime(1991, 04, 04)},
                            new Player() { FirstName = "Mario", LastName = "Batinović", DateOfBirth = new DateTime(1997, 03, 30)},
                            new Player() { FirstName = "Hrvoje-Petar", LastName = "Alajbeg", DateOfBirth = new DateTime(1997, 02, 10)},
                            new Player() { FirstName = "Ivan", LastName = "Brković", DateOfBirth = new DateTime(1989, 05, 06)},
                            new Player() { FirstName = "Grgo", LastName = "Lejo", DateOfBirth = new DateTime(1994, 10, 22)},
                            new Player() { FirstName = "Ivan", LastName = "Bralić", DateOfBirth = new DateTime(1986, 09, 12)},
                            new Player() { FirstName = "Ante", LastName = "Sladić", DateOfBirth = new DateTime(1995, 07, 17)},
                            new Player() { FirstName = "Boris", LastName = "Smolčić", DateOfBirth = new DateTime(1988, 07, 06)}
                        }}, //2 
                        new Team() { Name = "Rimac Automobili", Group = groups[1], Players = new List<Player>()
                        {
                            new Player() { FirstName = "Malik", LastName = "Jauk Suddani", DateOfBirth = new DateTime(1989, 09, 19)},
                            new Player() { FirstName = "Ivan", LastName = "Brko", DateOfBirth = new DateTime(1993, 04, 09)},
                            new Player() { FirstName = "Mate", LastName = "Granić", DateOfBirth = new DateTime(1989, 02, 28)},
                            new Player() { FirstName = "Ivan Pavao", LastName = "Lozančić", DateOfBirth = new DateTime(2002, 02, 26)},
                            new Player() { FirstName = "Vice", LastName = "Radman", DateOfBirth = new DateTime(1987, 06, 06)},
                            new Player() { FirstName = "Tomislav", LastName = "Odrljin", DateOfBirth = new DateTime(1980, 10, 02)},
                            new Player() { FirstName = "Leonard", LastName = "Blažević", DateOfBirth = new DateTime(1996, 09, 02)},
                            new Player() { FirstName = "Marin", LastName = "Milovac", DateOfBirth = new DateTime(1997, 01, 21)},
                            new Player() { FirstName = "Marin", LastName = "Kovačić", DateOfBirth = new DateTime(1984, 06, 23)},
                            new Player() { FirstName = "Miljenko", LastName = "Baković", DateOfBirth = new DateTime(1988, 05, 12)}
                        }}, //3
                        new Team() { Name = "Adriatic.hr", Group = groups[1], Players = new List<Player>()
                        {
                            new Player() { FirstName = "Jakša", LastName = "Babić", DateOfBirth = new DateTime(1976, 05, 12)},
                            new Player() { FirstName = "Mate", LastName = "Babić", DateOfBirth = new DateTime(1987, 04, 04)},
                            new Player() { FirstName = "Ivan", LastName = "Bašić", DateOfBirth = new DateTime(1986, 05, 15)},
                            new Player() { FirstName = "Ivan", LastName = "Brković", DateOfBirth = new DateTime(1992, 04, 16)},
                            new Player() { FirstName = "Duško", LastName = "Bulat", DateOfBirth = new DateTime(1995, 03, 19)},
                            new Player() { FirstName = "Anđelo", LastName = "Kačić", DateOfBirth = new DateTime(1988, 02, 13)},
                            new Player() { FirstName = "Tomislav", LastName = "Parčina", DateOfBirth = new DateTime(1980, 01, 29)},
                            new Player() { FirstName = "Roko", LastName = "Pauletić", DateOfBirth = new DateTime(1991, 06, 07)},
                            new Player() { FirstName = "Petar", LastName = "Perišić", DateOfBirth = new DateTime(1989, 10, 11)},
                            new Player() { FirstName = "Luka", LastName = "Šarlija", DateOfBirth = new DateTime(1995, 05, 13)},
                            new Player() { FirstName = "Željko", LastName = "Vranješ", DateOfBirth = new DateTime(1985, 05, 13)},
                            new Player() { FirstName = "Josip", LastName = "Žderić", DateOfBirth = new DateTime(1996, 03, 10)},
                            new Player() { FirstName = "Vanja", LastName = "Dobrijević", DateOfBirth = new DateTime(1984, 10, 18)}
                        }}, //4
                        new Team() { Name = "Hattrick", Group = groups[0], Players = new List<Player>()
                        {
                            new Player() { FirstName = "Bruno", LastName = "Reljan", DateOfBirth = new DateTime(1990, 06, 06)},
                            new Player() { FirstName = "Nikša", LastName = "Dell'Orco", DateOfBirth = new DateTime(1992, 03, 05)},
                            new Player() { FirstName = "Ivan", LastName = "Ivić", DateOfBirth = new DateTime(1992, 05, 02)},
                            new Player() { FirstName = "Davor", LastName = "Pavičević", DateOfBirth = new DateTime(1987, 10, 29)},
                            new Player() { FirstName = "Krste", LastName = "Šižgorić", DateOfBirth = new DateTime(1988, 08, 18)},
                            new Player() { FirstName = "Hrvoje", LastName = "Odak", DateOfBirth = new DateTime(1987, 04, 30)}
                        }}, //5
                        new Team() { Name = "Ericsson", Group = groups[0], Players = new List<Player>()
                        {
                            new Player() { FirstName = "Ante", LastName = "Šego", DateOfBirth = new DateTime(1985, 10, 25)},
                            new Player() { FirstName = "Marko", LastName = "Dragunić", DateOfBirth = new DateTime(1987, 04, 30)},
                            new Player() { FirstName = "Ivan", LastName = "Marušić", DateOfBirth = new DateTime(1985, 05, 05)},
                            new Player() { FirstName = "Marin", LastName = "Mihanović", DateOfBirth = new DateTime(1989, 10, 11)},
                            new Player() { FirstName = "Jerko", LastName = "Zorica", DateOfBirth = new DateTime(1989, 03, 01)},
                            new Player() { FirstName = "Vjeran", LastName = "Strniša", DateOfBirth = new DateTime(1983, 01, 31)},
                            new Player() { FirstName = "Stjepan", LastName = "Udiljak", DateOfBirth = new DateTime(1993, 02, 12)},
                            new Player() { FirstName = "Ante", LastName = "Barać", DateOfBirth = new DateTime(1992, 04, 21)},
                            new Player() { FirstName = "Ivan", LastName = "Križanović", DateOfBirth = new DateTime(1992, 12, 18)},
                            new Player() { FirstName = "Petar", LastName = "Jugović", DateOfBirth = new DateTime(1982, 10, 11)},
                            new Player() { FirstName = "Goran", LastName = "Petrović", DateOfBirth = new DateTime(1981, 09, 18)},
                            new Player() { FirstName = "Hrvoje", LastName = "Topić", DateOfBirth = new DateTime(1983, 09, 30)},
                        }}, //6
                        new Team() { Name = "Ambes", Group = groups[2], Players = new List<Player>()
                        {
                            new Player() { FirstName = "Ante", LastName = "Koceić", DateOfBirth = new DateTime(1994, 11, 21)},
                            new Player() { FirstName = "Blaž", LastName = "Majić", DateOfBirth = new DateTime(1994, 01, 30)},
                            new Player() { FirstName = "Karlo", LastName = "Mužinić", DateOfBirth = new DateTime(1996, 02, 22)},
                            new Player() { FirstName = "Ivan", LastName = "Marasović", DateOfBirth = new DateTime(1983, 09, 01)},
                            new Player() { FirstName = "Ivan", LastName = "Mravak", DateOfBirth = new DateTime(1993, 08, 11)},
                            new Player() { FirstName = "Ivan", LastName = "Milatić", DateOfBirth = new DateTime(1988, 09, 23)},
                            new Player() { FirstName = "Igor", LastName = "Miškić", DateOfBirth = new DateTime(1991, 07, 06)},
                            new Player() { FirstName = "Josip", LastName = "Labrović", DateOfBirth = new DateTime(1995, 02, 27)},
                            new Player() { FirstName = "Marko", LastName = "Jović", DateOfBirth = new DateTime(1995, 01, 29)},
                            new Player() { FirstName = "Paško", LastName = "Šalković", DateOfBirth = new DateTime(1993, 02, 20)},
                        }}, //7
                        new Team() { Name = "Poduzetnički akcelerator Split", Group = groups[2], Players = new List<Player>()
                        {
                            new Player() { FirstName = "Marino", LastName = "Bezmalinović", DateOfBirth = new DateTime(1987, 09, 04)},
                            new Player() { FirstName = "Boris", LastName = "Pekić", DateOfBirth = new DateTime(1978, 06, 26)},
                            new Player() { FirstName = "Karlo", LastName = "Barić", DateOfBirth = new DateTime(1991, 11, 04)},
                            new Player() { FirstName = "Andrea", LastName = "Omaršić", DateOfBirth = new DateTime(1987, 12, 10)},
                            new Player() { FirstName = "Robert", LastName = "Šarić", DateOfBirth = new DateTime(1992, 09, 27)},
                            new Player() { FirstName = "Ivo", LastName = "Šarić", DateOfBirth = new DateTime(1983, 05, 25)},
                            new Player() { FirstName = "Karlo", LastName = "Mrvić", DateOfBirth = new DateTime(1988, 07, 07)}
                        }}, //8
                        new Team() { Name = "Hotel's Touch", Group = groups[2], Players = new List<Player>()
                        {
                            new Player() { FirstName = "Juran", LastName = "Dadić", DateOfBirth = new DateTime(1994, 09, 15)},
                            new Player() { FirstName = "Luka", LastName = "Karan", DateOfBirth = new DateTime(1991, 06, 20)},
                            new Player() { FirstName = "Mario", LastName = "Ćibarić", DateOfBirth = new DateTime(1991, 04, 13)},
                            new Player() { FirstName = "Tomislav", LastName = "Škoda", DateOfBirth = new DateTime(1992, 04, 01)},
                            new Player() { FirstName = "Jakov", LastName = "Kusić", DateOfBirth = new DateTime(1992, 06, 23)},
                            new Player() { FirstName = "Branimir", LastName = "Lažeta", DateOfBirth = new DateTime(1994, 05, 31)},
                            new Player() { FirstName = "Nino", LastName = "Mijač", DateOfBirth = new DateTime(1988, 04, 22)},
                            new Player() { FirstName = "Ivan", LastName = "Bazina", DateOfBirth = new DateTime(1990, 03, 27)},
                            new Player() { FirstName = "Dujo", LastName = "Sarić", DateOfBirth = new DateTime(1994, 02, 24)}
                        }}, //9
                        new Team() { Name = "IT Sistemi", Group = groups[0], Players = new List<Player>()
                        {
                            new Player() { FirstName = "Ivan", LastName = "Pleština", DateOfBirth = null},
                            new Player() { FirstName = "Luka", LastName = "Jurić", DateOfBirth = null},
                            new Player() { FirstName = "Jašo", LastName = "Vrdoljak", DateOfBirth = null},
                            new Player() { FirstName = "Berislav", LastName = "Tomašić", DateOfBirth = null},
                            new Player() { FirstName = "Ante", LastName = "Maretić", DateOfBirth = null},
                            new Player() { FirstName = "Ivan", LastName = "Listeš", DateOfBirth = null},
                            new Player() { FirstName = "Ante", LastName = "Rota", DateOfBirth = null},
                            new Player() { FirstName = "Joško", LastName = "Radić", DateOfBirth = null},
                            new Player() { FirstName = "Ante", LastName = "Doljanin", DateOfBirth = null},
                            new Player() { FirstName = "Ivan", LastName = "Ožić Bebek", DateOfBirth = null},
                        }}, //10
                        new Team() { Name = "Profico", Group = groups[1], Players = new List<Player>()
                        {
                            new Player() { FirstName = "Ivan", LastName = "Ferenčak", DateOfBirth = new DateTime(1989, 08, 08)},
                            new Player() { FirstName = "Pero", LastName = "Pavlović", DateOfBirth = null},
                            new Player() { FirstName = "Miro", LastName = "Marasović", DateOfBirth = new DateTime(1993, 12, 20)},
                            new Player() { FirstName = "Nikola", LastName = "Dadić", DateOfBirth = new DateTime(1983, 06, 12)},
                            new Player() { FirstName = "Zvonimir", LastName = "Biočić", DateOfBirth = null},
                            new Player() { FirstName = "Siniša", LastName = "Marasović", DateOfBirth = null},
                            new Player() { FirstName = "Jure", LastName = "Glasović", DateOfBirth = null},
                            new Player() { FirstName = "Roko", LastName = "Čupić", DateOfBirth = null},
                            new Player() { FirstName = "Hrvoje", LastName = "Pavlinović", DateOfBirth = null}
                        }}, //11
                    };

                    var matches = new List<Match>()
                    {
                        new Match(){ HomeTeam = teams[0], AwayTeam = teams[4], MatchType = MatchType.Group, TimeOfMatch = new DateTime(2018, 08, 31, 19, 15, 0)},
                        new Match(){ HomeTeam = teams[6], AwayTeam = teams[5], MatchType = MatchType.Group, TimeOfMatch = new DateTime(2018, 08, 31, 20, 15, 0)},
                        new Match(){ HomeTeam = teams[7], AwayTeam = teams[9], MatchType = MatchType.Group, TimeOfMatch = new DateTime(2018, 08, 31, 21, 15, 0)},
                        new Match(){ HomeTeam = teams[2], AwayTeam = teams[8], MatchType = MatchType.Group, TimeOfMatch = new DateTime(2018, 08, 31, 22, 15, 0)},
                        new Match(){ HomeTeam = teams[11], AwayTeam = teams[3], MatchType = MatchType.Group, TimeOfMatch = new DateTime(2018, 09, 01, 19, 15, 0)},
                        new Match(){ HomeTeam = teams[6], AwayTeam = teams[10], MatchType = MatchType.Group, TimeOfMatch = new DateTime(2018, 09, 01, 20, 15, 0)},
                        new Match(){ HomeTeam = teams[7], AwayTeam = teams[8], MatchType = MatchType.Group, TimeOfMatch = new DateTime(2018, 09, 01, 21, 15, 0)},
                        new Match(){ HomeTeam = teams[9], AwayTeam = teams[2], MatchType = MatchType.Group, TimeOfMatch = new DateTime(2018, 09, 01, 22, 15, 0)},
                        new Match(){ HomeTeam = teams[1], AwayTeam = teams[5], MatchType = MatchType.Group, TimeOfMatch = new DateTime(2018, 09, 02, 19, 15, 0)},
                        new Match(){ HomeTeam = teams[0], AwayTeam = teams[11], MatchType = MatchType.Group, TimeOfMatch = new DateTime(2018, 09, 02, 20, 15, 0)},
                        new Match(){ HomeTeam = teams[4], AwayTeam = teams[3], MatchType = MatchType.Group, TimeOfMatch = new DateTime(2018, 09, 02, 21, 15, 0)},
                        new Match(){ HomeTeam = teams[1], AwayTeam = teams[10], MatchType = MatchType.Group, TimeOfMatch = new DateTime(2018, 09, 02, 22, 15, 0)},
                        new Match(){ HomeTeam = teams[8], AwayTeam = teams[9], MatchType = MatchType.Group, TimeOfMatch = new DateTime(2018, 09, 05, 19, 15, 0)},
                        new Match(){ HomeTeam = teams[1], AwayTeam = teams[6], MatchType = MatchType.Group, TimeOfMatch = new DateTime(2018, 09, 05, 20, 15, 0)},
                        new Match(){ HomeTeam = teams[7], AwayTeam = teams[2], MatchType = MatchType.Group, TimeOfMatch = new DateTime(2018, 09, 06, 19, 15, 0)},
                        new Match(){ HomeTeam = teams[4], AwayTeam = teams[11], MatchType = MatchType.Group, TimeOfMatch = new DateTime(2018, 09, 06, 20, 15, 0)},
                        new Match(){ HomeTeam = teams[5], AwayTeam = teams[10], MatchType = MatchType.Group, TimeOfMatch = new DateTime(2018, 09, 07, 19, 15, 0)},
                        new Match(){ HomeTeam = teams[0], AwayTeam = teams[3], MatchType = MatchType.Group, TimeOfMatch = new DateTime(2018, 09, 07, 20, 15, 0)},
                    };

                    context.Groups.AddRange(groups);
                    context.Teams.AddRange(teams);
                    context.Matches.AddRange(matches);

                    context.SaveChanges();
                }
                
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseWebSockets();
            app.UseAuthentication();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "Web";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
