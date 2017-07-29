using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    private NavMeshAgent _agent;

	// Use this for initialization
	void Start ()
	{
	    _agent = GetComponent<NavMeshAgent>();
	}

    public void MoveToPoint(Vector3 _point)
    {
        _agent.SetDestination(_point);
    }
}
