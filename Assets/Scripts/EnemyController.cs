using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public State state;
    public Material fire;
    public Material water;
    public Material smoke;
    
    private int lives;

    private Spawner spawner;

    private Animator animator;
    private VRPlayer player;
    public enum State
    {
        WATER,
        FIRE,
        SMOKE
    }
    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.Find("SpawnPoints").GetComponent<Spawner>();
        animator = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("PlayerPos").GetComponent<VRPlayer>();
        
        state = (State) Random.Range(0, 3);
        lives = Random.Range(2, 5);
        SetUpEnemy();
        
        print("BEHOLDER POS " + transform.position + " " + Camera.main.WorldToScreenPoint(transform.position));
    }

    void SetUpEnemy()
    {
        ChangeColor();
        ChangeLayer();
    }

    void ChangeColor()
    {
        var cubeRenderer = gameObject.GetComponentInChildren(typeof(SkinnedMeshRenderer));
        var mesh = cubeRenderer.GetComponent<SkinnedMeshRenderer>();
        var color = "_Color";
        if (state == State.FIRE) mesh.material = fire;
        if (state == State.WATER) mesh.material = water;
        if (state == State.SMOKE) mesh.material = smoke;
    }

    public void IsHit()
    {
        State previousState = state;
        state = (State) Random.Range(0, 3);

        while (previousState == state)
        {
            state = (State) Random.Range(0, 3);
        }
        lives -= 1;
        if (lives < 1)
        {
            Die();
        }
        else
        {
            SetUpEnemy();
        }
    }

    void Die()
    {
        StopAnimations();
        animator.SetBool("Alive",false);
        Destroy(gameObject, 2f);
    }

    void ChangeLayer()
    {
        if (state == State.FIRE) gameObject.layer = 7;
        if (state == State.WATER) gameObject.layer = 6;
        if (state == State.SMOKE) gameObject.layer = 8;
    }

    public void AttackPlayer()
    {
        player.TakeDamage();
        if (player.IsDead()) CelebrateWin();
    }

    void CelebrateWin()
    {
        StopAnimations();
        animator.SetInteger("StayAnimation",Random.Range(3,5));
    }

    void StopAnimations()
    {
        GetComponent<AgentMove>().StopCoroutine("StayInPlace");
        animator.SetInteger("StayAnimation",0);
    }
}
