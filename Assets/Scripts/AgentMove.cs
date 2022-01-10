using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AgentMove : MonoBehaviour
{
    private List<GameObject> destinations;
    private NavMeshAgent agent;
    private Vector3 destinationPosition;
    private int destinationIndex;

    private Animator animator;
    private GameObject playerpos;
    private LineRenderer _lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        
        //destinations = new List<GameObject>(GameObject.FindGameObjectsWithTag("AgentLocations"));
        agent = gameObject.GetComponent<NavMeshAgent>();
        
        playerpos = GameObject.FindGameObjectWithTag("PlayerPos");

        _lineRenderer = GetComponent<LineRenderer>();

        _lineRenderer.startWidth = 0.15f;
        _lineRenderer.endWidth = 0.15f;
        _lineRenderer.positionCount = 0;

        ChooseNewDestination();
    }

    private void OnTriggerEnter(Collider other)
    {
        //print(other.transform.name);
        /*if (other.CompareTag("AgentLocations") && other.transform.position == destinationPosition)
        {
            animator.SetInteger("StayAnimation",Random.Range(1,5));

            StartCoroutine("StayInPlace");

        }*/
    }

    IEnumerator StayInPlace()
    {
        yield return new WaitForSeconds(3);
        ChooseNewDestination();
        animator.SetInteger("StayAnimation",0);
    }

    void ChooseNewDestination()
    {
        //destinationIndex = (destinationIndex +  Random.Range(1, destinations.Count-1) ) % destinations.Count;
        //destinationPosition = destinations[destinationIndex].transform.position;
        
        agent.SetDestination(playerpos.transform.position);
        
        print(agent.pathStatus);
    }

    private void Update()
    {
        if (agent.hasPath)
        {
            DrawPath();
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
