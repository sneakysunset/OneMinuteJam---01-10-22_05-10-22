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
        Boucle,
        End_Tile
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public TileVariant tileType;
    public bool walkable;
    public bool originalPos;
    //public int tapisRoulantDirection;
    public Direction tapisRoulantDirection;
    public Vector2 teleporteurReceptorCoordinates;
    public Avatar avatar;
    public Material teleporterMat, tapisRoulantMat, glaceMat, loopMat, unwalkableMat, ogPosMatA, ogPosMatB, tileEndMatP1, tileEndMatP2;
    public Material[] walkableMats;
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


    public void TileEffect(UI_Actions.PlayerTarget playerTarget, Vector3 previousPos)
    {
        timeLineManager = FindObjectOfType<UI_TimeLineManager>();
        movementEvents = FindObjectOfType<MovementEvents>();
        switch (tileType)
        {
            case TileVariant.Tile:
                if (playerTarget == UI_Actions.PlayerTarget.Avatar_A)
                    timeLineManager.playerAready = true;
                else if (playerTarget == UI_Actions.PlayerTarget.Avatar_B)
                    timeLineManager.playerBready = true;
                break;
            case TileVariant.Tapis_Roulant:
                TapisRoulant(playerTarget, previousPos);
                break;
            case TileVariant.Glace:
                Glace(playerTarget, previousPos);
                break;
            case TileVariant.Arc_Electrique:
                ArcElectrique();
                break;
            case TileVariant.Teleporteur:
                Teleporteur(playerTarget);
                break;
            case TileVariant.Boucle:
                Boucle(playerTarget);
                break;
            case TileVariant.End_Tile:
                End_Tile(playerTarget);
                break;
        }
    }
    
    void End_Tile(UI_Actions.PlayerTarget playerTarget)
    {
        if (playerTarget == UI_Actions.PlayerTarget.Avatar_A && avatar == Avatar.Avatar_A)
            timeLineManager.endA = true;
        else if (playerTarget == UI_Actions.PlayerTarget.Avatar_B && avatar == Avatar.Avatar_B)
            timeLineManager.endB = true;

        if (playerTarget == UI_Actions.PlayerTarget.Avatar_A)
            timeLineManager.playerAready = true;
        else if (playerTarget == UI_Actions.PlayerTarget.Avatar_B)
            timeLineManager.playerBready = true;
    }

    void TapisRoulant(UI_Actions.PlayerTarget playerTarget, Vector3 previousPos)
    {
        switch (tapisRoulantDirection)
        {
            case Direction.Up:
                movementEvents.TapisRoulantMovement(1, playerTarget, previousPos);
                break;
            case Direction.Down:
                movementEvents.TapisRoulantMovement(2, playerTarget, previousPos);
                break;
            case Direction.Left:
                movementEvents.TapisRoulantMovement(3, playerTarget, previousPos);
                break;
            case Direction.Right:
                movementEvents.TapisRoulantMovement(4, playerTarget, previousPos);
                break;

        }
    }

    void Glace(UI_Actions.PlayerTarget playerTarget, Vector3 previousPos)
    {
        movementEvents.GlaceMovement( playerTarget, previousPos);
    }

    void ArcElectrique()
    {

    }

    void Teleporteur(UI_Actions.PlayerTarget playerTarget)
    {
        if (playerTarget == UI_Actions.PlayerTarget.Avatar_A)
            Player = GridGenerator.Instance.player_A;
        else
            Player = GridGenerator.Instance.player_B;
        Player.position = GridGenerator.Instance.grid[(int)teleporteurReceptorCoordinates.x, (int)teleporteurReceptorCoordinates.y].transform.position;
        //GridGenerator.Instance.grid[(int)teleporteurReceptorCoordinates.x, (int)teleporteurReceptorCoordinates.y].TileEffect(playerTarget);
        if (playerTarget == UI_Actions.PlayerTarget.Avatar_A)
            timeLineManager.playerAready = true;
        else if (playerTarget == UI_Actions.PlayerTarget.Avatar_B)
            timeLineManager.playerBready = true;
    }

    void Boucle(UI_Actions.PlayerTarget playerTarget)
    {
        timeLineManager.currentIndex = -1;
        if(playerTarget == UI_Actions.PlayerTarget.Avatar_A)
            timeLineManager.playerAready = true;
        else if(playerTarget == UI_Actions.PlayerTarget.Avatar_B)
            timeLineManager.playerBready = true;
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

        if (walkable && !originalPos && (tilemeshR.sharedMaterial != ogPosMatA || tilemeshR.sharedMaterial != walkableMats[0]))
        {
            switch (tileType)
            {
                case TileVariant.Tile:
                    tilemeshR.sharedMaterial = walkableMats[0];
                    break;
                case TileVariant.Tapis_Roulant:
                    tilemeshR.sharedMaterial = tapisRoulantMat;
                    switch (tapisRoulantDirection)
                    {
                        case Direction.Up:
                            tilemeshR.transform.rotation = Quaternion.Euler(0,90,0);
                            break;
                        case Direction.Down:
                            tilemeshR.transform.rotation = Quaternion.Euler(0, -90, 0);
                            break;                        
                        case Direction.Left:
                            tilemeshR.transform.rotation = Quaternion.Euler(0, 0, 0);
                            break;                        
                        case Direction.Right:
                            tilemeshR.transform.rotation = Quaternion.Euler(0, 180, 0);
                            break;

                    }
                    break;
                case TileVariant.Glace:
                    tilemeshR.sharedMaterial = glaceMat;
                    break;
                case TileVariant.Teleporteur:
                    tilemeshR.sharedMaterial = teleporterMat;
                    break;
                case TileVariant.Boucle:
                    tilemeshR.sharedMaterial = loopMat;
                    break;
                case TileVariant.End_Tile:
                    switch (avatar)
                    {
                        case Avatar.Avatar_A:
                            tilemeshR.sharedMaterial = tileEndMatP1;
                            break; 
                        case Avatar.Avatar_B:
                            tilemeshR.sharedMaterial = tileEndMatP2;
                            break;
                    }
                    break;
            }
            
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
            int rdMatIndex = Random.Range(0, walkableMats.Length - 1);
            tilemeshR.material = walkableMats[rdMatIndex];
        }

        if(walkable && tileType == TileVariant.Tile)
        {
            int rdMatIndex = Random.Range(0, walkableMats.Length - 1);
            tilemeshR.material = walkableMats[rdMatIndex];
        }

/*        if(tileType == TileVariant.Glace)
        {
            float rotation = Random.Range(0, 3) * 90;
            tilemeshR.transform.rotation = Quaternion.Euler(0, rotation, 0);
        }*/
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
