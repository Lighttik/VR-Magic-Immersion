using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject monster;

    private MonsterCounter monsterCounter;

    private List<GameObject> monsters;

    private List<GameObject> spawnpoints;
    
    // Start is called before the first frame update
    void Start()
    {
        monsters = new List<GameObject>();
        spawnpoints = new List<GameObject>();
        spawnpoints.AddRange(GameObject.FindGameObjectsWithTag("SpawnPoint"));
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        for (;;)
        {
            yield return new WaitForSeconds(5);

            GameObject spawnpoint = spawnpoints[Random.Range(0, spawnpoints.Count - 1)];
            GameObject obj = Instantiate(monster,spawnpoint.transform);

            obj.transform.parent = GameObject.FindGameObjectWithTag("SpawnPlat").transform;
            
            print("spawned");
            
            monsters.Add(obj);

            //notify GUI of new monster
            //monsterCounter.AddNewMonster();

            yield return new WaitForSeconds(10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
