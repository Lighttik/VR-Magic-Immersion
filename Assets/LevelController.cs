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
    private VRPlayer playerScript;

    public GameObject canvas;
    public GameObject winUI;
    public GameObject lostUI;
    
    private int SceneNumber;
    void Start()
    {
        player = GameObject.Find("OVRPlayerController Variant");
        playerScript = player.GetComponentInChildren<VRPlayer>();
        
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(GameObject.Find("UIHelpers"));
        
        

        SceneNumber = 0;

        //NextLevel();
    }

    bool HasPlayerWon()
    {
        return playerScript.monstersKilled() == MonstersCountByLevel[SceneNumber - 1];
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneNumber > 0)
        {
            if (playerScript.IsDead())
            {
                Debug.Log("player dead");
                lostUI.SetActive(true);
            }

            if (HasPlayerWon())
            {
                Debug.Log("player win");
                winUI.SetActive(true);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            NextLevel();
        }
    }

    public void RestartGame()
    {
        SceneNumber = 1;
        playerScript.Restart();
        DisableUI();
        SceneManager.LoadScene(1,LoadSceneMode.Single);
        
    }

    private void DisableUI()
    {
        lostUI.SetActive(false);
        winUI.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void NextLevel()
    {
        SceneNumber++;
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
            
        }
    }
}
