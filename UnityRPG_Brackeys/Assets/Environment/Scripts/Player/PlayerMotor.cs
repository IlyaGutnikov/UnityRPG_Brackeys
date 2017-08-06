﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    private Transform _target;
    private NavMeshAgent _agent;

	// Use this for initialization
	void Start ()
	{
	    _agent = GetComponent<NavMeshAgent>();
	}

    void Update()
    {
        if (_target != null)
        {
            _agent.SetDestination(_target.position);
            FaceTarget();
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void MoveToPoint(Vector3 point)
    {
        _agent.SetDestination(point);
    }

    public void FollowTarget(Interactable newTarget)
    {
        _agent.stoppingDistance = newTarget.Radius * .8f;
        _agent.updateRotation = false;

        _target = newTarget.InteractionTransform;
    }

    public void StopFollowingTarget()
    {
        _agent.stoppingDistance = 0f;
        _agent.updateRotation = true;

        _target = null;
    }
}
