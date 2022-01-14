using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject monster;

    public int MonstersToSpawnCount;
    public int MonstersLives;
    public int SpawnTime;
    public int MonsterSpeed;

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
        yield return new WaitForSeconds(2);
        
        int previousSpawnPoint = -1;
        for (int i = 0; i < MonstersToSpawnCount; i++)
        {
            
            
            // to not have the same spawn point
            int spawnPointInt = NewSpawnPoint(previousSpawnPoint);
            previousSpawnPoint = spawnPointInt;
            
            GameObject spawnpoint = spawnpoints[spawnPointInt];
            
            // set up monster
            GameObject obj = Instantiate(monster,spawnpoint.transform);
            obj.GetComponent<EnemyController>().lives = MonstersLives;
            obj.GetComponent<NavMeshAgent>().speed = MonsterSpeed;

            obj.transform.parent = GameObject.FindGameObjectWithTag("SpawnPlat").transform;

            monsters.Add(obj);
            
            yield return new WaitForSeconds(SpawnTime);
        }
    }

    private int NewSpawnPoint(int previousSpawnPoint)
    {
        int spawnPoint = Random.Range(0, spawnpoints.Count);
        while (spawnPoint == previousSpawnPoint)
        {
            spawnPoint = Random.Range(0, spawnpoints.Count);
        }

        return spawnPoint;

    }

    private void OnDestroy()
    {
        print("Destroyed?");
    }
}
