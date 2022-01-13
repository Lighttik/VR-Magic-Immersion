using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update
    private Spawner spawner;
    public List<int> MonstersCountByLevel;
    public List<int> MonstersLivesByLevel;
    public List<string> sceneNames;

    private GameObject player;
    private VRPlayer playerScript;

    public GameObject healthCanvas;
    public GameObject winUI;
    public GameObject lostUI;
    public GameObject lastLossUI;
    public GameObject staticCanvas;
    public List<GameObject> staticCanvasChildren;
    public GameObject totalWinUI;
    public Text scoreUI;
    
    private int SceneNumber;
    private bool restart;

    private bool UiShown;

    void Start()
    {
        player = GameObject.Find("OVRPlayerController Variant");
        playerScript = player.GetComponentInChildren<VRPlayer>();
        
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(GameObject.Find("UIHelpers"));
        DontDestroyOnLoad(GameObject.Find("App Voice Experience"));
        DontDestroyOnLoad(GameObject.Find("StaticCanvas"));

        UiShown = false;

        SceneNumber = 0;

        //NextLevel();
    }

    bool HasPlayerWon()
    {
        return playerScript.MonstersKilledInCurrentLevel() == MonstersCountByLevel[SceneNumber - 1];
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneNumber > 0 && SceneNumber < 4)
        {
            if (!UiShown)
            {
                if (playerScript.IsDead())
                {
                    Debug.Log("player dead");
                    StartCoroutine("ShowUI", lostUI);
                    UiShown = !UiShown;
                }

                else if (HasPlayerWon())
                {
                    Debug.Log("player win");
                    StartCoroutine("ShowUI", winUI);
                    UiShown = !UiShown;
                }
            }
        }

        if (SceneNumber == 4 && playerScript.IsDead()) 
        {
            lastLossUI.SetActive(true);
        }
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            NextLevel();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            RestartGame();
        }
    }

    IEnumerator ShowUI(GameObject ui)
    {
        yield return new WaitForSeconds(2);
        ui.SetActive(true);
    }

    public void RestartGame()
    {
        restart = true;
        StopAllCoroutines();
        SceneNumber = 1;
        playerScript.Restart();
        DisableUI();
        SceneManager.LoadScene(sceneNames[SceneNumber],LoadSceneMode.Single);
        StartCoroutine(waitForSceneLoad(SceneNumber));
    }

    private void DisableUI()
    {
        staticCanvas.SetActive(false);
        foreach (GameObject go in staticCanvasChildren)
        {
            go.SetActive(false);
        }
        totalWinUI.SetActive(false);
        
        lastLossUI.SetActive(false);
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
        DisableUI();
        playerScript.NextLevel();
        SceneManager.LoadScene(sceneNames[SceneNumber], LoadSceneMode.Single);

        if (SceneNumber == 1) healthCanvas.SetActive(true);
        if (SceneNumber == 5)
        {
            PrepareScoreScene();
        }

        if (SceneManager.GetActiveScene().name != sceneNames[SceneNumber])
        {
            StartCoroutine(waitForSceneLoad(SceneNumber));
        }
        UiShown = false;
    }

    private void PrepareScoreScene()
    {
        healthCanvas.SetActive(false);
        int userScore = ComputeUserScore();
        
        totalWinUI.SetActive(true);
        scoreUI.text = userScore.ToString();

    }

    private int ComputeUserScore()
    {
        int healthAtLevel4 = playerScript.GetHealthAtLevel(4);
        int totalMonstersKilled = playerScript.TotalMonstersKilled();

        return healthAtLevel4 * totalMonstersKilled;
    }
    
    
    IEnumerator waitForSceneLoad(int sceneNumber)
    {
        while (SceneManager.GetActiveScene().name != sceneNames[SceneNumber])
        {
            yield return null;
        }
        
        if (restart) yield return new WaitForSeconds(1);
 
        // Do anything after proper scene has been loaded
        if (SceneManager.GetActiveScene().name == sceneNames[SceneNumber])
        {
            Debug.Log("Finished loading");

            if (sceneNumber < 5)
            {
               // setup spawner properties
               spawner = GameObject.Find("SpawnPoints").GetComponent<Spawner>();
               spawner.MonstersToSpawnCount = MonstersCountByLevel[sceneNumber-1];
               spawner.MonstersLives = MonstersLivesByLevel[sceneNumber - 1];
               
               print("Setting spawner " + spawner.MonstersToSpawnCount + " " + spawner.MonstersLives);
               spawner.StartCoroutine("Spawn"); 
               print("Disabling UI");
               DisableUI();
            }
            else
            {
                staticCanvas.SetActive(true);
                totalWinUI.SetActive(true);
            }
            
            
            // set player to desired position
            player.transform.position = GameObject.Find("PlayerPos").transform.position;
            
        }
    }
}
