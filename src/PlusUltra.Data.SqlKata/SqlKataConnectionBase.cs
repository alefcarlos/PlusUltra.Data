using System;
using System.Data;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace PlusUltra.Data.SqlKata
{
    public class SqlKataConnectionBase : QueryFactory, IDisposable
    {
        public SqlKataConnectionBase(IDbConnection connection, Compiler compiler) : base(connection, compiler)
        {
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Connection.Dispose();
                }
                Connection = null;
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}