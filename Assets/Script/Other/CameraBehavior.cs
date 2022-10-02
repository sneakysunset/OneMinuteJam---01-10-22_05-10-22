using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    //public Transform target;
    public float damp;
    private Vector3 velocity = Vector3.zero;


    private void Start()
    {
        float xPos = (float)(GridGenerator.Instance.rows - 1) / 2f;
        float yPos = ((float)(GridGenerator.Instance.rows - 1) + (float)(GridGenerator.Instance.rows - 1));
        float zPos = (float)(GridGenerator.Instance.columns - 1) / 2f;
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
    }

}
