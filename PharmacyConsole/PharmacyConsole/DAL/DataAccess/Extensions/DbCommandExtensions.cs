using System.Data;
using System.Data.Common;

namespace PharmacyConsole.DAL.DataAccess.Extensions
{
    public static class DbCommandExtensions
    {
        public static DbCommand AddParam(this DbCommand command, string name, int value)
        {
            return command.AddParam(DbType.Int32, name, value);
        }

        private static DbCommand AddParam<T>(this DbCommand command, DbType type, string name, T value) where T : struct
        {
            AddParamInternal(command, type, name, ParameterDirection.Input).Value = value;
            return command;
        }

        public static DbCommand AddParam(this DbCommand command, string name, string value)
        {
            DbParameter dbParameter = AddParamInternal(command, DbType.String, name, ParameterDirection.Input);
            if (value == null)
            {
                dbParameter.Value = DBNull.Value;
            }
            else
            {
                dbParameter.Value = value;
            }

            return command;
        }

        private static DbParameter AddParamInternal(DbCommand command, DbType type, string name, ParameterDirection direction, int? size = null)
        {
            DbParameter dbParameter = command.CreateParameter();
            dbParameter.ParameterName = name;
            dbParameter.DbType = type;
            dbParameter.Direction = direction;
            if (size.HasValue)
            {
                dbParameter.Size = size.Value;
            }

            command.Parameters.Add(dbParameter);
            return dbParameter;
        }
    }
}
