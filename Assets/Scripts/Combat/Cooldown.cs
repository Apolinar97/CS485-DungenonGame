using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NamelessGame.Combat
{
    public class Cooldown
    {
        public float Time { get; set; }
        private float cdTimer = 0f;

        public Cooldown(float time, bool startOnCooldown = false)
        {
            setTime(time);
            setCooldown(startOnCooldown);
        }

        private void setTime(float time)
        {
            if (time >= 0.0f)
                this.Time = time;
            else
                this.Time = 0.0f;
        }

        private void setCooldown(bool startOnCooldown)
        {
            if (startOnCooldown)
                this.cdTimer = this.Time;
            else
                this.cdTimer = 0.0f;
        }

        public bool Ready()
        {
            if (this.cdTimer <= 0.0f)
                return true;
            else
                return false;
        }

        public void Used()
        {
            this.cdTimer = this.Time;
        }

        public float Tick(float tick)
        {
            if (this.cdTimer > 0.0f)
                this.cdTimer -= tick;
            else
                this.cdTimer = 0.0f;

            return this.cdTimer;
        }

        public float TimeRemaining()
        {
            return this.Time - this.cdTimer;
        }
    }
}
