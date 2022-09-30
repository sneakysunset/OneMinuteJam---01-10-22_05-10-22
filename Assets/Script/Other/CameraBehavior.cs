using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform target;
    public float damp;
    private Vector3 velocity = Vector3.zero;
    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, damp);
    }
}
