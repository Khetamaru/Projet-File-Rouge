using Local_API_Server.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Projet_File_Rouge.Object;
using System;
using System.IO;

namespace Red_Wire_API
{
    public class Startup
    {
        public DataBaseString redWireConnectionString;
        public DataBaseString ebpConnectionString;

        public string databaseVersion = "V1";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// API start here. Create BDD Connections and launch table links.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("Mise en place des liens (API <=> BDD).");
            Setup setup = SetupCacheFile.Start();

            redWireConnectionString = new DataBaseString("localhost", "root", "root", true, "redwiredb");
            ebpConnectionString = new DataBaseString(setup.IP_EBP, setup.USER_NAME, setup.PASSWORD, true, setup.BDD_NAME);

            services.AddDbContext<RedWireContext>(opt =>
                opt.UseMySql(redWireConnectionString.GetConnectionString(),
                ServerVersion.AutoDetect(redWireConnectionString.GetConnectionString())));
            Console.WriteLine("Lien vers la table \"RedWire\" créé.");

            services.AddDbContext<DocumentListContext>(opt =>
                opt.UseMySql(redWireConnectionString.GetConnectionString(),
                ServerVersion.AutoDetect(redWireConnectionString.GetConnectionString())));
            Console.WriteLine("Lien vers la table \"DocumentList\" créé.");

            services.AddDbContext<EvenementContext>(opt =>
                opt.UseMySql(redWireConnectionString.GetConnectionString(),
                ServerVersion.AutoDetect(redWireConnectionString.GetConnectionString())));
            Console.WriteLine("Lien vers la table \"Evenement\" créé.");

            services.AddDbContext<UserContext>(opt =>
                opt.UseMySql(redWireConnectionString.GetConnectionString(),
                ServerVersion.AutoDetect(redWireConnectionString.GetConnectionString())));
            Console.WriteLine("Lien vers la table \"User\" créé.");

            services.AddDbContext<UserHistoryListContext>(opt =>
                opt.UseMySql(redWireConnectionString.GetConnectionString(),
                ServerVersion.AutoDetect(redWireConnectionString.GetConnectionString())));
            Console.WriteLine("Lien vers la table \"UserHistoryList\" créé.");

            services.AddDbContext<CommandListContext>(opt =>
                opt.UseMySql(redWireConnectionString.GetConnectionString(),
                ServerVersion.AutoDetect(redWireConnectionString.GetConnectionString())));
            Console.WriteLine("Lien vers la table \"CommandList\" créé.");

            services.AddDbContext<LogContext>(opt =>
                opt.UseMySql(redWireConnectionString.GetConnectionString(),
                ServerVersion.AutoDetect(redWireConnectionString.GetConnectionString())));
            Console.WriteLine("Lien vers la table \"Log\" créé.");

            services.AddDbContext<VersionContext>(opt =>
                opt.UseMySql(redWireConnectionString.GetConnectionString(),
                ServerVersion.AutoDetect(redWireConnectionString.GetConnectionString())));
            Console.WriteLine("Lien vers la table \"Version\" créé.");

            services.AddDbContext<MissingCallContext>(opt =>
                opt.UseMySql(redWireConnectionString.GetConnectionString(),
                ServerVersion.AutoDetect(redWireConnectionString.GetConnectionString())));
            Console.WriteLine("Lien vers la table \"MissingCall\" créé.");

            services.AddDbContext<DocumentFTPContext>(opt =>
                opt.UseMySql(redWireConnectionString.GetConnectionString(),
                ServerVersion.AutoDetect(redWireConnectionString.GetConnectionString())));
            Console.WriteLine("Lien vers la table \"DocumentFTP\" créé.");

            services.AddDbContext<DbSaveContext>(opt =>
                opt.UseMySql(redWireConnectionString.GetConnectionString(),
                ServerVersion.AutoDetect(redWireConnectionString.GetConnectionString())));
            Console.WriteLine("Lien vers la table \"DbSave\" créé.");

            services.AddDbContext<SaleDocumentContext>(opt =>
                opt.UseSqlServer(ebpConnectionString.GetConnectionString() + ";Encrypt=true;TrustServerCertificate=true;", 
                providerOptions => providerOptions.EnableRetryOnFailure()));
            Console.WriteLine("Lien vers la table EBP \"SaleDocument\" créé.");

            services.AddDbContext<SaleDocumentLineContext>(opt =>
                opt.UseSqlServer(ebpConnectionString.GetConnectionString() + ";Encrypt=true;TrustServerCertificate=true;", 
                providerOptions => providerOptions.EnableRetryOnFailure()));
            Console.WriteLine("Lien vers la table EBP \"SaleDocumentLine\" créé.");

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = redWireConnectionString.databaseName, Version = databaseVersion });
            });
            Console.WriteLine("Réussite de la mise en place globale.");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpLogging();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Red_Wire_API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// BDD Connection String Object Manager
        /// </summary>
        public class DataBaseString
        {
            public string serverName;
            public string userId;
            public string password;
            public bool persistsecurityinfo;
            public string databaseName;

            public DataBaseString(string _serverName, string _userId, string _password, bool _persistsecurityinfo, string _databaseName)
            {
                serverName = _serverName;
                userId = _userId;
                password = _password;
                persistsecurityinfo = _persistsecurityinfo;
                databaseName = _databaseName;
            }

            public string GetConnectionString()
            {
                return "server=" + serverName + ";" +
                       "Uid=" + userId + ";" +
                       "password=" + password + ";" +
                       "persistsecurityinfo=" + persistsecurityinfo + ";" +
                       "database=" + databaseName;
            }
        }
    }
}