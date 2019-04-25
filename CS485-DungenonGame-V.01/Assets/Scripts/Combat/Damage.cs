using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NamelessGame.Combat
{
    public enum DamageType {
        Physical,
        Magic
    }

    public class Damage
    {
        public float Magnitude { get; set; }
        public float Modifier { get; set; }
        public DamageType Type { get; set; }


        public Damage(float magnitude, DamageType type, float modifier = 1.0f)
        {
            this.Magnitude = magnitude;
            this.Type = type;
            this.Modifier = modifier;
        }

    }
}

