using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayer : MonoBehaviour
{
    private int currentHealth;
    private int killedMonstersCount;
    private int totalKilledMonstersCount;
    
    public int maxHealth;
    public int monsterDamage;
    public HealthBar healthBar;

    public Switcher left;
    public Switcher right;

    void Start()
    {
        Restart();

        left = GameObject.Find("SwitcherLeft").GetComponent<Switcher>();
        right = GameObject.Find("SwitcherRight").GetComponent<Switcher>();
    }

    private void Update()
    {
        //Trigger Down
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) FireLeft();
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)) FireRight();

        //Trigger Up
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger)) StopLeft();
        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger)) StopRight();
        
        // for debugging
        if (OVRInput.Get(OVRInput.Button.Three))
        {
            left.Switch("smoke");
            right.Switch("smoke");
        }
        else if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            left.Switch("fire");
            right.Switch("fire");
        }
        else if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            left.Switch("water");
            right.Switch("water");
        }
        
    }

    void FireLeft()
    {
        left.EnableCurrent();
    }
    
    void FireRight()
    {
        right.EnableCurrent();
    }

    void StopLeft()
    {
        left.DisableAllSystems();
    }
    
    void StopRight()
    {
        right.DisableAllSystems();
    }

    public void Restart()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(currentHealth);
        killedMonstersCount = 0;
        totalKilledMonstersCount = 0;
    }

    public void NextLevel()
    {
        totalKilledMonstersCount += killedMonstersCount;
        killedMonstersCount = 0;
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

    public int monstersKilledInCurrentLevel()
    {
        return killedMonstersCount;
    }

    public int totalMonstersKilled()
    {
        return totalKilledMonstersCount;
    }

    public void KilledMonster()
    {
        killedMonstersCount++;
    }
}
