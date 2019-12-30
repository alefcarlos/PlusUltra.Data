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