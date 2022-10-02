using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorManager : MonoBehaviour
{
    public bool SetDecor;

    private void OnDrawGizmos()
    {
        if (SetDecor)
        {
            foreach(Decor item in GetComponentsInChildren<Decor>())
            {
                item.SetMat();
            }
            SetDecor = false;
        }
    }
}
