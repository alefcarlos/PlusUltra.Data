using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace PlusUltra.SqlKata.PostgreSQL
{
    public static class RegisterPostgressSQL
    {
        public static IServiceCollection AddPostgressSQL(this IServiceCollection services, IConfiguration configuration, ILoggerFactory logFactory)
        {
            var logger = logFactory.CreateLogger("PostgressSQL");

            services.AddScoped((provider) =>
            {
                var connection = new NpgsqlConnection(configuration.GetConnectionString("PostgreSQL"));

                var compiler = new PostgresCompiler();

                return new QueryFactory(connection, compiler){
                    Logger = compiled => logger.LogInformation(compiled.ToString())
                };
            });

            return services;
        }
    }
}