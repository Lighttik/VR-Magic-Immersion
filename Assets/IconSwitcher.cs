using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconSwitcher : MonoBehaviour
{
    public GameObject noneIcon;

    public GameObject fireIcon;

    public GameObject smokeIcon;

    public GameObject waterIcon;

    private void DisableAll()
    {
        noneIcon.SetActive(false);
        fireIcon.SetActive(false);
        smokeIcon.SetActive(false);
        waterIcon.SetActive(false);
    }

    public void Switch(string keyword)
    {
        if (keyword.ToLower().Contains("fire"))
        {
            DisableAll();
            fireIcon.SetActive(true);
        }
        else if (keyword.ToLower().Contains("water"))
        {
            DisableAll();
            waterIcon.SetActive(true);
        }
        else if (keyword.ToLower().Contains("smoke"))
        {
            DisableAll();
            smokeIcon.SetActive(true);
        }
    }
}
