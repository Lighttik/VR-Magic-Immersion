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
        //spawner = GameObject.Find("SpawnPlatform").GetComponent<Spawner>();
        
        cam = Camera.main;
    }
    
    private void Update1()
    {
        var m_particles = new ParticleSystem.Particle[ps.main.maxParticles];
        int alive = ps.GetParticles(m_particles);

        //monsters = spawner.monsters;
        
        for (int i = 0; i < alive; i++)
        {
            //print("==============================================");
            //print(m_particles[i].position + " ah");
            //print(Camera.main.WorldToScreenPoint(m_particles[i].position));
            Vector2 partPos = cam.WorldToScreenPoint(m_particles[i].position);
            foreach (var monster in monsters)
            {
                Vector2 monsterPos = cam.WorldToScreenPoint(monster.transform.position);

                float dist = Vector2.Distance(partPos, monsterPos);
                print("/////////////////////////////////////////////////////////");
                print(partPos + " " + monsterPos);
                print("DISTANCE: " + dist);
                if (dist < 100) OnParticleCollision(monster);
            }
        }
        
        print(monsters.Count);
    }

    private void OnParticleCollision(GameObject other)
    {
        print("HIT0");
        var controller = other.GetComponent<EnemyController>();
        if (controller != null)
        {
            print("HIT1");
            if (controller.state == EnemyController.State.FIRE && name == "FlameThrower") controller.IsHit();
            if (controller.state == EnemyController.State.WATER && name == "Shower") controller.IsHit();
            if (controller.state == EnemyController.State.SMOKE && name == "SmokeEffect") controller.IsHit();
            //Debug.Log("collide");
        }
    }
    
    
}
