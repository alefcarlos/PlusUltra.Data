namespace PlusUltra.Data.SqlKata
{
    public interface ISqlKataRepository
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}