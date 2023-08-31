using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace PharmacyConsole.DAL.DataAccess
{
    public abstract class BaseRepository
    {
        private readonly string connectionString;

        private readonly DbProviderFactory providerFactory;

        protected BaseRepository(IDbSettings settings)
        {
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
            connectionString = settings.ConnectionString;
            providerFactory = DbProviderFactories.GetFactory(settings.ProviderName);
        }

        public async Task<TransactionContext> StartTransactionAsync()
        {
            return await new TransactionContext().InitAsync(this);
        }

        private DbCommand CreateCommand(DbConnection connection, string commandText, TransactionContext context = null)
        {
            DbCommand dbCommand = connection.CreateCommand();
            if (context != null)
            {
                dbCommand.Transaction = context.Transaction;
            }

            dbCommand.CommandType = CommandType.Text;
            dbCommand.CommandText = commandText;

            return dbCommand;
        }

        protected async Task ExecuteReaderAsync(string commandText, Func<DbDataReader, Task> readerAction, Action<DbCommand> commandSetup = null)
        {
            await ExecuteReaderAsync(commandText, null, CancellationToken.None, readerAction, commandSetup);
        }

        protected async Task ExecuteReaderAsync(string commandText, TransactionContext context, CancellationToken cancellationToken, Func<DbDataReader, Task> readerAction, Action<DbCommand> commandSetup = null)
        {
            await WithConnectionAsync(context, async delegate (DbConnection connection)
            {
                using DbCommand command = CreateCommand(connection, commandText, context);
                commandSetup?.Invoke(command);
                using DbDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.Default, cancellationToken);
                await readerAction(reader);
            });
        }

        protected async Task ExecuteAsync(string commandText, Action<DbCommand> commandSetup = null)
        {
            await ExecuteAsync(commandText, null, CancellationToken.None, commandSetup);
        }

        protected async Task ExecuteAsync(string commandText, TransactionContext context, CancellationToken cancellationToken, Action<DbCommand> commandSetup = null)
        {
            await WithConnectionAsync(context, async delegate (DbConnection connection)
            {
                using DbCommand command = CreateCommand(connection, commandText, context);
                commandSetup?.Invoke(command);
                await command.ExecuteNonQueryAsync(cancellationToken);
            });
        }

        protected async Task<T> ExecuteAsync<T>(string procName, Func<DbDataReader, T> mapFunc, Action<DbCommand> commandSetup = null)
        {
            return await ExecuteAsync(procName, null, CancellationToken.None, mapFunc, commandSetup);
        }

        protected async Task<T> ExecuteAsync<T>(string procName, TransactionContext context, CancellationToken cancellationToken, Func<DbDataReader, T> mapFunc, Action<DbCommand> commandSetup = null)
        {
            T result = default(T);
            await ExecuteReaderAsync(procName, context, cancellationToken, async delegate (DbDataReader r)
            {
                result = ((await r.ReadAsync(cancellationToken)) ? mapFunc(r) : default(T));
            }, commandSetup);
            return result;
        }        

        protected async Task<List<T>> ExecuteListAsync<T>(string procName, Func<DbDataReader, T> mapFunc, Action<DbCommand> commandSetup = null)
        {
            return await ExecuteListAsync(procName, null, CancellationToken.None, mapFunc, commandSetup);
        }

        protected async Task<List<T>> ExecuteListAsync<T>(string procName, TransactionContext context, CancellationToken cancellationToken, Func<DbDataReader, T> mapFunc, Action<DbCommand> commandSetup = null)
        {
            List<T> result = new List<T>();
            await ExecuteReaderAsync(procName, context, cancellationToken, async delegate (DbDataReader r)
            {
                while (await r.ReadAsync(cancellationToken))
                {
                    result.Add(mapFunc(r));
                }
            }, commandSetup);
            return result;
        }

        private async Task WithConnectionAsync(TransactionContext context, Func<DbConnection, Task> action)
        {
            if (context?.Connection != null)
            {
                await action(context.Connection);
                return;
            }

            using DbConnection connection = CreateConnection();
            await connection.OpenAsync();
            await action(connection);
        }

        public DbConnection CreateConnection()
        {
            DbConnection dbConnection = providerFactory.CreateConnection();
            dbConnection.ConnectionString = connectionString;

            return dbConnection;
        }
    }
}
