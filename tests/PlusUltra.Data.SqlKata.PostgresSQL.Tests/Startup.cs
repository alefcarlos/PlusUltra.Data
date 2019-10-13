using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlusUltra.Testing;

namespace PlusUltra.Data.SqlKata.PostgresSQL.Tests
{
    public class Startup : TestStartup
    {
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            services.AddPostgressSQL(configuration, loggerFactory);
        }
    }
}