
using AutoMapper;
using BasicWebAPI.Dal;
using BasicWebAPI.Dal.Interfaces;
using BasicWebAPI.Dal.Repository;
using BasicWebAPI.Service.Automapper;
using BasicWebAPI.Service.Interfaces;
using BasicWebAPI.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace BasicWebAPI.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var cs = builder.Configuration.GetConnectionString("Default");
        builder.Services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(cs, sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            });
        });


        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new CountryMappingProfile());
            mc.AddProfile(new CompanyMappingProfile());
            mc.AddProfile(new ContactMappingProfile());
        });

        IMapper mapper = mappingConfig.CreateMapper();
        builder.Services.AddSingleton(mapper);
        builder.Services.AddAutoMapper(typeof(Program).Assembly);

        builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
        builder.Services.AddScoped<ICompanyService, CompanyService>();

        builder.Services.AddScoped<ICountryRepository, CountryRepository>();
        builder.Services.AddScoped<ICountryService, CountryService>();

        builder.Services.AddScoped<IContactRepository, ContactRepository>();
        builder.Services.AddScoped<IContactService, ContactService>();

        builder.Services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddConsole();
            loggingBuilder.AddDebug();
        });

        var app = builder.Build();


        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<DataContext>();
                context.Database.EnsureCreated();
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating or initializing the database.");
            }
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
