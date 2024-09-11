using DbClasses;
using Mono.Data.Sqlite;
using System;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

namespace GenerationClasses
{
    class NameGenerator
    {
        private string dbName;
        System.Random rnd = new System.Random();
        int nameRowsCount = 0;
        int nounRowsCount = 0;
        int adjRowsCount = 0;
        Name found_name = null;
        Noun found_noun = null;
        Adjective found_adjective = null;
        Gender gender;
        string result = "";
        string temp_word = "";
        DataTable namesDT;
        DataTable nounsDT = null;
        DataTable adjDT;

        public NameGenerator()
        {

        }

        public NameGenerator(string _dbName)
        {
            this.dbName = _dbName;
        }

        public string GenerateName()
        {
            result = "";
            using (SqliteConnection connection = new SqliteConnection(dbName)) 
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand()) 
                {
                    command.CommandText = "SELECT * FROM Names";

                    using (SqliteDataReader reader = command.ExecuteReader()) 
                    {
                        //сохраняем результат как DataTable;
                        namesDT = reader.GetSchemaTable(); //Неверная имплементация!
                        //выясняем, сколько всего записей в таблице "Names"
                        nameRowsCount = namesDT.Rows.Count;
                        //Debug.Log(namesDT.Rows[rnd.Next(nounRowsCount) + 1]);
                        foreach (DataRow dr in namesDT.Rows)
                        {
                            string temp_info = "";
                            foreach (var item in dr.ItemArray)
                            {
                                temp_info += item;
                                temp_info += " ";
                            }
                            Debug.Log(temp_info);
                        }
                        //found_name = Name.ToName(namesDT.Rows[rnd.Next(nounRowsCount) + 1]);
                    }
                }
                connection.Close();
            } 
/*
            int rows = db.Names.Count();
            //выбираем случайное
            found_name = db.Names.Find(rnd.Next(rows) + 1);
            gender = found_name.Gender;
*/
            //главный метод класса, вызвающий все остальные
            switch (RandomNameType())
            {
                case 0:
                    return GenerateNameType0();
                /*case 1:
                    return GenerateNameType1();
                case 2:
                    return GenerateNameType2();
                case 3:
                    return GenerateNameType3();
                case 4:
                    return GenerateNameType4();
                case 5:
                    return GenerateNameType5();
                case 6:
                    return GenerateNameType6();
                case 7:
                    return GenerateNameType7();
                case 8:
                    return GenerateNameType8();*/
                default:
                    return "ашыбка";
            }
        }

        private void GetNounsDT()
        {
            using (SqliteConnection connection = new SqliteConnection(dbName)) 
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand()) 
                {
                    command.CommandText = "SELECT * FROM Nouns";

                    using (SqliteDataReader reader = command.ExecuteReader()) 
                    {
                        //сохраняем результат как DataTable;
                        namesDT = reader.GetSchemaTable();
                        //выясняем, сколько всего записей в таблице "Nouns"
                        nameRowsCount = namesDT.Rows.Count;
                    }
                }
                connection.Close();
            } 
        }

        private void GetAdjDT()
        {
            using (SqliteConnection connection = new SqliteConnection(dbName)) 
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand()) 
                {
                    command.CommandText = "SELECT * FROM Adjectives";

                    using (SqliteDataReader reader = command.ExecuteReader()) 
                    {
                        //сохраняем результат как DataTable;
                        adjDT = reader.GetSchemaTable();
                        //выясняем, сколько всего записей в таблице "Adjectives"
                        adjRowsCount = adjDT.Rows.Count;
                    }
                }
                connection.Close();
            } 
        }

        public string CheckTitle()
        {

            return "title";
        }

        private int RandomNameType()
        {
            //return rnd.Next(9);
            return 0;
            //Формулы названий, вернет от 0 до 8
        }

        private string GenerateNameType0()
        {
            if (nounsDT == null) GetNounsDT();
            //Название + сущЕд
            found_noun = Noun.ToNoun(nounsDT.Rows.Find(rnd.Next(nounRowsCount) + 1));
            result = found_name.SingNom() + " " + found_noun.SingGen();
            return result;
            //return "Пещера Гоблина"
        }
/*
        private string GenerateNameType0()
        {
            //Название + сущЕд
            rows = db.Nouns.Count();
            found_noun = db.Nouns.Find(rnd.Next(rows) + 1);
            result = found_name.SingNom() + " " + found_noun.SingGen();
            return result;
            //return "Пещера Гоблина"
        }

        private string GenerateNameType1()
        {
            //Название + сущМн
            rows = db.Nouns.Count();
            found_noun = db.Nouns.Find(rnd.Next(rows) + 1);
            result = found_name.SingNom() + " " + found_noun.PluralGen();
            return result;
            //return "Пещера гоблинов"
        }

        private string GenerateNameType2()
        {
            //Название + прил + сущЕд
            rows = db.Nouns.Count();
            found_noun = db.Nouns.Find(rnd.Next(rows) + 1);
            rows = db.Adjectives.Count();
            found_adjective = db.Adjectives.Find(rnd.Next(rows) + 1);
            gender = found_noun.Gender;

            switch (gender)
            {
                case Gender.Masculine:
                case Gender.Neuter:
                    result = found_name.SingNom() + " " + found_adjective.SingGenOther(found_noun.Title) + " " + found_noun.SingGen();
                    break;
                case Gender.Feminine:
                    result = found_name.SingNom() + " " + found_adjective.SingGenFem(found_noun.Title) + " " + found_noun.SingGen();
                    break;
            }

            return result;
            //return "Пещера Великого гоблина";
        }
        private string GenerateNameType3()
        {
            //Название + прил + сущМн
            rows = db.Nouns.Count();
            found_noun = db.Nouns.Find(rnd.Next(rows) + 1);
            rows = db.Adjectives.Count();
            found_adjective = db.Adjectives.Find(rnd.Next(rows) + 1);
            result = found_name.SingNom() + " " + found_adjective.PluralGen(found_noun.Title) + " " + found_noun.PluralGen();

            return result;
            //return "Пещера Великих Гоблинов";
        }

        private string GenerateNameType4()
        {
            //Прил + название
            rows = db.Adjectives.Count();
            found_adjective = db.Adjectives.Find(rnd.Next(rows) + 1);
            switch (gender)
            {
                case Gender.Masculine : 
                    result = found_adjective.SingNomMasc() + " " + found_name.SingNom().ToLower();
                    break;
                case Gender.Feminine :
                    result = found_adjective.SingNomFem() + " " + found_name.SingNom().ToLower();
                    break;
                case Gender.Neuter:
                    result = found_adjective.SingNomNeuter() + " " + found_name.SingNom().ToLower();
                    break;
            }
            return result;
            //return "Великая пещера";
        }

        private string GenerateNameType5()
        {
            //Прил + название + сущМн
            rows = db.Adjectives.Count();
            found_adjective = db.Adjectives.Find(rnd.Next(rows) + 1);
            rows = db.Nouns.Count();
            found_noun = db.Nouns.Find(rnd.Next(rows) + 1);
            switch (gender)
            {
                case Gender.Masculine:
                    result = found_adjective.SingNomMasc() + " " + found_name.SingNom().ToLower() + " " + found_noun.PluralGen();
                    break;
                case Gender.Feminine:
                    result = found_adjective.SingNomFem() + " " + found_name.SingNom().ToLower() + " " + found_noun.PluralGen();
                    break;
                case Gender.Neuter:
                    result = found_adjective.SingNomNeuter() + " " + found_name.SingNom().ToLower() + " " + found_noun.PluralGen();
                    break;
            }
            return result;
            //return "Великая пещера гоблинов";
        }

        private string GenerateNameType6()
        {
            //Прил + название + сущЕд
            rows = db.Adjectives.Count();
            found_adjective = db.Adjectives.Find(rnd.Next(rows) + 1);
            rows = db.Nouns.Count();
            found_noun = db.Nouns.Find(rnd.Next(rows) + 1);
            switch (gender)
            {
                case Gender.Masculine:
                    result = found_adjective.SingNomMasc() + " " + found_name.SingNom().ToLower() + " " + found_noun.SingGen();
                    break;
                case Gender.Feminine:
                    result = found_adjective.SingNomFem() + " " + found_name.SingNom().ToLower() + " " + found_noun.SingGen();
                    break;
                case Gender.Neuter:
                    result = found_adjective.SingNomNeuter() + " " + found_name.SingNom().ToLower() + " " + found_noun.SingGen();
                    break;
            }
            return result;
            //return "Великая пещера гоблина";
        }

        private string GenerateNameType7()
        {
            //Прил + название + прил + сущМнож
            rows = db.Adjectives.Count();
            found_adjective = db.Adjectives.Find(rnd.Next(rows) + 1);
            switch (gender)
            {
                case Gender.Masculine:
                    temp_word = found_adjective.SingNomMasc() + " " + found_name.SingNom().ToLower();
                    break;
                case Gender.Feminine:
                    temp_word = found_adjective.SingNomFem() + " " + found_name.SingNom().ToLower();
                    break;
                case Gender.Neuter:
                    temp_word = found_adjective.SingNomNeuter() + " " + found_name.SingNom().ToLower();
                    break;
            }
            found_adjective = db.Adjectives.Find(rnd.Next(rows) + 1);
            rows = db.Nouns.Count();
            found_noun = db.Nouns.Find(rnd.Next(rows) + 1);
            result = temp_word + " " + found_adjective.PluralGen(found_noun.Title) + " " + found_noun.PluralGen();

            return result;
            //return "Великая пещера великих гоблинов";
        }

        private string GenerateNameType8()
        {
            //Прил + название + прил + сущЕд
            rows = db.Adjectives.Count();
            found_adjective = db.Adjectives.Find(rnd.Next(rows) + 1);
            switch (gender)
            {
                case Gender.Masculine:
                    temp_word = found_adjective.SingNomMasc() + " " + found_name.SingNom().ToLower();
                    break;
                case Gender.Feminine:
                    temp_word = found_adjective.SingNomFem() + " " + found_name.SingNom().ToLower();
                    break;
                case Gender.Neuter:
                    temp_word = found_adjective.SingNomNeuter() + " " + found_name.SingNom().ToLower();
                    break;
            }
            found_adjective = db.Adjectives.Find(rnd.Next(rows) + 1);
            rows = db.Nouns.Count();
            found_noun = db.Nouns.Find(rnd.Next(rows) + 1);
            gender = found_noun.Gender;
            switch (gender)
            {
                case Gender.Masculine:
                case Gender.Neuter:
                    result = temp_word + " " + found_adjective.SingGenOther(found_noun.Title) + " " + found_noun.SingGen();
                    break;
                case Gender.Feminine:
                    result = temp_word + " " + found_adjective.SingGenFem(found_noun.Title) + " " + found_noun.SingGen();
                    break;
            }

            return result;
            //return "Великая пещера великого гоблина";
        }*/
    }
}