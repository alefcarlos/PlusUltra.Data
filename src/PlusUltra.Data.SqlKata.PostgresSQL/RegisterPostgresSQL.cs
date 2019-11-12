using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace PlusUltra.Data.SqlKata.PostgresSQL
{
    public static class RegisterPostgressSQL
    {
        public static IServiceCollection AddPostgressSQL(this IServiceCollection services, IConfiguration configuration, bool addHealthCheck = true)
        {
            var logger = services.BuildServiceProvider().GetRequiredService<ILogger<PostgresSettings>>();

            services.Configure<PostgresSettings>(configuration.GetSection(nameof(PostgresSettings)));

            services.AddScoped((provider) =>
            {
                var settings = provider.GetRequiredService<IOptions<PostgresSettings>>().Value;

                var connection = new NpgsqlConnection(configuration.GetConnectionString("PostgreSQL"));

                void ProvideClientCertificates(X509CertificateCollection clientCerts)
                {
                    var clientCertPath = settings.ClientCertPath;
                    var cert = new X509Certificate2(clientCertPath);
                    clientCerts.Add(cert);
                }

                if (!string.IsNullOrEmpty(settings.ClientCertPath))
                    connection.ProvideClientCertificatesCallback += ProvideClientCertificates;

                var compiler = new PostgresCompiler();

                return new QueryFactory(connection, compiler)
                {
                    Logger = compiled => logger.LogInformation(compiled.ToString())
                };
            });

            //Adicionando healthcheck
            if (addHealthCheck)
            {
                services.AddHealthChecks()
                    .AddNpgSql(configuration.GetConnectionString("PostgreSQL"), tags: new string[] { "db", "sql", "postgres" });
            }

            return services;
        }
    }
}