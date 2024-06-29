using CarRentalPro.Authorization;
using CarRentalPro.Data.Models;
using CarRentalPro.Dtos.UserDtos;
using CarRentalPro.Repository.IRepo;
using CarRentalPro.Repository.Repo;
using CarRentalPro.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<AppDBContext>(op =>
        op.UseSqlServer(builder.Configuration.GetConnectionString("myCon")));
        builder.Services.AddControllers().AddNewtonsoftJson();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddTransient<IUOW, UOW>();

        builder.Services.AddScoped<IAuthentication<UserDto>, Authentication>();

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ICarSliderRepository, CarSliderRepository>();
        builder.Services.AddScoped<IWhyRepositoy, WhyRepositoy>();
        builder.Services.AddScoped<IAboutRepository, AboutRepository>();
        builder.Services.AddScoped<ITestimonialsRepository, TestimonialsRepository>();
        builder.Services.AddScoped<IContactRepository, ContactRepository>();
        builder.Services.AddScoped<ICtaRepository, CtaRepository>();
        builder.Services.AddScoped<IHomeRepository, HomeRepository>();


        // Register email settings and email service
        builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
        builder.Services.AddTransient<IEmailService, EmailService>();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "",
                Version = "v1",
                Description = ".Net 8.0 API",
                Contact = new OpenApiContact()
                {
                    Email = "",
                    Name = "",
                    Url = new Uri("https://www.google.com/")
                }
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = ""
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
            });
        });
        builder.Services
            .AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });
        builder.Services.AddCors();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
                            Path.Combine(Directory.GetCurrentDirectory(), "file")),
            RequestPath = "/file"
        });

        // Shows UseCors with CorsPolicyBuilder.
        app.UseCors(builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });

        app.Run();
    }
}