using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTiles : MonoBehaviour
{
    public bool walkable;
    public bool originalPos;
    public Material walkableMat, unwalkableMat;
    public MeshRenderer meshR;

    private void OnDrawGizmos()
    {
        if (walkable)
        {
            meshR.material = walkableMat;
        }
        else
        {
            meshR.material = unwalkableMat;
        }
    }
}
