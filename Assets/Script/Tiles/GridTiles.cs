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

    public enum TileVariant
    {
        Tile,
        Tapis_Roulant,
        Glace,
        Arc_Electrique,
        Teleporteur,
        Boucle
    }

    public TileVariant tileType;
    public bool walkable;
    public bool originalPos;
    public int tapisRoulantDirection;
    public Vector2 teleporteurReceptorCoordinates;
    public Avatar avatar;
    public Material walkableMat, unwalkableMat, ogPosMatA, ogPosMatB;
    public MeshRenderer tilemeshR, contourMeshR;
    UI_TimeLineManager timeLineManager;
    MovementEvents movementEvents;
    Transform Player;
    bool isRuntime = false;
    //public int step;
    //public bool highlight;
    //public GameObject Highlight;
    private void OnDrawGizmos()
    {
        if (!isRuntime)
        {
            MaterialsInGizmo();
            MoveTileOnGizmo();
        }
    }


    public void TileEffect(UI_Actions.PlayerTarget playerTarget)
    {
        timeLineManager = FindObjectOfType<UI_TimeLineManager>();
        movementEvents = FindObjectOfType<MovementEvents>();
        switch (tileType)
        {
            case TileVariant.Tile:
                timeLineManager.currentIndex++;
                timeLineManager.LaunchTimeline();
                break;
            case TileVariant.Tapis_Roulant:
                TapisRoulant(playerTarget);
                break;
            case TileVariant.Glace:
                Glace(playerTarget);
                break;
            case TileVariant.Arc_Electrique:
                ArcElectrique();
                break;
            case TileVariant.Teleporteur:
                Teleporteur(playerTarget);
                break;
            case TileVariant.Boucle:
                Boucle();
                break;
        }
    }

    void TapisRoulant(UI_Actions.PlayerTarget playerTarget)
    {
        movementEvents.TapisRoulantMovement(tapisRoulantDirection, playerTarget);
    }

    void Glace(UI_Actions.PlayerTarget playerTarget)
    {
        movementEvents.MovementActivation(1, playerTarget);
    }

    void ArcElectrique()
    {

    }

    void Teleporteur(UI_Actions.PlayerTarget playerTarget)
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Player.position = GridGenerator.Instance.grid[(int)teleporteurReceptorCoordinates.x, (int)teleporteurReceptorCoordinates.y].transform.position;
        GridGenerator.Instance.grid[(int)teleporteurReceptorCoordinates.x, (int)teleporteurReceptorCoordinates.y].TileEffect(playerTarget);
    }

    void Boucle()
    {
        timeLineManager.currentIndex = 0;
        timeLineManager.LaunchTimeline();
    }



    private void Update()
    {
/*        if (highlight)
        {
            Highlight.SetActive(true);
        }
        else
        {
            Highlight.SetActive(false);
        }*/
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
