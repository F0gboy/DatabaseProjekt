using Npgsql;
using NpgsqlTypes;

namespace DatabaseProjekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Host=localhost;Username=postgres;Password=xxxxx;Database=data";

            NpgsqlDataSource dataSource = NpgsqlDataSource.Create(connectionString);

            LoginSystem loginSystem = new LoginSystem(dataSource);

        }
    }
}