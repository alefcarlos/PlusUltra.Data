using System.Data;
using SqlKata.Compilers;

namespace PlusUltra.Data.SqlKata.PostgresSQL
{
    public class SqlKataPostgresConnection : SqlKataConnectionBase
    {
        public SqlKataPostgresConnection(IDbConnection connection, Compiler compiler)
            : base(connection, compiler)
        {
        }
    }
}