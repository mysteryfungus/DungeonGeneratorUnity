using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbClasses
{
    internal class Hazard
    {
        public int id {  get; set; }
        public string Name { get; set; }
        public string Complexity { get; set; }
        public string Description { get; set; }
        public string MechDescription { get; set; }
        public int Level { get; set; }
        public string Stealth {  get; set; }
        public Hazard() { }
        public Hazard(int id, string name, string complexity, string description, string mechDescription, int level, string stealth)
        {
            this.id = id;
            Name = name;
            Complexity = complexity;
            Description = description;
            MechDescription = mechDescription;
            Level = level;
            Stealth = stealth;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
