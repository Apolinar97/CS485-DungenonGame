using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamelessGame.Combat
{
    public class Resistances
    {
        public float PhysicalResist { get; set; }
        public float MagicalResist { get; set; }

        public Resistances(float physicalResist, float magicalResist)
        {
            SetPhysicalResist(physicalResist);
            SetMagicalResist(magicalResist);
        }

        public void SetPhysicalResist(float physcialResist)
        {
            this.PhysicalResist = physcialResist;
        }

        public void SetMagicalResist(float magicalResist)
        {
            this.MagicalResist = magicalResist;
        }
    }
}
