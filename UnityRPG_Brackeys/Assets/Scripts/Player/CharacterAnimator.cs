using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterAnimator : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;

    private float _speed;

    private const float Smooth = .1f;

	// Use this for initialization
	void Start ()
	{
	    _agent = GetComponent<NavMeshAgent>();
	    _animator = GetComponentInChildren<Animator>();

	    if (_animator == null)
	    {
            Debug.LogError("Animator is required");
	    }
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _speed = _agent.velocity.magnitude / _agent.speed;
        _animator.SetFloat("speedPercentage", _speed, Smooth, Time.deltaTime);
	}
}
