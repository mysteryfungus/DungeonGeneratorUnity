using DbClasses;
using System;
using System.Linq;
/*
namespace GenerationClasses
{
    class NameGenerator
    {
        private ApplicationContext db;
        Random rnd = new Random();
        int rows = 0;
        Name found_name = null;
        Noun found_noun = null;
        Adjective found_adjective = null;
        Gender gender;
        string result = "";
        string temp_word = "";

        public NameGenerator()
        {

        }

        public NameGenerator(ApplicationContext _db)
        {
            this.db = _db;
        }

        public string GenerateName()
        {
            result = "";
            //выясняем, сколько всего записей в таблице "Names"
            int rows = db.Names.Count();
            //выбираем случайное
            found_name = db.Names.Find(rnd.Next(rows) + 1);
            gender = found_name.Gender;

            //главный метод класса, вызвающий все остальные
            switch (RandomNameType())
            {
                case 0:
                    return GenerateNameType0();
                case 1:
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
                    return GenerateNameType8();
                default:
                    return "ашыбка";
            }
        }

        public string CheckTitle()
        {

            return "title";
        }

        private int RandomNameType()
        {
            return rnd.Next(9);
            //Формулы названий, вернет от 0 до 8
        }

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
        }
    }
}
*/