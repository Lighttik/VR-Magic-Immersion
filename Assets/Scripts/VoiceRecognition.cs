using System;
using Oculus.Voice;
using UnityEngine;
using Facebook.WitAi;
using Facebook.WitAi.Lib;
using UnityEngine.UI;


public class VoiceRecognition : MonoBehaviour
{
    private AppVoiceExperience voiceExperience;
    private Switcher switcher;
    public Text text;
    
    private void Start()
    {
        switcher = GameObject.FindWithTag("ParticleSwitcher").GetComponent<Switcher>();
        
        voiceExperience = GetComponent<AppVoiceExperience>();
        voiceExperience.Activate();
        Debug.Log("Activated VOICE");
    }
    
    private void OnEnable()
    {
        voiceExperience.events.OnRequestCreated.AddListener(OnRequestStarted);
    }

    private void OnDisable()
    {
        voiceExperience.events.OnRequestCreated.RemoveListener(OnRequestStarted);
    }

    private void OnRequestStarted(WitRequest r)
    {
        
    }
    
    
    public void OnResponse(WitResponseNode response)
    {
        if (!string.IsNullOrEmpty(response["text"]))
        {
            Debug.Log(response["text"]);
            //switcher.Switch(response["text"]);
        }
        
    }
    
}
