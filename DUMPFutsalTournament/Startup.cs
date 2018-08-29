using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DUMPFutsalTournament
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "Web/dist";
            });

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
                if (context.Groups.Any())
                    return;

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
                    }}, //0
                    new Team() { Name = "HRCloud", Group = groups[0], Players = new List<Player>()
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
                    new Team() { Name = "Poduzetnički Inkubator", Group = groups[2], Players = new List<Player>()
                    {
                        new Player() { FirstName = "AAH", LastName = "A", DateOfBirth = new DateTime(1994, 11, 21)},
                        new Player() { FirstName = "BAH", LastName = "BN", DateOfBirth = new DateTime(1994, 01, 30)},
                        new Player() { FirstName = "KAHo", LastName = "AFASFFAS", DateOfBirth = new DateTime(1996, 02, 22)},
                        new Player() { FirstName = "IAH", LastName = "ASFASFSA", DateOfBirth = new DateTime(1983, 09, 01)},
                        new Player() { FirstName = "IAH", LastName = "AFSA", DateOfBirth = new DateTime(1993, 08, 11)},
                        new Player() { FirstName = "IAH", LastName = "ASA", DateOfBirth = new DateTime(1988, 09, 23)},
                        new Player() { FirstName = "IAH", LastName = "TQA", DateOfBirth = new DateTime(1991, 07, 06)},
                        new Player() { FirstName = "JAHp", LastName = "R", DateOfBirth = new DateTime(1995, 02, 27)},
                        new Player() { FirstName = "MAHo", LastName = "D", DateOfBirth = new DateTime(1995, 01, 29)},
                        new Player() { FirstName = "PAHo", LastName = "C", DateOfBirth = new DateTime(1993, 02, 20)},
                    }}, //8
                    new Team() { Name = "Hotel", Group = groups[2], Players = new List<Player>()
                    {
                        new Player() { FirstName = "AAA", LastName = "JJJJ", DateOfBirth = new DateTime(1994, 11, 21)},
                        new Player() { FirstName = "BB", LastName = "III", DateOfBirth = new DateTime(1994, 01, 30)},
                        new Player() { FirstName = "NNNN", LastName = "HHHH", DateOfBirth = new DateTime(1996, 02, 22)},
                        new Player() { FirstName = "MNNN", LastName = "GGGG", DateOfBirth = new DateTime(1983, 09, 01)},
                        new Player() { FirstName = "JJJJ", LastName = "FFFF", DateOfBirth = new DateTime(1993, 08, 11)},
                        new Player() { FirstName = "HHHH", LastName = "EEE", DateOfBirth = new DateTime(1988, 09, 23)},
                        new Player() { FirstName = "GGGG", LastName = "DDDD", DateOfBirth = new DateTime(1991, 07, 06)},
                        new Player() { FirstName = "FFF", LastName = "CCCC", DateOfBirth = new DateTime(1995, 02, 27)},
                        new Player() { FirstName = "EEEE", LastName = "BBBB", DateOfBirth = new DateTime(1995, 01, 29)},
                        new Player() { FirstName = "DDD", LastName = "AAAA", DateOfBirth = new DateTime(1993, 02, 20)},
                    }}, //9
                    new Team() { Name = "IT SIstemi", Group = groups[0], Players = new List<Player>()
                    {
                        new Player() { FirstName = "AasAA", LastName = "JasdadJJJ", DateOfBirth = new DateTime(1994, 11, 21)},
                        new Player() { FirstName = "BB", LastName = "Iasd12II", DateOfBirth = new DateTime(1994, 01, 30)},
                        new Player() { FirstName = "NdsaNNN", LastName = "HH12HH", DateOfBirth = new DateTime(1996, 02, 22)},
                        new Player() { FirstName = "MsdaasdasNNN", LastName = "GG12GG", DateOfBirth = new DateTime(1983, 09, 01)},
                        new Player() { FirstName = "JasJJJ", LastName = "1212F21FFF", DateOfBirth = new DateTime(1993, 08, 11)},
                        new Player() { FirstName = "HHdsaHH", LastName = "E12EE", DateOfBirth = new DateTime(1988, 09, 23)},
                        new Player() { FirstName = "GGasdGG", LastName = "2112DDDD", DateOfBirth = new DateTime(1991, 07, 06)},
                        new Player() { FirstName = "FadsFF", LastName = "CC12CC", DateOfBirth = new DateTime(1995, 02, 27)},
                        new Player() { FirstName = "EasdasEEE", LastName = "B12BB", DateOfBirth = new DateTime(1995, 01, 29)},
                        new Player() { FirstName = "DDD", LastName = "AA12AA", DateOfBirth = new DateTime(1993, 02, 20)},
                    }}, //10
                    new Team() { Name = "Profico", Group = groups[1], Players = new List<Player>()
                    {
                        new Player() { FirstName = "12", LastName = "12sa", DateOfBirth = new DateTime(1994, 11, 21)},
                        new Player() { FirstName = "222", LastName = "I232II", DateOfBirth = new DateTime(1994, 01, 30)},
                        new Player() { FirstName = "412", LastName = "HH3124HH", DateOfBirth = new DateTime(1996, 02, 22)},
                        new Player() { FirstName = "412", LastName = "GG512GG", DateOfBirth = new DateTime(1983, 09, 01)},
                        new Player() { FirstName = "12", LastName = "F512FFF", DateOfBirth = new DateTime(1993, 08, 11)},
                        new Player() { FirstName = "12", LastName = "E4214EE", DateOfBirth = new DateTime(1988, 09, 23)},
                        new Player() { FirstName = "12", LastName = "DD412DD", DateOfBirth = new DateTime(1991, 07, 06)},
                        new Player() { FirstName = "21", LastName = "CC412CC", DateOfBirth = new DateTime(1995, 02, 27)},
                        new Player() { FirstName = "1212", LastName = "B241BBB", DateOfBirth = new DateTime(1995, 01, 29)},
                        new Player() { FirstName = "DDD", LastName = "AA412AA", DateOfBirth = new DateTime(1993, 02, 20)},
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

            app.UseAuthentication();

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
