using System;
using System.Data;

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


        public Name()
        {

        }

        public Name(string Base, string SingularNominative, string SingularGenitive, string PluralNominative, string PluralGenitive, Gender gender)
        {
            this.Base = Base;
            this.SingularNominative = SingularNominative;
            this.SingularGenitive = SingularGenitive;
            this.PluralNominative = PluralNominative;
            this.PluralGenitive = PluralGenitive;
            this.Gender = gender;
        }

        public static Name ToName(DataRow dataRow)
        {
            string Base = dataRow["Base"].ToString();
            string SingularNominative = dataRow["SingularNominative"].ToString();
            string SingularGenitive = dataRow["SingularGenitive"].ToString();
            string PluralNominative = dataRow["PluralNominative"].ToString();
            string PluralGenitive = dataRow["PluralGenitive"].ToString();
            Gender gender = ToGender(dataRow["Gender"].ToString());
            return new Name(Base, SingularNominative,  SingularGenitive,  PluralNominative, PluralGenitive, gender);
        }

        private static Gender ToGender(string genderString)
        {
            switch (genderString)
            {
                case "Masculine":
                    return Gender.Masculine;
                case "Feminine":
                    return Gender.Feminine;
                case "Neuter":
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
