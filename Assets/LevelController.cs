using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update
    private Spawner spawner;
    public List<int> MonstersCountByLevel;
    public List<int> MonstersLivesByLevel;

    private GameObject player;

    public GameObject canvas;

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

    public void RestartGame()
    {
        SceneNumber = 1;
        player.GetComponent<VRPlayer>().Restart();
        SceneManager.LoadScene(SceneNumber,LoadSceneMode.Single);
        
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneNumber, LoadSceneMode.Single);

        if (SceneNumber == 1) ActivateHealth();

        if (SceneManager.GetActiveScene().buildIndex != SceneNumber)
        {
            StartCoroutine(waitForSceneLoad(SceneNumber));
        }
    }

    void ActivateHealth()
    {
        canvas.SetActive(true);
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
            
            // setup spawner properties
            spawner = GameObject.Find("SpawnPoints").GetComponent<Spawner>();
            spawner.MonstersToSpawnCount = MonstersCountByLevel[sceneNumber-1];
            spawner.MonstersLives = MonstersLivesByLevel[sceneNumber - 1];
            spawner.StartCoroutine("Spawn");
            
            
            // set player to desired position
            player.transform.position = GameObject.Find("PlayerPos").transform.position;
            
            SceneNumber++;
        }
    }
}
