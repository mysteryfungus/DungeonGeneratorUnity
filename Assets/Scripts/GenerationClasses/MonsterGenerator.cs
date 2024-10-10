using DbClasses;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GenerationClasses
{
    class MonsterGenerator
    {
        private string dbName;
        private List<Monster> monsters;
        private int monstersCount = 0;
        public MonsterGenerator(string _dbName)
        {
            this.dbName = _dbName;
            //GetMonstersTByLevel(1);
        }
        Monster monster;
        readonly Dictionary<int, int> expcostlist = new Dictionary<int, int>()
            {
                {10, -4 },
                {15, -3 },
                {20, -2 },
                {30, -1},
                {40, 0 },
                {60, 1 },
                {80, 2 },
                {120, 3 },
                {160, 4 }
            };
        Dictionary<int, List<Monster>> monsters_by_lvl = new Dictionary<int, List<Monster>>();
        public List<Monster> BuildCombat(int xpbudget, int party_level)
        {
            monsters = new List<Monster>();
            //System.Console.Write($"----Всего опыта на монстров: - {xpbudget}\n");
            Random rnd = new Random();
            while (xpbudget >= 10)
            {
                int moncostindex;
                switch (party_level)
                {
                    case 1: //Для группы уровня "1"
                        moncostindex = rnd.Next(2, 8);
                        break;
                    case 2://Для группы уровня "2"
                        moncostindex = rnd.Next(1, 8);
                        break;
                    default://Для остальных разницы нет
                        moncostindex = rnd.Next(0, 8);
                        break;
                        //TODO: обработать случаи для уровней, близких к 20.
                }


                int moncost = expcostlist.ElementAt(moncostindex).Key; // стоимость моба по бюджету
                if (moncost > xpbudget) continue;
                //System.Console.Write($"Индекс стоимости по таблице - {moncost}\n");

                int monlvl = party_level + expcostlist.ElementAt(moncostindex).Value; // лвл исходя из уровня пати
                //Console.WriteLine($"Пытаемся достать челика с {monlvl}\n");
                if (!monsters_by_lvl.ContainsKey(monlvl)) //Проверка, сохраняли ли список монстров этого уровня
                {
                    monsters_by_lvl[monlvl] = GetMonstersTByLevel(monlvl); //Сохраняем, если нет
                }
                int max_mon_amount = monsters_by_lvl[monlvl].Count; //Сколько всего монстров этого уровня?
                monster = monsters_by_lvl[monlvl][rnd.Next(0, max_mon_amount)]; //Случайный монстр этого уровня
                monsters.Add(monster);
                xpbudget -= moncost; //Потратили опыт на этого монстра.
                //Console.WriteLine($"Осталось опыта: {xpbudget}\n");
                //MonsterOutput.Text += $"{monster} / {monster.Level} \n";
                if (party_level <= 2 && xpbudget <= 10) break;
            }
            return (monsters);
        }
        private List<Monster> GetMonstersTByLevel(int level)
        {
            using (SqliteConnection connection = new SqliteConnection(dbName)) 
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand()) 
                {
                    command.CommandText = "SELECT * FROM Monsters WHERE Level = " + level.ToString();

                    using (SqliteDataReader reader = command.ExecuteReader()) 
                    {
                        monsters = new List<Monster>();
                        //сохраняем результат как List;
                        while (reader.Read())
                        {
                            monsters.Add(Monster.ToMonster(reader.GetValue(0), reader.GetValue(1), reader.GetValue(2), reader.GetValue(3),reader.GetValue(4)));
                        }
                    }
                }
                connection.Close();
            }
            return monsters;
        }
    }
}