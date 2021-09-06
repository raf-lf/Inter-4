using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float cameraSpeed;

    private void Start()
    {
        if (target == null) target = FindObjectOfType<PlayerAtributes>().gameObject.transform;
    }

    private void Update()
    {
        if (target != null) transform.position = Vector3.Lerp(transform.position, target.position + offset, cameraSpeed);
    }
}
