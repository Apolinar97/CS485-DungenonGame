using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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

        private float CalcResistAmmount(float baseDamage, float resistance)
        {
            if (resistance >= 100)
                return 0;
            if (resistance == 0)
                return baseDamage;

            float resistedPercent = resistance * 1/100;
            float postResistDamage = baseDamage * resistedPercent;

            return postResistDamage;
        }

        public float CalcResisted(CombatAttack attack)
        {
            switch (attack.DamageType)
            {
                case DamageType.Magic:
                    return CalcResistAmmount(attack.AttackDamage, this.MagicalResist);
                case DamageType.Physical:
                    return CalcResistAmmount(attack.AttackDamage, this.PhysicalResist);
                default:
                    return 0;
            }
        }
    }
}
