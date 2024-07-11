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

        public Monster(string Name, string Description, string MechDescription, int Level)
        {
            this.Name = Name;
            this.Description = Description;
            this.MechDescription = MechDescription;
            this.Level = Level;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
