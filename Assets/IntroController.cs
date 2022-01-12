using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    public List<GameObject> UIs;
    private int UInumber;
    
    // Start is called before the first frame update
    void Start()
    {
        UInumber = 0;
    }

    public void Next()
    {
        UIs[UInumber].SetActive(false);
        UInumber++;
        UIs[UInumber].SetActive(true);
        
    }
    
    
}
