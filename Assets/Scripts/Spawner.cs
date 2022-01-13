using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject monster;

    public int MonstersToSpawnCount;
    public int MonstersLives;

    private MonsterCounter monsterCounter;

    private List<GameObject> monsters;

    private List<GameObject> spawnpoints;
    
    // Start is called before the first frame update
    void Start()
    {
        monsters = new List<GameObject>();
        spawnpoints = new List<GameObject>();
        spawnpoints.AddRange(GameObject.FindGameObjectsWithTag("SpawnPoint"));
    }

    IEnumerator Spawn()
    {
        print("spawning " + MonstersToSpawnCount);
        for (int i = 0; i < MonstersToSpawnCount; i++)
        {
            print("halo");
            yield return new WaitForSeconds(5);
            print("halo");

            GameObject spawnpoint = spawnpoints[Random.Range(0, spawnpoints.Count)];
            GameObject obj = Instantiate(monster,spawnpoint.transform);
            obj.GetComponent<EnemyController>().lives = MonstersLives;
            

            obj.transform.parent = GameObject.FindGameObjectWithTag("SpawnPlat").transform;
            
            print("spawned");
            
            monsters.Add(obj);

            //notify GUI of new monster
            //monsterCounter.AddNewMonster();

            yield return new WaitForSeconds(10);
        }
    }

    private void OnDestroy()
    {
        print("Destroyed?");
    }
}
