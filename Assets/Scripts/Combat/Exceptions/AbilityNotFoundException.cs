using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamelessGame.Combat
{
    public class AbilityNotFoundException : Exception
    {
        public AbilityNotFoundException(string abilityName, string description) : base(string.Format("{0}: Not Found In {1}", abilityName, description))
        {
        }
    }
}
