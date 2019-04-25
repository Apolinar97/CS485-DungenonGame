using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NamelessGame.Combat.Abilities;

namespace NamelessGame.Combat
{
    public class CombatAttack
    {
        public float AttackDamage { get; set; }
        public DamageType DamageType { get; set; }

        public CombatAttack(float attackDamage, DamageType damageType)
        {
            SetAttackDamage(attackDamage);
            SetDamageType(damageType);
        }

        public void SetAttackDamage(float attackDamage)
        {
            if (attackDamage < 0)
                this.AttackDamage = 0;
            else
                this.AttackDamage = attackDamage;
        }

        public void SetDamageType(DamageType damageType)
        {
            this.DamageType = damageType;
        }
    }
}
