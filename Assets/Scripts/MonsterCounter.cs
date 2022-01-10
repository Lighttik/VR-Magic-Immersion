using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterCounter : MonoBehaviour
{
    public int maxMonsterCount = 10;
    
    private int monsterCount = 0;
    private Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        
        print("text: " + text);
        UpdateText();
    }

    public void AddNewMonster()
    {
        monsterCount++;
        UpdateText();   
    }

    private void UpdateText()
    {
        print("text: " + text);
        text.text = "Monsters: " + monsterCount + "/" + maxMonsterCount;
    }
}
