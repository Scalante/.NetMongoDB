using MongoDB.Driver;
using MongoDB.Net6.Core;

namespace MongoDB.Net6
{
    public class Startup
    {
        #region Private Attributes
        private readonly string _corsRule = "CorsRule";
        #endregion

        #region Public Attributes
        public IConfiguration Configuration { get; }
        #endregion

        #region Constructor
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region Public Methods
        public void ConfigureServices(IServiceCollection services)
        {
            // Cadena conexión Mongo DB
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];

            // Instancia de MongoClient
            var client = new MongoClient(connectionString);

            // Registra el MongoClient en el contenedor de dependencias para su posterior uso
            services.AddSingleton<IMongoClient>(client);

            services.AddTransient(typeof(ICrudMongoDB<>), typeof(CrudMongoDB<>));

            services.AddControllers()
             .AddNewtonsoftJson(options =>
             {
                 options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                 options.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore;
                 options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                 options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                 options.SerializerSettings.DateFormatString = "dd/MM/yyyy HH:mm:ss";
                 options.SerializerSettings.FloatParseHandling = Newtonsoft.Json.FloatParseHandling.Double;
             });

            services.AddMvc().AddViewLocalization();
            services.AddLocalization(options => options.ResourcesPath = "");

            services.AddCors(options =>
            {
                options.AddPolicy(name: _corsRule, policy =>
                {
                    policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                });
            });

            // Add custom services - Swagger
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env,
                              IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseExceptionHandler("/error");
            app.UseStatusCodePagesWithRedirects("~/status-code/{0}");
            app.UseStatusCodePages();


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors(_corsRule);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });

            app.UseSwagger();
            app.UseSwaggerUI();
        }
        #endregion

    }
}
