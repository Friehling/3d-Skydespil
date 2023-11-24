using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class MultiplayerAgent : MonoBehaviour
{
    [SerializeField] private List<Transform> positions = new List<Transform>();

    private NavMeshAgent _agent;
    private int positionIndex = 0;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            NextPosition();

    }

    
    void NextPosition()
    {

        _agent.SetDestination(FindObjectOfType<PlayerController>().transform.position);
    }
}