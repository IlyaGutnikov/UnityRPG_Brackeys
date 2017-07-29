using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public LayerMask MovementMask;

    [SerializeField]
    private float rangeOfHit = 100f;

    private Camera _cam;
    private PlayerMotor _motor;

	// Use this for initialization
	void Start () {
		_cam = Camera.main;
	    _motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetMouseButtonDown(0))
	    {
	        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
	        RaycastHit hit;

	        if (Physics.Raycast(ray, out hit, rangeOfHit, MovementMask))
	        {
                _motor.MoveToPoint(hit.point);

                //Stop focus
	        }

	    }
	}
}
