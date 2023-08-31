using System.Data.Common;

namespace PharmacyConsole.DAL.DataAccess.Extensions
{
    public static class DbDataReaderExtensions
    {
        public static string GetString(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            if (!reader.IsDBNull(ordinal))
            {
                return reader.GetString(ordinal);
            }

            return null;
        }

        public static int GetInt(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            return reader.GetInt32(ordinal);
        }
    }
}
