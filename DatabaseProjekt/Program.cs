﻿using Npgsql;
using NpgsqlTypes;
using System.Data;
using System;
using System.Security.Cryptography.X509Certificates;

namespace DatabaseProjekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Host=localhost;Username=postgres;Password=xk74yj6pd4w20;Database=data";

            NpgsqlDataSource dataSource = NpgsqlDataSource.Create(connectionString);

            

            string createTableSql = "CREATE TABLE IF NOT EXISTS Characters (Charater_id integer NOT NULL GENERATED ALWAYS AS IDENTITY(INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1), Login_id integer NOT NULL, LvL integer NOT NULL, Death_Order integer NOT NULL, Class character varying(50) NOT NULL, Charater_names character varying(50) NOT NULL, Stege integer NOT NULL, Kills integer NOT NULL, Death character varying(50) NOT NULL, PRIMARY KEY(Charater_id), CONSTRAINT fk_login FOREIGN KEY (login_id) REFERENCES login(login_id))";

            try
            {
                // Establish connection
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    // Create command and execute SQL
                    using (NpgsqlCommand cmd = new NpgsqlCommand(createTableSql, conn))
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Table created successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }
    }
}


