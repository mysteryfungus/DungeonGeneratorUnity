using System;
using UnityEngine;

namespace DbClasses
{
    class Name
    {
        public int Id { get; set; }
        public string Base { get; set; }
        public string SingularNominative { get; set; }
        public string SingularGenitive { get; set; }
        public string PluralNominative { get; set; }
        public string PluralGenitive { get; set; }
        public Gender Gender { get; set; }

        public Name(string Base, string SingularNominative, string SingularGenitive, string PluralNominative, string PluralGenitive, Gender gender)
        {
            this.Base = Base;
            this.SingularNominative = SingularNominative;
            this.SingularGenitive = SingularGenitive;
            this.PluralNominative = PluralNominative;
            this.PluralGenitive = PluralGenitive;
            this.Gender = gender;
        }

        public static Name ToName(object _Base, object _SingularNominative, object _SingularGenitive, object _PluralNominative, object _PluralGenitive, object _gender)
        {
            string Base = _Base.ToString();
            string SingularNominative = _SingularNominative.ToString();
            string SingularGenitive = _SingularGenitive.ToString();
            string PluralNominative = _PluralNominative.ToString();
            string PluralGenitive = _PluralGenitive.ToString();
            Gender gender = ToGender(Convert.ToInt32(_gender));
            return new Name(Base, SingularNominative,  SingularGenitive,  PluralNominative, PluralGenitive, gender);
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

        public string SingNom()
        {
            return this.Base + this.SingularNominative;
        }

        public string SingGen()
        {
            return this.Base + this.SingularGenitive;
        }
        public string PluralNom()
        {
            return this.Base + this.PluralNominative;
        }
        public string PluralGen()
        {
            return this.Base + this.PluralGenitive;
        }
    }
}
