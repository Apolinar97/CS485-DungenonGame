using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using NamelessGame.Combat;

public class CombatantScript : MonoBehaviour
{
    public float healthPool = 100;
    public float magicResist = 0;
    public float physicalResist = 0;
    public float attackPower = 0;
    public float magicPower = 0;
    public float currentHealth = 1;

    private Combatant objCombatant;

    public event Action<float> OnHealthChanged = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        objCombatant = new Combatant(healthPool, magicResist, physicalResist, attackPower, magicPower);
        currentHealth = objCombatant.CombatantHealth.CurrentHealth;
    }

    void UpdateHealth()
    {
        this.currentHealth = objCombatant.CombatantHealth.CurrentHealth;
        float currentHealthPercent = (float)currentHealth / (float)healthPool;
        OnHealthChanged(currentHealthPercent);

        if (objCombatant.CombatantHealth.HealthDepleated)
            DeathTrigger();
    }

    public void DamageCombatant(CombatAttack atk)
    {
        objCombatant.DamageCombatant(atk);
        UpdateHealth();
    }

    void DeathTrigger()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
