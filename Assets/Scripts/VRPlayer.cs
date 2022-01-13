using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayer : MonoBehaviour
{
    private int currentHealth;
    private int killedMonstersCount;
    
    public int maxHealth;
    public int monsterDamage;
    public HealthBar healthBar; 

    void Start()
    {
        Restart();
    }

    public void Restart()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(currentHealth);
    }

    public void TakeDamage()
    {
        currentHealth = currentHealth > 0 ? currentHealth - monsterDamage : 0;
        healthBar.SetHealth(currentHealth);
        
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    public int monstersKilled()
    {
        return killedMonstersCount;
    }

    public void KilledMonster()
    {
        killedMonstersCount++;
    }
}
