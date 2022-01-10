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

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        
        destinations = new List<GameObject>(GameObject.FindGameObjectsWithTag("AgentLocations"));
        agent = gameObject.GetComponent<NavMeshAgent>();

        ChooseNewDestination();
    }

    private void OnTriggerEnter(Collider other)
    {
        //print(other.transform.name);
        if (other.CompareTag("AgentLocations") && other.transform.position == destinationPosition)
        {
            animator.SetInteger("StayAnimation",Random.Range(1,5));

            StartCoroutine("StayInPlace");

        }
    }

    IEnumerator StayInPlace()
    {
        yield return new WaitForSeconds(3);
        ChooseNewDestination();
        animator.SetInteger("StayAnimation",0);
    }

    void ChooseNewDestination()
    {
        destinationIndex = (destinationIndex +  Random.Range(1, destinations.Count-1) ) % destinations.Count;
        destinationPosition = destinations[destinationIndex].transform.position;
        
        agent.SetDestination(destinationPosition);
    }
    
}
