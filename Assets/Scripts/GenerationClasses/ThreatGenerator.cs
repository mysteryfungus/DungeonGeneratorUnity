using DbClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
namespace GenerationClasses
{
    class ThreatGenerator
    {
        private ApplicationContext db;
        private MonsterGenerator monsterGenerator;
        private HazardGenerator hazardGenerator;
        private int party_member_amount;
        private int party_level;
        private int room_amount;
        private List<Tuple<int, List<Monster>, List<Hazard>>> room_contents = new List<Tuple<int, List<Monster>, List<Hazard>>>();
        private List<Monster> temp_monsters;
        private List<Hazard> temp_hazards = new List<Hazard>();
        /*
         * 1, List<Monster>, List<Hazard>
         * 2, List<Monster>, List<Hazard>
         * 
         *'/
        public ThreatGenerator()
        {

        }

        public ThreatGenerator(ApplicationContext _db)
        {
            this.db = _db;
            monsterGenerator = new MonsterGenerator(db);
            hazardGenerator = new HazardGenerator(db);
        }

        readonly Dictionary<string, int> xb_by_difficulty = new Dictionary<string, int>()
            {
                {"Trivial", 40 },
                {"Low", 60 },
                {"Moderate", 80 },
                {"Severe", 120 },
                {"Extreme", 160 }
            };
        readonly Dictionary<string, int> xp_by_party_size = new Dictionary<string, int>()
            {
                {"Trivial", 10 },
                {"Low", 15 },
                {"Moderate", 20 },
                {"Severe", 30 },
                {"Extreme", 40 }
            };
        
        public void Init()
        {
            //Инициализация себя и MonsterGenerator + HazardGenerator
        }

        public void BuildEncounter(int party_member_amount, int party_level, int room_amount)
        {
            Random random = new Random();
            this.party_member_amount = party_member_amount;
            this.party_level = party_level;
            this.room_amount = room_amount;
            // При trivial или low сложностях группа 1 игрока 1 уровня не может набрать необходимое количество опыта для 1 монстра
            // if (party_member_amount == 1 && party_level == 1 && (difficulty == "Low" || difficulty == "Trivial")) difficulty = "Moderate";
            for (int i  = 1; i <= room_amount; i++)
            {
                String difficulty = RandomDifficulty(random);
                System.Console.Write($"Комната #{i} Сложность: {difficulty}\n");
                BuildRoom(difficulty);
                room_contents.Add(Tuple.Create(i, temp_monsters, temp_hazards));
                System.Console.Write($"МОНСТРОВ: {temp_monsters.Count}; ЛОВУШЕК: {temp_hazards.Count}\n\n");
            }
            System.Console.Write($"Сгенерировано приколов: {room_contents.Count}\n");
            //SaveToFile(room_contents);
        }
        public string RandomDifficulty(Random random)
        {
            double randomNumber = random.NextDouble();
            if (randomNumber < 0.1) return "Trivial"; //10%
            else if (randomNumber < 0.25) return "Low"; //15%
            else if (randomNumber < 0.77) return "Moderate"; //53%
            else if (randomNumber < 0.93) return "Severe"; //15%
            else return "Extreme"; //7%
        }

        public void BuildRoom(string difficulty)
        {
            int xpbudget = xb_by_difficulty[difficulty];
            // Вариативность от кол-ва людей в пати отн. сложности
            if (party_member_amount != 4)
            {
                int xp_change = (party_member_amount - 4) * xp_by_party_size[difficulty];
                xpbudget += xp_change;
            }
            //Ниже примерно представлено, как будет работать распределение опыта: небольшой процент уходит в генерацию ловушек
            //но только при условии, что хватит на одну простую ловушку, которая на 1 уровень ниже уровня игроков
            /* if(HazardCheck && ((int)(xpbudget * 0.9)>=4)) {
             * monsterGenerator.BuildCombat((int)(xpbudget*0.9))
             * hazardGenerator.BuildHazard((int)(xpbudget*0.1))
             * }
             * else monsterGenerator.BuildCombat(xpbudget)*'/
            if ((int)(xpbudget * 0.1) >= 4)
            {
                temp_monsters = monsterGenerator.BuildCombat((int)(xpbudget * 0.9), party_level);
                temp_hazards = hazardGenerator.BuildHazard((int)(xpbudget * 0.1), party_level);
            } else
            {
                temp_monsters = monsterGenerator.BuildCombat(xpbudget, party_level);
            }
        }
    }
}
*/