using Npgsql;
using NpgsqlTypes;

namespace DatabaseProjekt
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            string connectionString = "Host=localhost;Username=postgres;Password=hats1234;Database=data";
            NpgsqlDataSource dataSource = NpgsqlDataSource.Create(connectionString);
            List<Character> userChars = new List<Character>();


            userChars.Add(GenerateChar(userChars, dataSource));
            userChars.Add(GenerateChar(userChars, dataSource));
            userChars.Add(GenerateChar(userChars, dataSource));
            userChars.Add(GenerateChar(userChars, dataSource));
            userChars.Add(GenerateChar(userChars, dataSource));
            userChars.Add(GenerateChar(userChars, dataSource));
            userChars.Add(GenerateChar(userChars, dataSource));



            foreach (Character cha in userChars)
            {
                Console.WriteLine("Name: " + cha.name + "\nLevel: " + cha.lvl + "\nStage: " + cha.stage + "\nKills: " + cha.kills + "\nDeath: " + DeathText(cha.death)+ "\n\n");


            }
        }





        static Character GenerateChar(List<Character> chars, NpgsqlDataSource dataSource)
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
            Character character = new Character( name, lvl, order, stage, kills, death ) ;
            

            //SQL when Marc Database is finished
            NpgsqlCommand cmd = dataSource.CreateCommand(@"INSERT INTO characters (username, email, password) VALUES ($1, $2, $3)");
            Console.WriteLine("Register an account\nEnter Username");
            cmd.Parameters.AddWithValue(Console.ReadLine());
            Console.WriteLine("Enter Email");
            cmd.Parameters.AddWithValue(Console.ReadLine());
            Console.WriteLine("Enter Password");
            cmd.Parameters.AddWithValue(Console.ReadLine());
            cmd.ExecuteNonQuery();

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


        static string NamePick()
        {
            Random rnd = new Random();
            string[] names1 = new string[] { "Alexander", "Benjamin", "Casper", "Daniel", "Emil", "Frederik", "Gustav", "Henrik", "Isak", "Johan", "Kasper", "Lukas", "Mathias", "Nikolaj", "Oliver", "Philip", "Quentin", "Rasmus", "Sebastian", "Theodor", "Ulrik", "Victor", "William", "Xander", "Yannick", "Zacharias", "Albert", "Bjørn", "Christian", "David", "Erik", "Filip", "Gabriel", "Hugo", "Ibrahim", "Jacob", "Kristian", "Lars", "Mikkel", "Noah", "Oscar", "Patrick", "Quincy", "Robin", "Simon", "Tobias", "Uffe", "Viggo", "Walter", "Xavier", "Yusuf", "Zander", "Anders", "Bo", "Carl", "Dennis", "Emmanuel", "Felix", "Gunnar", "Hans", "Ivan", "Jonas", "Karl", "Liam", "Mads"};
            string[] names2 = new string[] { "Andersen", "Bach", "Christensen", "Dahl", "Eriksen", "Frederiksen", "Gundersen", "Hansen", "Iversen", "Jensen", "Kristensen", "Larsen", "Madsen", "Nielsen", "Olsen", "Petersen", "Qvist", "Rasmussen", "Sørensen", "Thomsen", "Unger", "Vestergaard", "Wagner", "Xu", "Yilmaz", "Zimmermann", "Andreasen", "Berg", "Christiansen", "Davidsen", "Enevoldsen", "Friis", "Gustafsson", "Hoffmann", "Ibrahimovic", "Jørgensen", "Kjeldsen", "Lassen", "Mortensen", "Nissen", "Pedersen", "Schmidt", "Lund", "Jacobsen", "Møller", "Olesen", "Jakobsen", "Poulsen", "Villadsen", "Holm", "Schultz", "Mortensen", "Andersson", "Svensson", "Karlsson", "Eriksson", "Hermansen", "Thomsen", "Carlsen", "Lorentzen", "Søgaard", "Johansen", "Bachmann", "Petersson", "Damgaard", "Nørgaard", "Mikkelsen", "Bergmann", "Rasmussen", "Hansen", "Christiansen", "Andreasen", "Jørgensen", "Olsen", "Larsen", "Madsen", "Poulsen", "Eriksen", "Hoffmann" };
            return (names1[rnd.Next(names1.Length)]+" "+ names2[rnd.Next(names2.Length)]);  

        }

    }
}