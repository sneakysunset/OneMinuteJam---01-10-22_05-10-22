using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTiles : MonoBehaviour
{
    public bool walkable;
    public bool originalPos;
    public Material walkableMat, unwalkableMat, ogPosMat;
    public MeshRenderer tilemeshR, contourMeshR;
    bool isRuntime = false;

    private void OnDrawGizmos()
    {
        if (!isRuntime)
        {
            MaterialsInGizmo();
        }
    }

    void MaterialsInGizmo()
    {
        if (originalPos && tilemeshR.sharedMaterial != ogPosMat)
        {
            walkable = true;
            tilemeshR.material = ogPosMat;
        }

        if (walkable && !originalPos && (tilemeshR.sharedMaterial != ogPosMat || tilemeshR.sharedMaterial != walkableMat))
        {
            tilemeshR.sharedMaterial = walkableMat;
        }
        else if (tilemeshR.sharedMaterial != unwalkableMat && !walkable)
        {
            tilemeshR.sharedMaterial = unwalkableMat;
        }
    }

    private void Start()
    {
        isRuntime = true;
        if (!walkable)
        {
            tilemeshR.enabled = false;
            contourMeshR.enabled = false;
        }
        if (originalPos)
        {
            tilemeshR.material = walkableMat;
        }
    }
}
