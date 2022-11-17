using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Brotherhood_Server.Data;
using Brotherhood_Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Brotherhood_Server
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Brotherhood_Server", Version = "v1" });
            });

			services.AddDbContext<BrotherhoodServerContext>(options =>
			{
				options.UseLazyLoadingProxies();
				options.UseSqlServer(Configuration.GetConnectionString("BrotherhoodServerContext"));
			});

			services.AddIdentity<Assassin, IdentityRole>().AddEntityFrameworkStores<BrotherhoodServerContext>();

			services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequireDigit = false;
				options.Password.RequiredLength = 5;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = false;
			});

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.SaveToken = true;
				options.RequireHttpsMetadata = false; // TODO: set true before deploying
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateAudience = true,
					ValidateIssuer = true,
					ValidAudience = "https://localhost:4200",
					ValidIssuer = "https://localhost:44386",
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Though yet of Hamlet our dear brother's death,The memory be green, and that it us befittedTo bear our hearts in grief, and our whole kingdomTo be contracted in one brow of woe;Yet so far has discretion fought with natureThat we with wisest sorrow think on himTogether with remembrance of ourselves.Therefore our sometimes sister, now our queen,Th' imperial jointress of this warlike state,Have we (as 'twere with a defeated joy,With one auspicious and one dropping eye,With mirth in funeral and with dirge in marriage,In equal scale weighing delight and dole) Taken to wife. Nor have we herein barred  Your better wisdoms which have freely goneWith this affair along. For all, our thanks.Now follows that you know — young Fortinbras, Holding a weak supposal of our worth,Or thinking by our late dear brother's deathOur state to be disjoint and out of frame,Colleagued with the dream of his advantage,He has not failed to pester us with messages,Importing the surrender of those landsLost by his father with all bonds of lawTo our most valiant brother. So much for him.[Enter messengers]Now for ourself and for this time of meeting. Thus much the business is: we have here writTo Norway (uncle of young FortinbrasWho, impotent and bed-rid, scarcely hearsOf this his nephew's purpose) to suppressHis further gait herein in that the levies,The lists, and full proportions are all madeOut of his subjects. And we here dispatch You, good Cornelius, and you, Voltemand,For bearing of this greeting to old Norway,Giving to you no further personal powerTo business with the king more than the scopeOf these delated articles allow.Farewell, and let your haste commend your duty."))
				};
			});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Brotherhood_Server v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

			app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
