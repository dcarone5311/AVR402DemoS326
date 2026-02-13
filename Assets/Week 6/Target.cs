using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    private NavMeshAgent[] navAgents;
    public Transform targetMarker;
    public float verticalOffset = 10.0f;

    public bool clickTarget;

    Transform player;


    // Start is called before the first frame update
    void Start()
    {
        navAgents = FindObjectsOfType<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void UpdateTargets(Vector3 targetPosition)
    {
        foreach (NavMeshAgent agent in navAgents)
        {
            agent.destination = targetPosition;
        }
    }


    // Update is called once per frame
    void Update()
    {

        //get the point of the hit when mouse is clicked
        if (Input.GetMouseButtonDown(0) && clickTarget)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //find where ray hits floor, set to target location

            if (Physics.Raycast(ray.origin, ray.direction, out var hitInfo))
            {
                Vector3 targetPosition = hitInfo.point;
                UpdateTargets(targetPosition);
                targetMarker.position = targetPosition + verticalOffset * Vector3.up;
            }


        }
        if (!clickTarget)
        {
            foreach (NavMeshAgent agent in navAgents)
            {
                agent.destination = player.position;
            }
        }



        foreach (NavMeshAgent agent in navAgents)
        {
            if (agent.velocity == Vector3.zero)
                agent.gameObject.GetComponentInChildren<Animator>().SetInteger("State", 0);
            else
                agent.gameObject.GetComponentInChildren<Animator>().SetInteger("State", 1);
        }

    }
}
