using Npgsql;
using NpgsqlTypes;
using System.Data;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace DatabaseProjekt
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            string connectionString = "Host=localhost;Username=postgres;Password=xxxxxxx;Database=data";
            NpgsqlDataSource dataSource = NpgsqlDataSource.Create(connectionString);

            string createTableLogin = "CREATE TABLE IF NOT EXISTS Login_system (Login_id SERIAL PRIMARY KEY, Username VARCHAR(50) NOT NULL UNIQUE, Password VARCHAR(50) NOT NULL)";
            string createTableCharacters = "CREATE TABLE IF NOT EXISTS Characters (Character_id SERIAL PRIMARY KEY, Login_id INTEGER NOT NULL, Levels INTEGER NOT NULL, Death_Order INTEGER NOT NULL, Class INTEGER NOT NULL, Character_names VARCHAR(50) NOT NULL, Stege INTEGER NOT NULL, Kills INTEGER NOT NULL, Death INTEGER NOT NULL, FOREIGN KEY (Login_id) REFERENCES Login_system(Login_id))";

            try
            {
                using (NpgsqlConnection conn = dataSource.OpenConnection())
                {
                    using (NpgsqlCommand cmd1 = new NpgsqlCommand(createTableLogin, conn))
                    {
                        cmd1.ExecuteNonQuery();
                        Console.WriteLine("Login Table created successfully.");
                    }
                    using (NpgsqlCommand cmd2 = new NpgsqlCommand(createTableCharacters, conn))
                    {
                        cmd2.ExecuteNonQuery();
                        Console.WriteLine("Character Table created successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            LoginSystem loginSystem = new LoginSystem(dataSource);
        }
    }
}



