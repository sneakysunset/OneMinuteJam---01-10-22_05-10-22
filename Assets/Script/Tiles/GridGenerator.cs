using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Transform tileFolder;
    public static GridGenerator Instance { get; private set; }
    [HideInInspector] public bool inAnim;
    [Header("Components References")]


    public PathHighlighter highlighter;
    public PathFindingMovement pathFM;
    [Header("Input Values")]
    
    [SerializeField] public int rows;
    [SerializeField] public int columns;
    [HideInInspector] public Vector3 ogPos;
    public int stepHeight;
    public int dropHeight;
    //public float maxDepth = 50f;

    void GetReferences()
    {
        highlighter = GetComponent<PathHighlighter>();
    }
  
    #endregion
    void Awake()
    {
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
            int y = (int)list[i].transform.position.z / (int)list[i].transform.localScale.y;
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

                //check which avatar to spawn
                switch (obj.avatar)
                {
                    case GridTiles.Avatar.Avatar_A:
                        player_A.position = ogPos;
                        break;

                    case GridTiles.Avatar.Avatar_B:
                        player_B.position = ogPos;
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
                int y = (int)list[i].transform.position.z / (int)list[i].transform.localScale.y;
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
                int y = (int)list[i].transform.position.z / (int)list[i].transform.localScale.y;
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

    public bool PFTestDirectionForMovement(int x, int y, int direction, int step)
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
        
    }

    public bool TestDirectionForMovement(int x, int y, int direction)
    {
        if (grid[x, y] != null)
        {
            if (inAnim)
                return false;
            switch (direction)
            {
                //up
                case 1:
                    if (y + 1 < columns && grid[x, y + 1] && grid[x, y + 1].transform.position.y - grid[x, y].transform.position.y <= stepHeight && grid[x, y + 1].transform.position.y - grid[x, y].transform.position.y >= dropHeight && grid[x, y + 1].walkable)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }



                //down
                case 2:
                    if (y - 1 > -1 && grid[x, y - 1] && grid[x, y - 1].transform.position.y - grid[x, y].transform.position.y <= stepHeight && grid[x, y - 1].transform.position.y - grid[x, y].transform.position.y >= dropHeight && grid[x, y - 1].walkable)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                //left
                case 3:
                    if (x - 1 > -1 && grid[x - 1, y] && grid[x - 1, y].transform.position.y - grid[x, y].transform.position.y <= stepHeight && grid[x - 1, y].transform.position.y - grid[x, y].transform.position.y >= dropHeight && grid[x - 1, y].walkable)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                //right
                case 4:
                    if (x + 1 < rows && grid[x + 1, y] && grid[x + 1, y].transform.position.y - grid[x, y].transform.position.y <= stepHeight && grid[x + 1, y].transform.position.y - grid[x, y].transform.position.y >= dropHeight && grid[x + 1, y].walkable)
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

    }

}

