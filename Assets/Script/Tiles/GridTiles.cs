using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

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
        End_Tile,
        Plaque_De_Pression
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


    [HideInInspector] public float originalY;
    //public int tapisRoulantDirection;
    public Direction tapisRoulantDirection;
    public Vector2 teleporteurReceptorCoordinates;
    public Vector2 plaqueDePressionCoordinates;
    [Range(-2,2)] public int porteHeightChange;
    public Avatar avatar;
    public Material teleporterMat, tapisRoulantMat, glaceMat, loopMat, unwalkableMat, ogPosMatA, ogPosMatB, tileEndMatP1, tileEndMatP2, tilePlaqueDePressionMat, tilePorteMat;
    public Material[] walkableMats;
    public MeshRenderer tilemeshR, contourMeshR;
    UI_TimeLineManager timeLineManager;
    MovementEvents movementEvents;
    Transform Player;
    bool isRuntime = false;
    public bool gizmoFlag;
    //public int step;
    //public bool highlight;
    //public GameObject Highlight;
    private void OnDrawGizmos()
    {
        if (!isRuntime && gizmoFlag)
        {
            MaterialsInGizmo();
            MoveTileOnGizmo();
            gizmoFlag = false;
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
                ArcElectrique(playerTarget);
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
            case TileVariant.Plaque_De_Pression:
                PlaqueDePression(playerTarget);
                break;
        }
    }

    public float animSpeed;
    public AnimationCurve animCurve;
    IEnumerator elevateBloc(int startY, int endY, Transform porte, UI_Actions.PlayerTarget playerTarget)
    {
        float i = 0;
        while (i < 1)
        {
            i += Time.deltaTime * (1 / animSpeed);

            porte.position = new Vector3(porte.position.x, Mathf.Lerp(startY, endY, animCurve.Evaluate(i)), porte.position.z);
            yield return null;
        }
        porte.position = new Vector3(porte.position.x, endY, porte.position.z);

        yield return null;

        if (playerTarget == UI_Actions.PlayerTarget.Avatar_A)
            timeLineManager.playerAready = true;
        else if (playerTarget == UI_Actions.PlayerTarget.Avatar_B)
            timeLineManager.playerBready = true;
    }

    void PlaqueDePression(UI_Actions.PlayerTarget playerTarget)
    {
        RuntimeManager.PlayOneShot("event:/Elements/Pressure plate press");
        Transform tile = GridGenerator.Instance.grid[Mathf.RoundToInt(plaqueDePressionCoordinates.x), Mathf.RoundToInt(plaqueDePressionCoordinates.y)].transform;
        StartCoroutine(elevateBloc(Mathf.RoundToInt(tile.position.y), Mathf.RoundToInt(tile.position.y) + porteHeightChange, tile, playerTarget));
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

    void ArcElectrique(UI_Actions.PlayerTarget playerTarget)
    {
        if (playerTarget == UI_Actions.PlayerTarget.Avatar_A)
        {
            timeLineManager.playerAready = true;
            timeLineManager.stunnedA = true;
        }
        else if (playerTarget == UI_Actions.PlayerTarget.Avatar_B)
        {

            timeLineManager.playerBready = true;
            timeLineManager.stunnedB = true;
        }
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

        RuntimeManager.PlayOneShot("event:/Elements/Teleport");

    }

    void Boucle(UI_Actions.PlayerTarget playerTarget)
    {
        timeLineManager.currentIndex = -1;
        if(playerTarget == UI_Actions.PlayerTarget.Avatar_A)
            timeLineManager.playerAready = true;
        else if(playerTarget == UI_Actions.PlayerTarget.Avatar_B)
            timeLineManager.playerBready = true;
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

    public Transform objectFolder;

    public GameObject pressurePlate, arcElectrique, teleporteur;
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
                    SpawnItem("Teleporteur", teleporteur);

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
                case TileVariant.Plaque_De_Pression:
                    SpawnItem("plaque de pression", pressurePlate);

                    break;
                case TileVariant.Arc_Electrique:
                    SpawnItem("arcElectrique", arcElectrique);
                    break;
            }
            
        }
        else if (tilemeshR.sharedMaterial != unwalkableMat && !walkable)
        {
            tilemeshR.sharedMaterial = unwalkableMat;
        }
    }

    void SpawnItem(string childName, GameObject objectToInst)
    {
        if(objectFolder.childCount == 0)
        {
            GameObject inst = Instantiate(objectToInst, objectFolder);
            inst.name = childName;
        }
        else if(objectFolder.childCount == 1)
        {
            if (objectFolder.GetChild(0).name != childName)
            {
                DestroyImmediate(objectFolder.GetChild(0).gameObject);
                GameObject inst = Instantiate(objectToInst, objectFolder);
                inst.name = childName;
            }
        }
        else
        {
            print(objectFolder.childCount);
            Transform[] children = objectFolder.GetComponentsInChildren<Transform>();
            bool mybool = false;
            foreach(Transform child in children)
            {
                if(child.name != childName)
                {
                    DestroyImmediate(child.gameObject);
                }
                else
                {
                    mybool = true;
                }
            }
            if (!mybool)
            {
                GameObject inst = Instantiate(objectToInst, objectFolder);
                inst.name = childName;
            }
        }
    }
    private void Start()
    {
        originalY = transform.position.y;
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

        if(tileType == TileVariant.Plaque_De_Pression) 
        {
            GridGenerator.Instance.grid[Mathf.RoundToInt(plaqueDePressionCoordinates.x), Mathf.RoundToInt(plaqueDePressionCoordinates.y)].tilemeshR.sharedMaterial = tilePorteMat;
        }
/*        if(tileType == TileVariant.Glace)
        {
            float rotation = Random.Range(0, 3) * 90;
            tilemeshR.transform.rotation = Quaternion.Euler(0, rotation, 0);
        }*/
    }


    private void Update()
    {
        if (tileType == TileVariant.Plaque_De_Pression)
        {
            GridGenerator.Instance.grid[Mathf.RoundToInt(plaqueDePressionCoordinates.x), Mathf.RoundToInt(plaqueDePressionCoordinates.y)].tilemeshR.sharedMaterial = tilePorteMat;
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
