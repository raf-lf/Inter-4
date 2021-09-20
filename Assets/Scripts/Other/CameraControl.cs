using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    private Vector3 offsetModifier;
    public float cameraSpeed;

    private void OnEnable()
    {
        GameManager.scriptCamera = this;    
    }

    public void ChangeCameraOffset(Vector3 newOffset)
        =>
        offsetModifier = newOffset;
    public void ResetCameraOffset()
        =>
        offsetModifier = Vector3.zero;

    private void Start()
    {
        if (target == null) target = FindObjectOfType<PlayerAtributes>().gameObject.transform;
    }

    private void Update()
    {
        if (target != null) transform.position = Vector3.Lerp(transform.position, target.position + offset + offsetModifier, cameraSpeed);
    }
}
