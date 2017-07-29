using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform TargetToFollow;

    public float Pitch = 2f;
    public float ZoomSpeed = 4f;
    public float MinZoom = 5f;
    public float MaxZoom = 15f;

    public float YawSpeed = 100f;

    public Vector3 Offset;

    private float _currentZoom = 10f;
    private float _currentYaw = 0f;

    void Update()
    {
        _currentZoom -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        _currentZoom = Mathf.Clamp(_currentZoom, MinZoom, MaxZoom);

        _currentYaw -= Input.GetAxis("Horizontal") * YawSpeed * Time.deltaTime;
    }

    void LateUpdate()
    {

        transform.position = TargetToFollow.position - Offset * _currentZoom;
        transform.LookAt(TargetToFollow.position + Vector3.up * Pitch);

        transform.RotateAround(TargetToFollow.position, Vector3.up, _currentYaw);
    }
}
