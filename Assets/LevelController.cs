using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update
    private Spawner spawner;
    public List<int> MonstersCountByLevel;

    private GameObject player;

    private int SceneNumber;
    void Start()
    {
        player = GameObject.Find("OVRPlayerController Variant");
        
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(GameObject.Find("UIHelpers"));
        
        

        SceneNumber = 1;

        //NextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            NextLevel();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneNumber, LoadSceneMode.Single);

        if (SceneManager.GetActiveScene().buildIndex != SceneNumber)
        {
            StartCoroutine(waitForSceneLoad(SceneNumber));
        }
    }
    
    IEnumerator waitForSceneLoad(int sceneNumber)
    {
        while (SceneManager.GetActiveScene().buildIndex != sceneNumber)
        {
            yield return null;
        }
 
        // Do anything after proper scene has been loaded
        if (SceneManager.GetActiveScene().buildIndex == sceneNumber)
        {
            Debug.Log("Finished loading");
            spawner = GameObject.Find("SpawnPoints").GetComponent<Spawner>();
            spawner.MonstersToSpawnCount = MonstersCountByLevel[0];
            spawner.StartCoroutine("Spawn");
            
            MonstersCountByLevel.RemoveAt(0);

            player.transform.position = GameObject.Find("PlayerPos").transform.position;
            
            SceneNumber++;
        }
    }
}
