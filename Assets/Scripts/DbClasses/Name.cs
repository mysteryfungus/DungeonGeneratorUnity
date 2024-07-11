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
