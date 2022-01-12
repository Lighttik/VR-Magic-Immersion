using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update
    private Spawner spawner;
    public List<int> MonstersCountByLevel;
    void Start()
    {
        DontDestroyOnLoad(this);
        spawner = GameObject.Find("SpawnPoints").GetComponent<Spawner>();

        spawner.MonstersToSpawnCount = MonstersCountByLevel[0];
        MonstersCountByLevel.Remove(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
