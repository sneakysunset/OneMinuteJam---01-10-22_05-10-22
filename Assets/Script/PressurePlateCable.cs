using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateCable : MonoBehaviour
{
    LineRenderer lineR;
    private void Awake()
    {
        lineR = GetComponent<LineRenderer>();
    }

    void Update()
    {
        lineR.SetPosition(2, transform.parent.position + new Vector3(0, 0.5f, 0.5f));
        lineR.SetPosition(3, transform.parent.position + 1 / 2 * Vector3.up);
    }
}
