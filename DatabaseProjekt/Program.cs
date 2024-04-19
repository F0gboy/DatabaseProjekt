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
            string connectionString = "Host=localhost;Username=postgres;Password=hats1234;Database=data";
            NpgsqlDataSource dataSource = NpgsqlDataSource.Create(connectionString);
            List<Character> userChars = new List<Character>();
            LoginSystem loginSystem = new LoginSystem(dataSource);


            userChars.Add(GenerateChar(userChars, dataSource, "PlaceHolder"));
            userChars.Add(GenerateChar(userChars, dataSource, "PlaceHolder"));
            userChars.Add(GenerateChar(userChars, dataSource, "PlaceHolder"));
            userChars.Add(GenerateChar(userChars, dataSource, "PlaceHolder"));
            userChars.Add(GenerateChar(userChars, dataSource, "PlaceHolder"));
            userChars.Add(GenerateChar(userChars, dataSource, "PlaceHolder"));
            userChars.Add(GenerateChar(userChars, dataSource, "PlaceHolder"));



            foreach (Character cha in userChars)
            {
                Console.WriteLine("Name: " + cha.name + "\nLevel: " + cha.lvl + "\nStage: " + cha.stage + "\nClass: " + Class(cha.classe) + "\nKills: " + cha.kills + "\nDeath: " + DeathText(cha.death)+ "\n\n");


            }
        }





        static Character GenerateChar(List<Character> chars, NpgsqlDataSource dataSource, string Login_Id)
        {
            Random rnd = new Random();
            string name = NamePick();
            int lvl = rnd.Next(100);
            int order = chars.Count();
            int stage = rnd.Next(1000);
            int kills = 0;
            for (int i = 0; i < stage; i++)
            {
                kills += rnd.Next(10);
            }
            int death = rnd.Next(6);
            int classe = rnd.Next(6);
            Character character = new Character( name, lvl, order, stage, kills, death, classe ) ;

           

            

            string createTableSql = "CREATE TABLE IF NOT EXISTS Characters (Charater_id integer NOT NULL GENERATED ALWAYS AS IDENTITY(INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1), Login_id integer NOT NULL, LvL integer NOT NULL, Death_Order integer NOT NULL, Class character varying(50) NOT NULL, Charater_names character varying(50) NOT NULL, Stege integer NOT NULL, Kills integer NOT NULL, Death character varying(50) NOT NULL, PRIMARY KEY(Charater_id), CONSTRAINT fk_login FOREIGN KEY (login_id) REFERENCES login(login_id))";

            try
            {
                // Establish connection
                using (NpgsqlConnection conn = dataSource.OpenConnection())
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



            //Login_id Lvl Death_Order Class Character_names Stege Kills Death
            NpgsqlCommand cmdd = dataSource.CreateCommand(@"INSERT INTO Characters (Login_id, Lvl, Death_Order, Class, Character_names, Stege, Kills, Death) VALUES ($1, $2, $3, $4, $5, $6, $7, $8)");
            cmdd.Parameters.AddWithValue(Login_Id);
            cmdd.Parameters.AddWithValue(lvl);
            cmdd.Parameters.AddWithValue(order);
            cmdd.Parameters.AddWithValue(classe);
            cmdd.Parameters.AddWithValue(name);
            cmdd.Parameters.AddWithValue(stage);
            cmdd.Parameters.AddWithValue(kills);
            cmdd.Parameters.AddWithValue(death);
            cmdd.ExecuteNonQuery();


            return character;
        }

        static string DeathText(int i)
        {
            switch (i)
            {
                case 1:
                    return "Died to fall damage";

                case 2:
                    return "Died to a skeleton";

                case 3:
                    return "Died to incompitance";

                case 4:
                    return "Died to starvation";

                case 5:
                    return "Died to collapsing ruble";

                case 6:
                    return "Died to monkey pox";

                default:
                    return "Died to unknown causes";
            }
        }

        static string Class(int i)
        {
            switch (i)
            {
                case 1:
                    return "Rogue";

                case 2:
                    return "Ranger";

                case 3:
                    return "Necromancer";

                case 4:
                    return "Paladin";

                case 5:
                    return "Knight";

                case 6:
                    return "Healer";

                default:
                    return "Peasant";
            }
        }

        static string NamePick()
        {
            Random rnd = new Random();
            string[] names1 = new string[] { "Alexander", "Benjamin", "Casper", "Daniel", "Emil", "Frederik", "Gustav", "Henrik", "Isak", "Johan", "Kasper", "Lukas", "Mathias", "Nikolaj", "Oliver", "Philip", "Quentin", "Rasmus", "Sebastian", "Theodor", "Ulrik", "Victor", "William", "Xander", "Yannick", "Zacharias", "Albert", "Bjørn", "Christian", "David", "Erik", "Filip", "Gabriel", "Hugo", "Ibrahim", "Jacob", "Kristian", "Lars", "Mikkel", "Noah", "Oscar", "Patrick", "Quincy", "Robin", "Simon", "Tobias", "Uffe", "Viggo", "Walter", "Xavier", "Yusuf", "Zander", "Anders", "Bo", "Carl", "Dennis", "Emmanuel", "Felix", "Gunnar", "Hans", "Ivan", "Jonas", "Karl", "Liam", "Mads"};
            string[] names2 = new string[] { "Andersen", "Bach", "Christensen", "Dahl", "Eriksen", "Frederiksen", "Gundersen", "Hansen", "Iversen", "Jensen", "Kristensen", "Larsen", "Madsen", "Nielsen", "Olsen", "Petersen", "Qvist", "Rasmussen", "Sørensen", "Thomsen", "Unger", "Vestergaard", "Wagner", "Xu", "Yilmaz", "Zimmermann", "Andreasen", "Berg", "Christiansen", "Davidsen", "Enevoldsen", "Friis", "Gustafsson", "Hoffmann", "Ibrahimovic", "Jørgensen", "Kjeldsen", "Lassen", "Mortensen", "Nissen", "Pedersen", "Schmidt", "Lund", "Jacobsen", "Møller", "Olesen", "Jakobsen", "Poulsen", "Villadsen", "Holm", "Schultz", "Mortensen", "Andersson", "Svensson", "Karlsson", "Eriksson", "Hermansen", "Thomsen", "Carlsen", "Lorentzen", "Søgaard", "Johansen", "Bachmann", "Petersson", "Damgaard", "Nørgaard", "Mikkelsen", "Bergmann", "Rasmussen", "Hansen", "Christiansen", "Andreasen", "Jørgensen", "Olsen", "Larsen", "Madsen", "Poulsen", "Eriksen", "Hoffmann" };
            return (names1[rnd.Next(names1.Length)]+" "+ names2[rnd.Next(names2.Length)]);  

        }

    }
}



