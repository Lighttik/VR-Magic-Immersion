using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    private ParticleSystem ps;

    private Spawner spawner;

    private List<GameObject> monsters;

    private Camera cam;
    
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        cam = Camera.main;
    }

    private void OnParticleCollision(GameObject other)
    {
        var controller = other.GetComponent<EnemyController>();
        if (controller != null)
        {
            if (controller.state == EnemyController.State.FIRE && name == "FlameThrower") controller.IsHit();
            if (controller.state == EnemyController.State.WATER && name == "Water") controller.IsHit();
            if (controller.state == EnemyController.State.SMOKE && name == "SmokeEffect") controller.IsHit();
        }
    }
    
    
}
