
namespace DbClasses
{
    class Noun
    {
        public int Id { get; set; }
        public string Base { get; set; }
        public string SingularNominative { get; set; }
        public string SingularGenitive { get; set; }
        public string PluralNominative { get; set; }
        public string PluralGenitive { get; set; }
        public Gender Gender { get; set; }
        public int Title { get; set; }


        public Noun()
        {

        }

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

        private bool CheckTitle()
        {
            switch (Title)
            {
                case -1:
                    return false;
                case 0:
                    if (new Random().Next(100) < 50) return true;
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
