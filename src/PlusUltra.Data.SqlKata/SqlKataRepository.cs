using System.Data;
using Dapper;
using SqlKata;
using SqlKata.Execution;

namespace PlusUltra.Data.SqlKata
{
    public class SqlKataBaseRepository : ISqlKataRepository
    {
        protected readonly QueryFactory db;

        public SqlKataBaseRepository(QueryFactory db)
        {
            this.db = db;
        }

        protected (IDbConnection Connection, string Sql, DynamicParameters Parameters) GetDataForMapJoins(Query query)
        {
            var xQuery = query as XQuery;
            var compiler = xQuery.Compiler;
            var connection = xQuery.Connection;
            var compiled = compiler.Compile(query);
            var parameters = new DynamicParameters();

            foreach (var param in compiled.NamedBindings)
                parameters.Add(param.Key, param.Value);

            return (connection, compiled.Sql, parameters);
        }

        public void BeginTransaction()
        {
            db.Statement("BEGIN TRANSACTION");
        }

        public void Commit()
        {
            db.Statement("COMMIT");
        }

        public void Rollback()
        {
            db.Statement("ROLLBACK");
        }
    }
}