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
    
    public List<int> MonstersCountByLevel;
    public List<int> MonstersLivesByLevel;
    public List<int> MonstersSpeedByLevel;
    public List<int> MonsterSpawnTimeByLevel;
    public List<string> sceneNames;

    public GameObject healthCanvas;
    public GameObject winUI;
    public GameObject lostUI;
    public GameObject lastLossUI;
    public GameObject staticCanvas;
    public List<GameObject> staticCanvasChildren;
    public GameObject totalWinUI;
    public Text scoreUI;
    
    
    private GameObject player;
    private VRPlayer playerScript;
    
    private int SceneNumber;
    private bool restart;
    private bool UiShown;
    
    private Spawner spawner;
    

    // Particle objects
    public GameObject spawnParticle;
    private MeshRenderer meshParticle;
    private ParticleSystem psParticle;

    void Start()
    {
        player = GameObject.Find("OVRPlayerController Variant");
        playerScript = player.GetComponentInChildren<VRPlayer>();
        
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(GameObject.Find("UIHelpers"));
        DontDestroyOnLoad(GameObject.Find("App Voice Experience"));
        DontDestroyOnLoad(GameObject.Find("StaticCanvas"));

        meshParticle = spawnParticle.GetComponent<MeshRenderer>();
        meshParticle.enabled = false;
        psParticle = spawnParticle.GetComponentInChildren<ParticleSystem>();

        UiShown = false;

        SceneNumber = 0;

    }

    void StartPort()
    {
        meshParticle.enabled = true;
        psParticle.Play();
    }

    bool FinishedPort()
    {
        if (!psParticle.isPlaying)
        {
            meshParticle.enabled = false;
            return true;
        }
        return false;
    }

    bool HasPlayerWon()
    {
        return playerScript.MonstersKilledInCurrentLevel() == MonstersCountByLevel[SceneNumber - 1];
    }

    
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
                    UiShown = true;
                }

                else if (HasPlayerWon())
                {
                    Debug.Log("player win");
                    StartCoroutine("ShowUI", winUI);
                    UiShown = true;
                }
            }
        }

        if (SceneNumber == 4 && playerScript.IsDead()) 
        {
            StartCoroutine("ShowUI", lastLossUI);
            UiShown = true;
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
        StartPort();
        restart = true;
        StopAllCoroutines();
        SceneNumber = 1;
        playerScript.Restart();
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
        StartPort();
        SceneNumber++;
        StartCoroutine(waitForSceneLoad(SceneNumber));

        UiShown = false;
    }

    private void PrepareScoreScene()
    {
        staticCanvas.SetActive(true);
        totalWinUI.SetActive(true);
        healthCanvas.SetActive(false);
        
        int userScore = ComputeUserScore();
        
        scoreUI.text = userScore.ToString();
        Debug.Log("user score" + userScore);

    }

    private int ComputeUserScore()
    {
        int healthAtLevel4 = playerScript.GetHealthAtLevel(3);
        int totalMonstersKilled = playerScript.TotalMonstersKilled();

        return healthAtLevel4 * totalMonstersKilled;
    }

    private void SetUpSpawner(int sceneNumber)
    {
        // setup spawner properties
        spawner = GameObject.Find("SpawnPoints").GetComponent<Spawner>();
        spawner.MonstersToSpawnCount = MonstersCountByLevel[sceneNumber-1];
        spawner.MonstersLives = MonstersLivesByLevel[sceneNumber - 1];
        spawner.MonsterSpeed = MonstersSpeedByLevel[sceneNumber - 1];
        spawner.SpawnTime = MonsterSpawnTimeByLevel[sceneNumber - 1];
    }
    
    
    IEnumerator waitForSceneLoad(int sceneNumber)
    {
        while (!FinishedPort())
        {
            yield return null;
        }

        SceneManager.LoadScene(sceneNames[SceneNumber],LoadSceneMode.Single);
        
        while (SceneManager.GetActiveScene().name != sceneNames[SceneNumber])
        {
            yield return null;
        }
        
        if (restart) yield return new WaitForSeconds(1);
 
        // Do anything after proper scene has been loaded
        if (SceneManager.GetActiveScene().name == sceneNames[SceneNumber])
        {
            Debug.Log("Finished loading");
            
            DisableUI();
            playerScript.NextLevel();

            if (SceneNumber == 1) healthCanvas.SetActive(true);
            if (SceneNumber == 5)
            {
                PrepareScoreScene();
            }

            if (sceneNumber < 5)
            {
                SetUpSpawner(sceneNumber);

                spawner.StartCoroutine("Spawn"); 
               
            }


            // set player to desired position
            player.transform.position = GameObject.Find("PlayerPos").transform.position;
            
        }
    }
}
