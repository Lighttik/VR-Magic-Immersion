using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayer : MonoBehaviour
{
    public int currentHealth;
    private int killedMonstersCount;
    private int totalKilledMonstersCount;
    private List<int> healthAtEachLevel = new List<int>();

    public int maxHealth;
    public int monsterDamage;
    public HealthBar healthBar;

    public Switcher left;
    public Switcher right;
    public IconSwitcher iconSwitcher;

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
            iconSwitcher.Switch("smoke");
        }
        else if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            left.Switch("fire");
            right.Switch("fire");
            iconSwitcher.Switch("fire");
        }
        else if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            left.Switch("water");
            right.Switch("water");
            iconSwitcher.Switch("water");
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
        healthAtEachLevel.Clear();
    }

    public void NextLevel()
    {
        totalKilledMonstersCount += killedMonstersCount;
        killedMonstersCount = 0;
        healthAtEachLevel.Add(currentHealth);
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

    public int MonstersKilledInCurrentLevel()
    {
        return killedMonstersCount;
    }

    public int TotalMonstersKilled()
    {
        return totalKilledMonstersCount;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void KilledMonster()
    {
        killedMonstersCount++;
    }

    public int GetHealthAtLevel(int level)
    {
        return healthAtEachLevel[level];
    }
}
