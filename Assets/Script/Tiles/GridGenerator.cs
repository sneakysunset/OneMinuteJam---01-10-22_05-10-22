using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if  UNITY_EDITOR
using UnityEditor;
#endif
public class GridGenerator : MonoBehaviour
{
    #region variables
    public GridTiles[,] grid;
    [SerializeField] public bool instantiateGrid = false;
    public GameObject Tile;
    public Transform player_A;
    public Transform player_B;
    [HideInInspector] public Vector3 playerOGPosA, playerOGPosB; 
    [HideInInspector] public Quaternion playerOGRotA, playerOGRotB; 
    public Transform tileFolder;
    public UI_TimeLineManager timeLineManager;
    public static GridGenerator Instance { get; private set; }
    [HideInInspector] public bool inAnim;
    [Header("Components References")]

    [Header("Input Values")]
    
    [SerializeField] public int rows;
    [SerializeField] public int columns;
    [HideInInspector] public Vector3 ogPos;
    public int stepHeight;
    public int dropHeight;
    //public float maxDepth = 50f;

    void GetReferences()
    {
        timeLineManager = FindObjectOfType<UI_TimeLineManager>();
    }
  
    #endregion
    void Awake()
    {
        timeLineManager = FindObjectOfType<UI_TimeLineManager>();
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        GetReferences();

        instantiateGrid = false;

        GridTiles[] list = FindObjectsOfType<GridTiles>();

        grid = new GridTiles[rows, columns];

        for (int i = 0; i < list.Length; i++)
        {
            int x = (int)list[i].transform.position.x / (int)list[i].transform.localScale.x;
            int y = (int)list[i].transform.position.z / (int)list[i].transform.localScale.z;
            grid[x, y] = list[i];
            grid[x, y].name = "tiles " + x + " "+ y;
        }
        
    }

    private void Start()
    {
        foreach (GridTiles obj in grid)
        {
            if (obj.originalPos)
            {
                ogPos = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);

                //check which playerTarget to spawn
                switch (obj.avatar)
                {
                    case GridTiles.Avatar.Avatar_A:
                        player_A.position = ogPos;
                        playerOGPosA = player_A.position;
                        playerOGRotA = player_A.rotation;
                        break;

                    case GridTiles.Avatar.Avatar_B:
                        player_B.position = ogPos;
                        playerOGPosB = player_B.position;
                        playerOGRotB = player_B.rotation;
                        break;
                }
            }
        }
    }

    public void generateGrid()
    {
        GridTiles[] list = FindObjectsOfType<GridTiles>();
        if (list.Length != 0)
        {
            grid = new GridTiles[rows, columns];
            for (int i = 0; i < list.Length; i++)
            {
                int x = (int)list[i].transform.position.x / (int)list[i].transform.localScale.x;
                int y = (int)list[i].transform.position.z / (int)list[i].transform.localScale.z;
                grid[x, y] = list[i];
                grid[x, y].name = "tiles " + x + " " + y;
            }
        }
    }

    private void OnDrawGizmos()
    {

        if (instantiateGrid)
        {
            GridTiles[] list = FindObjectsOfType<GridTiles>();
            grid = new GridTiles[rows, columns];
            for (int i = 0; i < list.Length; i++)
            {
                int x = (int)list[i].transform.position.x / (int)list[i].transform.localScale.x;
                int y = (int)list[i].transform.position.z / (int)list[i].transform.localScale.z;
                grid[x, y] = list[i];
                grid[x, y].name = "tiles " + x + " " + y;
            }



            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    if (!grid[x, y])
                    {
#if UNITY_EDITOR
                        Selection.activeObject = PrefabUtility.InstantiatePrefab(Tile);
                       // Selection.activeObject = PrefabUtility.InstantiatePrefab(Tile);
                        var inst = Selection.activeObject as GameObject;
#endif

#if !UNITY_EDITOR
                        var inst = Instantiate(Tile);
#endif
                        //Debug.Log(1);
                        inst.transform.parent = tileFolder;
                        inst.transform.position = new Vector3(x, 0, y);
                       // inst.transform.Find("Renderer").Rotate(90 * Random.Range(0, 5), 90 * Random.Range(0, 5), 90 * Random.Range(0, 5));
                        grid[x, y] = inst.GetComponent<GridTiles>();
                        grid[x, y].name = "tiles " + x + " " + y;
                    }
                }
            }

            instantiateGrid = false;

        }

    }

    /* public bool PFTestDirectionForMovement(int x, int y, int direction, int step)
     {
         if (grid[x,y] != null)
         {
             if (inAnim)
                 return false;
             switch (direction)
             {
                 //up
                 case 1:
                     if (y + 1 < columns && grid[x, y + 1].step == -1 && grid[x, y + 1] && grid[x, y + 1].transform.position.y - grid[x, y].transform.position.y <= stepHeight && grid[x, y + 1].transform.position.y - grid[x, y].transform.position.y >= dropHeight && grid[x, y + 1].walkable)
                     {
                         return true;
                     }
                     else
                     {
                         return false;
                     }



                 //down
                 case 2:
                     if (y - 1 > -1 && grid[x, y - 1].step == -1 && grid[x, y - 1] && grid[x, y - 1].transform.position.y - grid[x, y].transform.position.y <= stepHeight && grid[x, y - 1].transform.position.y - grid[x, y].transform.position.y >= dropHeight && grid[x, y - 1].walkable)
                     {
                         return true;
                     }
                     else
                     {
                         return false;
                     }

                 //left
                 case 3:
                     if (x - 1 > -1 && grid[x-1, y].step == -1 && grid[x - 1, y] && grid[x - 1, y].transform.position.y - grid[x, y].transform.position.y <= stepHeight && grid[x-1, y].transform.position.y - grid[x, y].transform.position.y >= dropHeight && grid[x - 1, y].walkable)
                     {
                         return true;
                     }
                     else
                     {
                         return false;
                     }

                 //right
                 case 4:
                     if (x + 1 < rows && grid[x+1, y].step == -1 && grid[x + 1, y] && grid[x + 1, y].transform.position.y - grid[x, y].transform.position.y <= stepHeight && grid[x + 1, y].transform.position.y - grid[x, y].transform.position.y >= dropHeight && grid[x + 1, y].walkable)
                     {
                         return true;
                     }
                     else
                     {
                         return false;
                     }

                 default:
                     return false;
             }
         }

             return false;

     }*/


    public UnityEvent<Vector3, Transform, UI_Actions.PlayerTarget, bool> cancelledMoveEvent;

    public bool TestDirectionForMovement(int x, int y, int newX, int newY,  UI_Actions.PlayerTarget playerTarget, Vector3 startPos, Transform endTarget)
    {
        bool ready = false;
        bool end = false;
        if(playerTarget == UI_Actions.PlayerTarget.Avatar_A)
        {
            end = timeLineManager.endA;
            ready = timeLineManager.playerAready;
        }
        else if(playerTarget == UI_Actions.PlayerTarget.Avatar_B)
        {
            end = timeLineManager.endB;
            ready = timeLineManager.playerBready;
        }
        if (grid[x, y] != null)
        {
            if (inAnim)
                return false;

            if (newX < rows && newX > -1 && newY < columns && newY > -1 && grid[newX, newY] && grid[newX, newY].transform.position.y - grid[x, y].transform.position.y <= 0 && grid[newX, newY].transform.position.y - grid[x, y].transform.position.y >= dropHeight && grid[newX, newY].walkable && !end /*&& grid[newX, newY].transform.position != otherPlayer.position*/)
            {

                if (playerTarget == UI_Actions.PlayerTarget.Avatar_A)
                {
                    if (timeLineManager.stunnedA)
                    {
                        timeLineManager.stunnedB = false;

                        timeLineManager.playerAready = true;
                        return false;
                    }
                }
                else if (playerTarget == UI_Actions.PlayerTarget.Avatar_B)
                {
                    if (timeLineManager.stunnedB)
                    {
                        timeLineManager.stunnedB = false;
                        timeLineManager.playerBready = true;
                        return false;
                    }
                }

                return true;
            }
            else
            {
                if (playerTarget == UI_Actions.PlayerTarget.Avatar_A)
                {
                    timeLineManager.playerAready = true;
                }
                else
                {
                    timeLineManager.playerBready = true;
                }
                cancelledMoveEvent.Invoke(startPos, endTarget, playerTarget, false);
                return false;
            }
        }
        cancelledMoveEvent.Invoke(startPos, endTarget, playerTarget, false);
        return false;

    }

}

