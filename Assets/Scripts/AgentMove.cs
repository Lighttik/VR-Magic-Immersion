using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AgentMove : MonoBehaviour
{
    private VRPlayer player;
    private List<GameObject> destinations;
    public NavMeshAgent agent;
    private Vector3 destinationPosition;
    private int destinationIndex;

    private Animator animator;
    private GameObject playerpos;
    private LineRenderer _lineRenderer;

    private EnemyController behaviourController;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        
        playerpos = GameObject.FindGameObjectWithTag("PlayerPos");
        player = playerpos.GetComponent<VRPlayer>();
        behaviourController = GetComponent<EnemyController>();

        _lineRenderer = GetComponent<LineRenderer>();

        _lineRenderer.startWidth = 0.15f;
        _lineRenderer.endWidth = 0.15f;
        _lineRenderer.positionCount = 0;

        ChooseNewDestination();
    }

    private void OnTriggerEnter(Collider other)
    {
        agent.SetDestination(transform.position);
        
        if (player.IsDead())
        {
            behaviourController.CelebrateWin();
        }
        else if (other.CompareTag("PlayerPos"))
        {
            StartCoroutine("StayInPlace");
        }
    }

    string RandomAttack()
    {
        if (Random.Range(0f, 1f) < 0.5)
        {
            return "attack2";
        }

        return "attack1";
    }
    
    

    IEnumerator StayInPlace()
    {
        for (;;)
        {
            animator.SetTrigger(RandomAttack());
            yield return new WaitForSeconds(2);
            behaviourController.AttackPlayer();
            yield return new WaitForSeconds(5);
        }
    }

    void ChooseNewDestination()
    {
        agent.SetDestination(playerpos.transform.position);
    }

    private void Update()
    {
        if (player.IsDead())
        {
            agent.SetDestination(transform.position);
            behaviourController.CelebrateWin();
        }
        
        else if (agent.hasPath)
        {
            //DrawPath();
        }
    }

    void DrawPath()
    {
        _lineRenderer.positionCount = agent.path.corners.Length;
        _lineRenderer.SetPosition(0,transform.position);

        if (agent.path.corners.Length < 2)
        {
            return;
        }

        for (int i = 0; i < agent.path.corners.Length; i++)
        {
            Vector3 pointPosition = new Vector3(agent.path.corners[i].x, agent.path.corners[i].y,agent.path.corners[i].z);
            _lineRenderer.SetPosition(i,pointPosition);
        }
    }
}
