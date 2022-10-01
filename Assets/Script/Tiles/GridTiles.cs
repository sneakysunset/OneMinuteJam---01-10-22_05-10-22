using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTiles : MonoBehaviour
{
    //enum to choose between playerTarget A and B
    public enum Avatar
    {
        Avatar_A,
        Avatar_B
    }

    public bool walkable;
    public bool originalPos;
    public Avatar avatar;
    public Material walkableMat, unwalkableMat, ogPosMatA, ogPosMatB;
    public MeshRenderer tilemeshR, contourMeshR;
    bool isRuntime = false;
    public int step;
    public bool highlight;
    public GameObject Highlight;
    private void OnDrawGizmos()
    {
        if (!isRuntime)
        {
            MaterialsInGizmo();
            MoveTileOnGizmo();
        }
    }

    private void Update()
    {
        if (highlight)
        {
            Highlight.SetActive(true);
        }
        else
        {
            Highlight.SetActive(false);
        }
    }

    void MoveTileOnGizmo()
    {
        if(transform.position.x != (int)transform.position.x)
        {
            transform.position = new Vector3((int)transform.position.x, transform.position.y, transform.position.z);
        }
        if (transform.position.y != (int)transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, (int)transform.position.y, transform.position.z);
        }
        if (transform.position.z != (int)transform.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, (int)transform.position.z);
        }
    }


    void MaterialsInGizmo()
    {
        if (originalPos && tilemeshR.sharedMaterial != ogPosMatA)
        {
            walkable = true;
            //change color depending on playerTarget
            switch (avatar)
            {
                case Avatar.Avatar_A:
                    tilemeshR.sharedMaterial = ogPosMatA;
                    break;
                case Avatar.Avatar_B:
                    tilemeshR.sharedMaterial = ogPosMatB;
                    break;
            }
        }

        if (walkable && !originalPos && (tilemeshR.sharedMaterial != ogPosMatA || tilemeshR.sharedMaterial != walkableMat))
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

    private void OnMouseOver()
    {
        //if (walkable && !GridGenerator.Instance.inAnim)
        //{
        //    var hl = GridGenerator.Instance.highlighter;
        //    foreach (GridTiles tile in hl.highlightedTiles)
        //    {
        //        tile.highlight = false;
        //    }
        //    hl.highlightedTiles.Clear();
        //    hl.PathAssignment((int)transform.position.x,(int)transform.position.z, step);
        //}
    }

    private void OnMouseDown()
    {
        //if (walkable && !GridGenerator.Instance.inAnim)
        //{
        //    GridGenerator.Instance.pathFM.Move();
        //}
    }
}
