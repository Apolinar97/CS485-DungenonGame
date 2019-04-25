using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NamelessGame.Combat.Abilities;

namespace NamelessGame.Combat
{
    public class Combat
    {
        public string CombatLog { get; set; }

        public Combat()
        {
            this.CombatLog = "";
        }

        public CombatAttack AttackByAbility(Combatant attacker, CombatAbility abilityAttack)
        {
            var attack = new CombatAttack(0, abilityAttack.damageType);
            var cl = new StringBuilder();

            float sourceDamage = abilityAttack.damageMag * abilityAttack.damageMod;
            cl.AppendLine("Source Damage: " + sourceDamage.ToString());

            float stattedDamage = attacker.CombatantStats.CalculateStatMod(sourceDamage, abilityAttack);
            cl.AppendLine("Statted Damage: " + stattedDamage.ToString());
            this.CombatLog += cl.ToString();

            attack.SetAttackDamage(stattedDamage);
            return attack;
        }

        public void DamageCombatant(Combatant target, CombatAttack combatAttack)
        {
            float resistedDamage = target.CombatantResistances.CalcResisted(combatAttack);
            target.CombatantHealth.HealthDamaged(resistedDamage);
        }

    }
}
