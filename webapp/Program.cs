using domain.Interface;
using domain.Validate;
using FluentMigrator.Runner;
using Infraestrutura.Migracoes;
using Infraestrutura.Repositorios;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using System.Configuration;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace webapp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var serviceProvider = CreateServices())
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IAnabolizanteRepositorio, AnabolizanteRepositorio>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var AllowSpecificOrigins = "_allowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: AllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("https://localhost:7208",
                                                          "http://localhost:5186");
                                  });
            });

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")
                ),
                ContentTypeProvider = new FileExtensionContentTypeProvider
                {
                    Mappings = { [".properties"] = "application/x-msdownload" }
                }
            });

            app.UseDefaultFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }

        private static ServiceProvider CreateServices()
        {
            return new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSqlServer()
                .WithGlobalConnectionString(ConfigurationManager.ConnectionStrings["BombasDB"].ConnectionString)
                .ScanIn(typeof(MigracaoTabelaAnabolizantes).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }
    }
}
    