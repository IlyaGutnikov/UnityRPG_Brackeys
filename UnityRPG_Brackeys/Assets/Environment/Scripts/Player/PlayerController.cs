using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    private Interactable _focus;

    public LayerMask MovementMask;

    [SerializeField]
    private float rangeOfHit = 100f;

    private Camera _cam;
    private PlayerMotor _motor;

    private const int LeftMouseButton = 0;
    private const int RightMouseButton = 1;

	// Use this for initialization
	void Start () {
		_cam = Camera.main;
	    _motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    WalkToPoint();

	    InteractWithObject();
	}

    /// <summary>
    /// Interact with object (right click)
    /// </summary>
    private void InteractWithObject()
    {
        if (Input.GetMouseButtonDown(RightMouseButton))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rangeOfHit))
            {
                var interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }

    /// <summary>
    /// Walk to the point (left click)
    /// </summary>
    private void WalkToPoint()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rangeOfHit, MovementMask))
            {
                _motor.MoveToPoint(hit.point);

                RemoveFocus();
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != _focus)
        {
            if (_focus != null)
                _focus.OnDefocused();

            _focus = newFocus;
            _motor.FollowTarget(_focus);
        }

        _focus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (_focus != null)
            _focus.OnDefocused();

        _focus = null;
        _motor.StopFollowingTarget();
    }
}
