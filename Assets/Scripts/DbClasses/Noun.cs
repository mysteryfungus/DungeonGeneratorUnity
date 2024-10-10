using System;
using UnityEngine;

namespace DbClasses
{
    class Noun
    {
        //public int Id { get; set; }
        public string Base { get; set; }
        public string SingularNominative { get; set; }
        public string SingularGenitive { get; set; }
        public string PluralNominative { get; set; }
        public string PluralGenitive { get; set; }
        public Gender Gender { get; set; }
        public int Title { get; set; }

        public Noun(string Base, string SingularNominative, string SingularGenitive, string PluralNominative, string PluralGenitive, Gender gender, int title)
        {
            this.Base = Base;
            this.SingularNominative = SingularNominative;
            this.SingularGenitive = SingularGenitive;
            this.PluralNominative = PluralNominative;
            this.PluralGenitive = PluralGenitive;
            this.Gender = gender;
            this.Title = title;
        }

        public static Noun ToNoun(object _Base, object _SingularNominative, object _SingularGenitive, object _PluralNominative, object _PluralGenitive, object _gender, object _title)
        {
            string Base = _Base.ToString();
            string SingularNominative = _SingularNominative.ToString();
            string SingularGenitive = _SingularGenitive.ToString();
            string PluralNominative = _PluralNominative.ToString();
            string PluralGenitive = _PluralGenitive.ToString();
            Gender gender = ToGender(Convert.ToInt32(_gender));
            int title = Convert.ToInt32(_title);
            return new Noun(Base, SingularNominative,  SingularGenitive,  PluralNominative, PluralGenitive, gender, title);
        }

        private static Gender ToGender(int genderString)
        {
            switch (genderString)
            {
                case 1:
                    return Gender.Masculine;
                case 2:
                    return Gender.Feminine;
                case 3:
                    return Gender.Neuter;
                default: 
                    return Gender.None;
            }
        }

        private bool CheckTitle()
        {
            switch (Title)
            {
                case -1:
                    return false;
                case 0:
                    if (new System.Random().Next(100) < 50) return true;
                    else return false;
                case 1:
                    return true;
                default: return false;
            }
        }

        public string SingNom()
        {
            if (CheckTitle()) return this.Base + this.SingularNominative;
            else return this.Base.ToLower() + this.SingularNominative;
        }

        public string SingGen()
        {
            if (CheckTitle()) return this.Base + this.SingularGenitive;
            else return this.Base.ToLower() + this.SingularGenitive;
        }
        public string PluralNom()
        {
            if (CheckTitle()) return this.Base + this.SingularGenitive;
            else return this.Base.ToLower() + this.SingularGenitive;
        }
        public string PluralGen()
        {
            if (CheckTitle()) return this.Base + this.PluralGenitive;
            else return this.Base.ToLower() + this.PluralGenitive;
        }
    }
}
