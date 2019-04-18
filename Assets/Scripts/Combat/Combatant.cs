using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamelessGame.Combat
{
    public class Combatant
    {
        public float HealthPool { get; set; }
        public Resistances CombatantResistances { get; set; }
        public Stats CombatantStats { get; set; }

    }
}
