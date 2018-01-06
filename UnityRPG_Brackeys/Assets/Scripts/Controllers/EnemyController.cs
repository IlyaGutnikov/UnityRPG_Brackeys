using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float LookRadius = 10f;

    private Transform target;
    private NavMeshAgent _agent;

	// Use this for initialization
	void Start ()
	{
	    target = PlayerManager.Instance.Player.transform;
	    _agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    float distance = Vector3.Distance(target.position, transform.position);

	    if (distance <= LookRadius)
	    {
	        _agent.SetDestination(target.position);

	        if (distance <= _agent.stoppingDistance)
	        {
                //Attack and face
	            FaceTarget();
	        }
	    }
	}

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, LookRadius);
    }
#endif
}
