using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NamelessGame.Combat.Abilities
{
    public class CombatAbility : MonoBehaviour
    {
        public string AbilityRefName;
        public int SharedCooldownPool;
        public Damage Damage;
        public float damageMag;
        public float damageMod;
        public DamageType damageType;
   
        public Cooldown Cooldown;
        public float cooldownTime;
        public float cooldownRemaining;
        public bool onCooldown;

        public bool abilitySignaled;
        

        // Start is called before the first frame update
        void Start()
        {
            Damage = new Damage(damageMag, damageType, damageMod);
            Cooldown = new Cooldown(cooldownTime, onCooldown);
        }

        // Update is called once per frame
        void Update()
        {
            cooldownRemaining = Cooldown.Tick(Time.deltaTime);

            if (abilitySignaled)
            {
                if (Cooldown.Ready())
                {
                    //ability Triggers
                    Cooldown.Used();

                }
                abilitySignaled = false;
            }
        }
    }
}
