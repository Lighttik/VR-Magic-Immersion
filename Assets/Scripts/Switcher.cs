using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher : MonoBehaviour
{
    public GameObject fireParticleSystem;
    public GameObject waterParticleSystem;
    public GameObject smokeParticleSystem;

    private string current;

    public void Switch(String keyword)
    {
        if (keyword.ToLower().Contains("fire"))
        {
            //DisableAllSystems();
            //fireParticleSystem.SetActive(true);
            current = "fire";

        } else if (keyword.ToLower().Contains("water"))
        {
            //DisableAllSystems();
            //waterParticleSystem.SetActive(true);
            current = "water";
        } else if (keyword.ToLower().Contains("smoke"))
        {
            //DisableAllSystems();
            //smokeParticleSystem.SetActive(true);
            current = "smoke";
        }
    }

    public void DisableAllSystems()
    {
        fireParticleSystem.SetActive(false);
        waterParticleSystem.SetActive(false);
        smokeParticleSystem.SetActive(false);
    }

    public void EnableCurrent()
    {
        if (current.Contains("fire"))
        {
            DisableAllSystems();
            fireParticleSystem.SetActive(true);
        }
        if (current.Contains("water"))
        {
            DisableAllSystems();
            waterParticleSystem.SetActive(true);
        }
        if (current.Contains("smoke"))
        {
            DisableAllSystems();
            smokeParticleSystem.SetActive(true);
        }
    }
}
