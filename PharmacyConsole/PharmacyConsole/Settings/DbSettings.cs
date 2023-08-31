using PharmacyConsole.DAL.DataAccess;

namespace PharmacyConsole.Settings
{
    internal class DbSettings : IDbSettings
    {
        public string ProviderName { get; set; }

        public string ConnectionString { get; set; }

        public int? CommandTimeout { get; set; }
    }
}
