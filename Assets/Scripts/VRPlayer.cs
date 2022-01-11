using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayer : MonoBehaviour
{
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    public void TakeDamage()
    {
        health = health > 0 ? health - 5 : 0;
    }

    public bool IsDead()
    {
        return health == 0;
    }
}
