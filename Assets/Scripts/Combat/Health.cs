using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamelessGame.Combat
{
    public class Health
    {
        public float HealthPool { get; set; }
        public float CurrentHealth { get; set; }
        public bool HealthDepleated { get; set; }

        public Health(float healthPool)
        {
            this.HealthDepleated = false;
            SetHealthPool(healthPool);
        }

        public void SetHealthPool(float healthpool)
        {
            this.HealthPool = healthpool;
            this.CurrentHealth = healthpool;
        }

        public void SetCurrentHealth(float amt)
        {
            if (amt <= 0)
                HealthDepletedTrigger();

            if (amt > this.HealthPool)
                amt = this.HealthPool;

            this.CurrentHealth = amt;
        }

        public void Revive(float healAmmount)
        {
            if (!HealthDepleated)
                return;

            this.HealthDepleated = false;
            HealthHealed(healAmmount);
        }

        public void HealthDamaged(float amt)
        {
            if (HealthDepleated)
                return;
            if (amt < 0)
                return;

            float healthCheck = this.CurrentHealth -= amt;
            if (healthCheck <= 0)
                HealthDepletedTrigger();
            else
            {
                this.CurrentHealth -= amt;
            }
        }

        public void HealthHealed(float amt)
        {
            if (HealthDepleated)
                return;
            if (amt < 0)
                return;

            float healthCheck = this.CurrentHealth += amt;
            if (healthCheck >= this.HealthPool)
                this.CurrentHealth = this.HealthPool;
            else
            {
                this.CurrentHealth += amt;
            }
        }

        private void HealthDepletedTrigger()
        {
            this.CurrentHealth = 0;
            this.HealthDepleated = true;
        }
    }
}
