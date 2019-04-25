using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NamelessGame.Combat.Abilities;


namespace NamelessGame.Combat
{
    public enum StatType
    {
        AttackPower,
        MagicPower
    }

    public class Stats
    {
        public float AttackPower { get; set; }
        public float MagicPower { get; set; }

        public Stats(float attackPower, float magicPower)
        {
            SetAttackPower(attackPower);
            SetMagicPower(magicPower);
        }

        public void SetAttackPower(float attackPower)
        {
            this.AttackPower = attackPower;
        }
        
        public void SetMagicPower(float magicPower)
        {
            this.MagicPower = magicPower;
        }

        public float CalculateStatMod(float sourceDamage, CombatAbility attack)
        {
            float stattedDamage = sourceDamage;
            switch (attack.damageType)
            {
                case DamageType.Physical:
                    stattedDamage += AttackPower;
                    break;
                case DamageType.Magic:
                    stattedDamage += MagicPower;
                    break;
            }
            return stattedDamage;
        }
    }
}
