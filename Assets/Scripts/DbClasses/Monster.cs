using System;

namespace DbClasses
{
    class Monster
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MechDescription { get; set; }
        public int Level { get; set; }

        public Monster()
        {

        }

        public Monster(int id, string Name, string Description, string MechDescription, int Level)
        {
            this.id = id;
            this.Name = Name;
            this.Description = Description;
            this.MechDescription = MechDescription;
            this.Level = Level;
        }

        public static Monster ToMonster(object _id, object _Name, object _Description, object _MechDescription, object _Level)
        {
            int id = Convert.ToInt32(_id);
            string Name = _Name.ToString();
            string Description = _Description.ToString();
            string MechDescription = _MechDescription.ToString();
            int Level = Convert.ToInt32(_Level);
            return new Monster(id, Name, Description, MechDescription, Level);
        }        

        public override string ToString()
        {
            return Name;
        }
    }
}
