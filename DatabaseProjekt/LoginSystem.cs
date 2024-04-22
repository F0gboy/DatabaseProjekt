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
        private CharacterRepository characterRepository;

        public LoginSystem(NpgsqlDataSource datasource)
        {
            this.dataSource = datasource;
            this.characterRepository = new CharacterRepository(dataSource);

            Start();
        }

        public void Start()
        {
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
            Console.Clear();
            Console.WriteLine("Username?");
            string inputUsername = Console.ReadLine();

            Console.WriteLine("Password?");
            string inputPassword = Console.ReadLine();

            Console.WriteLine("Checking Database");

            NpgsqlCommand cmd = dataSource.CreateCommand(
                "SELECT * " +
                "FROM login_system " +
                "WHERE username = $1");

            cmd.Parameters.AddWithValue(inputUsername);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    Console.Clear();
                    Console.WriteLine("Username already exists!");
                    Start();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Registering new user");

                    NpgsqlCommand cmd2 = dataSource.CreateCommand(
                        "INSERT INTO login_system (username, password) " +
                        "VALUES ($1, $2)");

                    cmd2.Parameters.AddWithValue(inputUsername);
                    cmd2.Parameters.AddWithValue(inputPassword);

                    cmd2.ExecuteNonQuery();

                    Console.WriteLine("Registration successful!");

                    Start();
                }
            }
        }

        void Login(NpgsqlDataSource dataSource)
        {
            Console.Clear();
            Console.WriteLine("Username?");
            string inputUsername = Console.ReadLine();

            Console.WriteLine("Password?");
            string inputPassword = Console.ReadLine();

            Console.WriteLine("Checking Database");

            NpgsqlCommand cmd = dataSource.CreateCommand(
                "SELECT * FROM login_system WHERE username = $1 AND password = $2");

            cmd.Parameters.AddWithValue(inputUsername);
            cmd.Parameters.AddWithValue(inputPassword);

            NpgsqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                int userId = reader.GetInt32(0);

                Console.Clear();
                Console.WriteLine("Login successful!");
                Menu(userId);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Login failed!");
                Start();
            }
        }

        void Menu(int userId)
        {
            Console.WriteLine("1. Create character");
            Console.WriteLine("2. List characters");

            string input = Console.ReadLine();

            if (input == "1")
            {
                Character newCharacter = GenerateChar(userId);
                characterRepository.InsertCharacter(newCharacter,
                    userId); 
                Console.Clear();
                Console.WriteLine("Created Character!");
                Menu(userId);
            }
            else if (input == "2")
            {
                List<Character> userCharacters = characterRepository.GetCharactersByLoginId(userId);

                foreach (Character cha in userCharacters)
                {
                    Console.WriteLine("Name: " + cha.name + "\nLevel: " + cha.lvl + "\nStage: " + cha.stage +
                                      "\nClass: " + Class(cha.classe) + "\nKills: " + cha.kills + "\nDeath: " +
                                      DeathText(cha.death) + "\n\n");
                }

                Menu(userId);
            }
        }

        List<Character> FetchUserCharacters(NpgsqlDataSource dataSource, int userId)
        {
            List<Character> userCharacters = new List<Character>();

            NpgsqlCommand cmd = dataSource.CreateCommand(
                "SELECT Character_names, Levels, Death_Order, Stege, Kills, Death, Class FROM Characters WHERE Login_id = $1");
            cmd.Parameters.AddWithValue(userId);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    int lvl = reader.GetInt32(1);
                    int order = reader.GetInt32(2);
                    int stage = reader.GetInt32(3);
                    int kills = reader.GetInt32(4);
                    int death = reader.GetInt32(5);
                    int classe = reader.GetInt32(6);

                    Character character = new Character(name, lvl, order, stage, kills, death, classe);
                    userCharacters.Add(character);
                }
            }

            return userCharacters;
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

        Character GenerateChar(int userId)
        {
            Random rnd = new Random();
            string name = NamePick();
            int lvl = rnd.Next(100);
            int order = characterRepository.GetCharactersByLoginId(userId).Count;
            int stage = rnd.Next(1000);
            int kills = 0;
            for (int i = 0; i < stage; i++)
            {
                kills += rnd.Next(10);
            }

            int death = rnd.Next(6);
            int classe = rnd.Next(6);
            Character character = new Character(name, lvl, order, stage, kills, death, classe);

            return character;
        }

        static string NamePick()
        {
            Random rnd = new Random();
            string[] names1 = new string[]
            {
                "Alexander", "Benjamin", "Casper", "Daniel", "Emil", "Frederik", "Gustav", "Henrik", "Isak", "Johan",
                "Kasper", "Lukas", "Mathias", "Nikolaj", "Oliver", "Philip", "Quentin", "Rasmus", "Sebastian",
                "Theodor", "Ulrik", "Victor", "William", "Xander", "Yannick", "Zacharias", "Albert", "Bjørn",
                "Christian", "David", "Erik", "Filip", "Gabriel", "Hugo", "Ibrahim", "Jacob", "Kristian", "Lars",
                "Mikkel", "Noah", "Oscar", "Patrick", "Quincy", "Robin", "Simon", "Tobias", "Uffe", "Viggo", "Walter",
                "Xavier", "Yusuf", "Zander", "Anders", "Bo", "Carl", "Dennis", "Emmanuel", "Felix", "Gunnar", "Hans",
                "Ivan", "Jonas", "Karl", "Liam", "Mads"
            };
            string[] names2 = new string[]
            {
                "Andersen", "Bach", "Christensen", "Dahl", "Eriksen", "Frederiksen", "Gundersen", "Hansen", "Iversen",
                "Jensen", "Kristensen", "Larsen", "Madsen", "Nielsen", "Olsen", "Petersen", "Qvist", "Rasmussen",
                "Sørensen", "Thomsen", "Unger", "Vestergaard", "Wagner", "Xu", "Yilmaz", "Zimmermann", "Andreasen",
                "Berg", "Christiansen", "Davidsen", "Enevoldsen", "Friis", "Gustafsson", "Hoffmann", "Ibrahimovic",
                "Jørgensen", "Kjeldsen", "Lassen", "Mortensen", "Nissen", "Pedersen", "Schmidt", "Lund", "Jacobsen",
                "Møller", "Olesen", "Jakobsen", "Poulsen", "Villadsen", "Holm", "Schultz", "Mortensen", "Andersson",
                "Svensson", "Karlsson", "Eriksson", "Hermansen", "Thomsen", "Carlsen", "Lorentzen", "Søgaard",
                "Johansen", "Bachmann", "Petersson", "Damgaard", "Nørgaard", "Mikkelsen", "Bergmann", "Rasmussen",
                "Hansen", "Christiansen", "Andreasen", "Jørgensen", "Olsen", "Larsen", "Madsen", "Poulsen", "Eriksen",
                "Hoffmann"
            };
            return (names1[rnd.Next(names1.Length)] + " " + names2[rnd.Next(names2.Length)]);

        }

    }
}
