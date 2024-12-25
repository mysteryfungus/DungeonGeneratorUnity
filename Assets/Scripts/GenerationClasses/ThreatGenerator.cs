using DbClasses;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GenerationClasses
{
    
    class ThreatGenerator : ObjectGenerator
    {
        private MonsterGenerator monsterGen;
        private HazardGenerator hazardGen;
        private int party_member_amount;
        private int party_level;
        private List<Monster> temp_monsters;
        private List<Hazard> temp_hazards;
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
        
        public ThreatGenerator()
        {
            this.monsterGen = new MonsterGenerator();
            this.hazardGen = new HazardGenerator();
        }
        //BuildEncounter генерирует для всех комнат сразу
        public void BuildEncounter(int party_member_amount, int party_level, int room_amount, bool useHumansInBattle, bool useHazards)
        {
            System.Random random = new System.Random();
            this.party_member_amount = party_member_amount;
            this.party_level = party_level;
            List<Tuple<int, List<Monster>, List<Hazard>>> room_contents = new List<Tuple<int, List<Monster>, List<Hazard>>>();
            // При trivial или low сложностях группа 1 игрока 1 уровня не может набрать необходимое количество опыта для 1 монстра
            // if (party_member_amount == 1 && party_level == 1 && (difficulty == "Low" || difficulty == "Trivial")) difficulty = "Moderate";
            for (int i  = 1; i <= room_amount; i++)
            {
                String difficulty = RandomDifficulty(random);
                Debug.Log($"Комната #{i} Сложность: {difficulty}\n");
                BuildRoom(difficulty, useHumansInBattle, useHazards);
                room_contents.Add(Tuple.Create(i, temp_monsters, temp_hazards));
            }
            //rooms.Add(room_contents);
            //SaveToFile(room_contents);
        }
        //случайно выбрать сложность, чаще всего выпадает средняя
        public string RandomDifficulty(System.Random random)
        {
            double randomNumber = random.NextDouble();
            switch (randomNumber)
            {
                case < 0.1:
                    return "Trivial"; //10%
                case < 0.25:
                    return "Low"; //15%
                case < 0.77:
                    return "Moderate"; //53%
                case < 0.93:
                    return "Severe"; //15%
                default:
                    return "Extreme"; //7%
            }
        }
        //BuildRoom строит одну комнату
        public void BuildRoom(string difficulty, bool useHumansInBattle, bool useHazards)
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
             * else monsterGenerator.BuildCombat(xpbudget)*/

            if (useHazards && (int)(xpbudget * 0.1) >= 4)
            {
                temp_monsters = monsterGen.BuildCombat((int)(xpbudget * 0.8), party_level, useHumansInBattle);
                temp_hazards = hazardGen.BuildHazard((int)(xpbudget * 0.2), party_level);
            } else
            {
                temp_monsters = monsterGen.BuildCombat(xpbudget, party_level, useHumansInBattle);
            }
        }

    }
}