using Local_API_Server.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            redWireConnectionString = new DataBaseString("localhost", "root", "root", true, "redwiredb");
            ebpConnectionString = new DataBaseString("192.168.1.195", "sa", "", true, "DO53_0895452f-b7c1-4c00-a316-c6a6d0ea4bf4");

            services.AddDbContext<RedWireContext>(opt =>
                opt.UseMySql(redWireConnectionString.GetConnectionString(), ServerVersion.AutoDetect(redWireConnectionString.GetConnectionString())));

            services.AddDbContext<DocumentListContext>(opt =>
                opt.UseMySql(redWireConnectionString.GetConnectionString(), ServerVersion.AutoDetect(redWireConnectionString.GetConnectionString())));

            services.AddDbContext<EvenementContext>(opt =>
                opt.UseMySql(redWireConnectionString.GetConnectionString(), ServerVersion.AutoDetect(redWireConnectionString.GetConnectionString())));

            services.AddDbContext<UserContext>(opt =>
                opt.UseMySql(redWireConnectionString.GetConnectionString(), ServerVersion.AutoDetect(redWireConnectionString.GetConnectionString())));

            services.AddDbContext<UserHistoryListContext>(opt =>
                opt.UseMySql(redWireConnectionString.GetConnectionString(), ServerVersion.AutoDetect(redWireConnectionString.GetConnectionString())));

            services.AddDbContext<CommandListContext>(opt =>
                opt.UseMySql(redWireConnectionString.GetConnectionString(), ServerVersion.AutoDetect(redWireConnectionString.GetConnectionString())));

            services.AddDbContext<SaleDocumentContext>(opt =>
                opt.UseSqlServer(ebpConnectionString.GetConnectionString() + ";Encrypt=true;TrustServerCertificate=true;", providerOptions => providerOptions.EnableRetryOnFailure()));

            services.AddDbContext<SaleDocumentLineContext>(opt =>
                opt.UseSqlServer(ebpConnectionString.GetConnectionString() + ";Encrypt=true;TrustServerCertificate=true;", providerOptions => providerOptions.EnableRetryOnFailure()));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = redWireConnectionString.databaseName, Version = databaseVersion });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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