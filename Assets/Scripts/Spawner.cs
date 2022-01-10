using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;

    private MonsterCounter monsterCounter;

    public GameObject topLeft;
    public GameObject bottomRight;

    private Vector3 posTop;
    private Vector3 posBottom;
    
    public List<GameObject> monsters;
    
    // Start is called before the first frame update
    void Start()
    {
        monsterCounter = GameObject.FindWithTag("GUI").GetComponent<MonsterCounter>();
        
        posTop = topLeft.transform.localPosition;
        posBottom = bottomRight.transform.localPosition;
        
        print(posTop);
        print(posBottom);
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        for (;;)
        {
            yield return new WaitForSeconds(5);
            GameObject obj = Instantiate(enemy,transform);
            
            monsters.Add(obj);
            
            var x = Random.Range(posTop.x, posBottom.x);
            var z = Random.Range(posBottom.z, posTop.z);
        
            Vector3 newPos = new Vector3(x,0,z);
            obj.transform.localPosition = newPos;

            //float scaleYZ = obj.transform.localScale.y;
            //obj.transform.localScale = new Vector3(0.4915436f,scaleYZ,scaleYZ);
            //obj.transform.localScale = new Vector3(scaleYZ,scaleYZ,scaleYZ);

            print(obj.transform.position);
            
            //notify GUI of new monster
            monsterCounter.AddNewMonster();

            yield return new WaitForSeconds(10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
