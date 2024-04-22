using Npgsql;
using System;
using System.Collections.Generic;

namespace DatabaseProjekt
{
    public class CharacterRepository
    {
        private NpgsqlDataSource dataSource;

        public CharacterRepository(NpgsqlDataSource dataSource)
        {
            this.dataSource = dataSource;
        }

        public void InsertCharacter(Character character, int userId)
        {
            using (NpgsqlConnection connection = dataSource.OpenConnection())
            {
                using (NpgsqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Characters (Login_id, Levels, Death_Order, Class, Character_names, Stege, Kills, Death) VALUES (@Login_id, @Levels, @Death_Order, @Class, @Character_names, @Stege, @Kills, @Death)";
                    cmd.Parameters.AddWithValue("@Login_id", userId);
                    cmd.Parameters.AddWithValue("@Levels", character.lvl);
                    cmd.Parameters.AddWithValue("@Death_Order", character.order);
                    cmd.Parameters.AddWithValue("@Class", character.classe);
                    cmd.Parameters.AddWithValue("@Character_names", character.name);
                    cmd.Parameters.AddWithValue("@Stege", character.stage);
                    cmd.Parameters.AddWithValue("@Kills", character.kills);
                    cmd.Parameters.AddWithValue("@Death", character.death);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Character> GetCharactersByLoginId(int userId)
        {
            List<Character> characters = new List<Character>();

            using (NpgsqlConnection connection = dataSource.OpenConnection())
            {
                using (NpgsqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT Character_names, Levels, Death_Order, Stege, Kills, Death, Class FROM Characters WHERE Login_id = @Login_id";
                    cmd.Parameters.AddWithValue("@Login_id", userId);

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
                            characters.Add(character);
                        }
                    }
                }
            }

            return characters;
        }
    }
}
