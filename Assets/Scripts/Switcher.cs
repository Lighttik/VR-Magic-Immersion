using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher : MonoBehaviour
{
    public GameObject fireParticleSystem;
    public GameObject waterParticleSystem;
    public GameObject smokeParticleSystem;

    public void Switch(String keyword)
    {
        if (keyword.ToLower().Contains("fire"))
        {
            DisableAllSystems();
            fireParticleSystem.SetActive(true);

        } else if (keyword.ToLower().Contains("water"))
        {
            DisableAllSystems();
            waterParticleSystem.SetActive(true);
        } else if (keyword.ToLower().Contains("smoke"))
        {
            DisableAllSystems();
            smokeParticleSystem.SetActive(true);
        }
    }

    private void DisableAllSystems()
    {
        fireParticleSystem.SetActive(false);
        waterParticleSystem.SetActive(false);
        smokeParticleSystem.SetActive(false);
    }
}
