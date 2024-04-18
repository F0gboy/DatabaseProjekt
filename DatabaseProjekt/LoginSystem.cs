using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProjekt
{
    internal class LoginSystem
    {
        private NpgsqlDataSource dataSource;

        public LoginSystem(NpgsqlDataSource datasource)
        {
            this.dataSource = datasource;

            Console.WriteLine("1. Register new user");
            Console.WriteLine("2. Login");

            string input = Console.ReadLine();

            if (input == "1")
            {
                Register(dataSource);
            }
            else if (input == "2")
            {
                Login(dataSource);
            }
        }

        void Register(NpgsqlDataSource dataSource)
        {
            Console.WriteLine("Username?");
            string inputUsername = Console.ReadLine();

            Console.WriteLine("Password?");
            string inputPassword = Console.ReadLine();

            Console.WriteLine("Checking Database");

            NpgsqlCommand cmd = dataSource.CreateCommand(
                "SELECT * " +
                "FROM users " +
                "WHERE username = $1");

            cmd.Parameters.AddWithValue(inputUsername);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    Console.WriteLine("Username already exists!");
                }
                else
                {
                    Console.WriteLine("Registering new user");

                    NpgsqlCommand cmd2 = dataSource.CreateCommand(
                        "INSERT INTO users (username, password) " +
                        "VALUES ($1, $2)");

                    cmd2.Parameters.AddWithValue(inputUsername);
                    cmd2.Parameters.AddWithValue(inputPassword);

                    cmd2.ExecuteNonQuery();

                    Console.WriteLine("Registration successful!");
                }
            }
        }

        void Login(NpgsqlDataSource dataSource)
        {
            Console.WriteLine("Username?");
            string inputUsername = Console.ReadLine();

            Console.WriteLine("Password?");
            string inputPassword = Console.ReadLine();

            Console.WriteLine("Checking Database");

            NpgsqlCommand cmd = dataSource.CreateCommand(
                "SELECT * FROM users WHERE username = $1 AND password = $2");

            cmd.Parameters.AddWithValue(inputUsername);
            cmd.Parameters.AddWithValue(inputPassword);

            NpgsqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                int userId = reader.GetInt32(0);

                Console.WriteLine("Login successful!");

                NpgsqlCommand cmd1 = dataSource.CreateCommand(
                    "INSERT INTO login_attempts (success, attempt_time, user_id) " +
                    "VALUES ($1, $2)");

                cmd1.Parameters.AddWithValue(true);
                cmd1.Parameters.AddWithValue(DateTime.Now);
                cmd1.Parameters.AddWithValue(userId);
                cmd1.ExecuteNonQuery();
            }
            else
            {
                int userId = reader.GetInt32(0);

                Console.WriteLine("Login failed!");

                NpgsqlCommand cmd1 = dataSource.CreateCommand(
                    "INSERT INTO login_attempts (success, attempt_time, user_id) " +
                    "VALUES ($1, $2)");

                cmd1.Parameters.AddWithValue(false);
                cmd1.Parameters.AddWithValue(DateTime.Now);
                cmd1.Parameters.AddWithValue(userId);
                cmd1.ExecuteNonQuery();
            }

        }
    }
}
