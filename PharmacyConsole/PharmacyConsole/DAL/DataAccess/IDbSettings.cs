namespace PharmacyConsole.DAL.DataAccess
{
    public interface IDbSettings
    {
        string ProviderName { get; }
        string ConnectionString { get; }
        int? CommandTimeout { get; }
    }
}
