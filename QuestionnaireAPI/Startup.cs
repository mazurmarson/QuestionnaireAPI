using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuestionnaireAPI.Context;
using QuestionnaireAPI.Dtos;
using QuestionnaireAPI.Middleware;
using QuestionnaireAPI.Models;
using QuestionnaireAPI.Repos;
using QuestionnaireAPI.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireAPI
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

            var authenticationSettings = new AuthenticationSettings();
            Configuration.GetSection("Authentication").Bind(authenticationSettings);
            services.AddSingleton(authenticationSettings);
            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = "Bearer";
                opt.DefaultScheme = "Bearer";
                opt.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg => {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer, //Kto generuje
                    ValidAudience = authenticationSettings.JwtIssuer, //Kto jest odbiorcą
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
                };
            });
            services.AddControllers()
            .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
).AddFluentValidation(x => ValidatorOptions.Global.LanguageManager.Enabled = false);
            services.AddDbContext<QuestionnaireDbContext>();
            services.AddScoped<IGenRepo, GenRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IQuestionnaireRepo, QuestionnaireRepo>();
            services.AddScoped<IQuestionRepo, QuestionRepo>();
            services.AddScoped<IAnswerRepo, AnswerRepo>();
            services.AddScoped<IValidator<QuestionAnswerContentAddDto>,QuestionAnswerContentAddDtoValidator>();
            services.AddScoped<IValidator<QuestionAddDto> ,QuestionAddDtoValidator>();
            services.AddScoped<IValidator<QuestionnaireAddDto> ,QuestionnaireAddDtoValidator>();
            services.AddScoped<IValidator<QuestionAnswerOpen>, AddOpenAnswersValidator>();
            services.AddScoped<IValidator<RegisterUserDto>, RegisterDtoValidator>();
            services.AddScoped<IValidator<List<SubAnswerAddDto>>, SubAnswerAddValidatior>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QuestionnaireAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuestionnaireAPI v1"));
            }
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
