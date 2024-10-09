using DbClasses;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;

namespace GenerationClasses
{
    class HazardGenerator
    {
        private string dbName;
        private List<Hazard> hazards;
        private int hazardsCount = 0;
        
        public HazardGenerator(string _dbName)
        {
            this.dbName = _dbName;
            GetHazardsT();
        }

        private void GetHazardsT()
        {
            using (SqliteConnection connection = new SqliteConnection(dbName)) 
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand()) 
                {
                    command.CommandText = "SELECT * FROM Hazards";

                    using (SqliteDataReader reader = command.ExecuteReader()) 
                    {
                        hazards = new List<Hazard>();
                        //сохраняем результат как List;
                        while (reader.Read())
                        {
                            hazards.Add(Hazard.ToHazard(reader.GetValue(0), reader.GetValue(1), reader.GetValue(2), reader.GetValue(3),reader.GetValue(4),reader.GetValue(5),reader.GetValue(6)));
                        }
                        //выясняем, сколько всего записей в таблице "Names"
                        hazardsCount = hazards.Count;
                    }
                }
                connection.Close();
            }
            Debug.Log("Hazards read: " + hazardsCount);
            Debug.Log("First hazard: " + hazards[0].Name);
        }
        /*
        private object db;
        private List<Hazard> hazards;

        readonly Dictionary<int, int> simpleExpCostList = new Dictionary<int, int>()
            {
                {2, -4 },
                {3, -3 },
                {4, -2 },
                {6, -1},
                {8, 0 },
                {12, 1 },
                {16, 2 },
                {24, 3 },
                {32, 4 }
            };

        readonly Dictionary<int, int> complexExpCostList = new Dictionary<int, int>()
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

        public HazardGenerator()
        {

        }

        public HazardGenerator(ApplicationContext _db)
        {
            this.db = _db;
        }
        Dictionary<int, List<Hazard>> simple_hazards_by_lvl = new Dictionary<int, List<Hazard>>();
        Dictionary<int, List<Hazard>> complex_hazards_by_lvl = new Dictionary<int, List<Hazard>>();
        Hazard hazard;

        public List<Hazard> BuildHazard(int xpbudget, int party_level)
        {
            hazards = new List<Hazard>();
            System.Console.Write($"----Всего опыта на ловушки: - {xpbudget}\n");
            Random rnd = new Random();
            while (xpbudget >= 2)
            {
                xpbudget = BuildSimpleHazard(xpbudget, party_level);
                if (party_level == 1 && xpbudget < 4) break;
                if (party_level == 2 && xpbudget < 3) break;
            }
            //Если уровень группы > 2 && xpbudget >= 10, то
            //Рандомно определяем, простая или сложная
            //Иначе простая
            return hazards;
           
        }

        public int BuildSimpleHazard(int xpbudget, int party_level)
        {
            Random rnd = new Random();
            int hazcostindex;
            while (true) {
                switch (party_level)
                {
                    case 1: //Для группы уровня "1"
                        hazcostindex = rnd.Next(2, 8);
                        break;
                    case 2://Для группы уровня "2"
                        hazcostindex = rnd.Next(1, 8);
                        break;
                    default://Для остальных разницы нет
                        if (xpbudget == 2) hazcostindex = 0;
                        else hazcostindex = rnd.Next(0, 8);
                        break;
                        //TODO: обработать случаи для уровней, близких к 20.
                }
                int hazcost = simpleExpCostList.ElementAt(hazcostindex).Key; // стоимость моба по бюджету
                Console.WriteLine($"Пробуем генерировать ловушку стоимостью: {hazcost}");
                if (hazcost > xpbudget) continue;
                //System.Console.Write($"Индекс стоимости по таблице - {hazcost}\n");

                int hazlvl = party_level + simpleExpCostList.ElementAt(hazcostindex).Value; // лвл исходя из уровня пати
                                                                                            //Console.WriteLine($"Пытаемся достать челика с {monlvl}\n");
                if (!simple_hazards_by_lvl.ContainsKey(hazlvl)) //Проверка, сохраняли ли список монстров этого уровня
                {
                    List<Hazard> lvl_list = db.Hazards.Where(x => x.Level == hazlvl && x.Complexity == "Простая").ToList(); //Сохраняем, если нет
                    simple_hazards_by_lvl[hazlvl] = lvl_list;
                }
                int max_mon_amount = simple_hazards_by_lvl[hazlvl].Count; //Сколько всего монстров этого уровня?
                hazard = simple_hazards_by_lvl[hazlvl][rnd.Next(0, max_mon_amount)]; //Случайный монстр этого уровня
                hazards.Add(hazard);
                xpbudget -= hazcost; //Потратили опыт на этого монстра.
                Console.WriteLine($"Сгенерировали ловушку: {hazard.Name} / {hazard.Level}; Осталось опыта на ловушки: {xpbudget}");

                return xpbudget;
            }
            
        }*/
    }
}