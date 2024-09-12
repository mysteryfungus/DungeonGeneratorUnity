using System;
using UnityEngine;

namespace DbClasses
{
    class Adjective
    {
        public int Id { get; set; }
        public string Base { get; set; }
        public string SingularNominativeFeminine { get; set; }
        public string SingularGenitiveFeminine { get; set; }
        public string SingularNominativeMasculine { get; set; }
        public string SingularNominativeNeuter { get; set; }
        public string SingularGenitiveOther { get; set; }
        public string PluralNominative { get; set; }
        public string PluralGenitive { get; set; }

        public Adjective(string Base, string SingularNominativeFeminine, string SingularGenitiveFeminine, string SingularNominativeMasculine, string SingularNominativeNeuter, string SingularGenitiveOther, string PluralNominative, string PluralGenitive)
        {
            this.Base = Base;
            this.SingularNominativeFeminine = SingularNominativeFeminine;
            this.SingularGenitiveFeminine = SingularGenitiveFeminine;
            this.SingularNominativeMasculine = SingularNominativeMasculine;
            this.SingularNominativeNeuter = SingularNominativeNeuter;
            this.SingularGenitiveOther = SingularGenitiveOther;
            this.PluralNominative = PluralNominative;
            this.PluralGenitive = PluralGenitive;
        }

        public static Adjective ToAdjective(object _Base, object _SingularNominativeFeminine, object _SingularGenitiveFeminine, object _SingularNominativeMasculine, object _SingularNominativeNeuter, object _SingularGenitiveOther, object _PluralNominative, object _PluralGenitive)
        {
            string Base = _Base.ToString();
            string SingularNominativeFeminine = _SingularNominativeFeminine.ToString();
            string SingularGenitiveFeminine = _SingularGenitiveFeminine.ToString();
            string SingularNominativeMasculine = _SingularNominativeMasculine.ToString();
            string SingularNominativeNeuter = _SingularNominativeNeuter.ToString();
            string SingularGenitiveOther = _SingularGenitiveOther.ToString();
            string PluralNominative = _PluralNominative.ToString();
            string PluralGenitive = _PluralGenitive.ToString();
            return new Adjective(Base, SingularNominativeFeminine, SingularGenitiveFeminine, SingularNominativeMasculine, SingularNominativeNeuter, SingularGenitiveOther, PluralNominative, PluralGenitive);
        }

        private bool CheckTitle(int Title)
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

        public string SingNomFem(int Title = 1)
        {
            if (CheckTitle(Title)) return this.Base + this.SingularNominativeFeminine;
            else return this.Base.ToLower() + this.SingularNominativeFeminine;
        }
        public string SingNomMasc(int Title = 1)
        {
            if (CheckTitle(Title)) return this.Base + this.SingularNominativeMasculine;
            else return this.Base.ToLower() + this.SingularNominativeMasculine;
        }
        public string SingNomNeuter(int Title = 1)
        {
            if (CheckTitle(Title)) return this.Base + this.SingularNominativeNeuter;
            else return this.Base.ToLower() + this.SingularNominativeNeuter;
        }
        public string SingGenFem(int Title = 1)
        {
            if (CheckTitle(Title)) return this.Base + this.SingularGenitiveFeminine;
            else return this.Base.ToLower() + this.SingularGenitiveFeminine;
        }
        public string SingGenOther(int Title = 1)
        {
            if (CheckTitle(Title)) return this.Base + this.SingularGenitiveOther;
            else return this.Base.ToLower() + this.SingularGenitiveOther;
        }
        public string PluralNom(int Title = 1)
        {
            if (CheckTitle(Title)) return this.Base + this.PluralNominative;
            else return this.Base.ToLower() + this.PluralNominative;
        }
        public string PluralGen(int Title = 1)
        {
            if (CheckTitle(Title)) return this.Base + this.PluralGenitive;
            else return this.Base.ToLower() + this.PluralGenitive;
        }
    }
}
