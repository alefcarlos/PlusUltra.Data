using System;
using PlusUltra.Testing;
using SqlKata.Execution;
using Xunit;

namespace PlusUltra.Data.SqlKata.PostgresSQL.Tests
{
    public class ConnectionTest : TestHost<Startup>
    {
        public ConnectionTest(){
            queryFactory = GetService<QueryFactory>();
        }

        private readonly QueryFactory queryFactory;

        [Fact]
        public void Connection_Should_Be_Succesfuly()
        {
            queryFactory.Connection.Open();
        }
    }
}
