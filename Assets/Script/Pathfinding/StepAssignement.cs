using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepAssignement : MonoBehaviour
{
    [TextArea]
    [SerializeField] string Notes = "Comment Here.";
    #region variables
    [HideInInspector] public int startPosX, startPosY, row, columns;
    GridTiles[,] grid;
    [SerializeField] Transform player;
    Transform playerT;
    #endregion

    private void Awake()
    {
        player = GridGenerator.Instance.playerT;       
        grid = GridGenerator.Instance.grid;
        row = GridGenerator.Instance.rows;
        columns = GridGenerator.Instance.columns;
    }
    private void Start()
    {

        foreach (GridTiles obj in grid)
        {
            if (obj.originalPos)
            {
                var ogPos = new Vector3(obj.transform.position.x, player.position.y, obj.transform.position.z);
                player.position = ogPos;
            }
        }
        Initialisation();
    }

    public void Initialisation()
    {
        startPosX = (int)player.position.x;
        startPosY = (int)player.position.z;
        foreach (GridTiles obj in grid)
        {
            obj.step = -1;
            if (!obj.walkable)
            {
                obj.step = -2;
            }
        }
        grid[startPosX, startPosY].step = 0;
        AssignationChecker();
    }

    void AssignationChecker()
    {
                for(int i = 1; i< row*columns; i++) 
                {
                    foreach(GridTiles obj in grid)
                    {
                        if(obj.step == i-1)
                        {
                            TestFourDirection((int)obj.transform.position.x, (int)obj.transform.position.z, obj.step);                  
                        }
                    }
                }
    }

    void TestFourDirection(int x, int y, int step)
    {
        if(GridGenerator.Instance.PFTestDirectionForMovement(x, y, 1, step))       
            SetVisited(x, y+1, step);

        if (GridGenerator.Instance.PFTestDirectionForMovement(x, y, 2, step))
            SetVisited(x, y-1, step);  
        
        if(GridGenerator.Instance.PFTestDirectionForMovement(x, y, 3, step))        
            SetVisited(x-1, y, step);
        
        if(GridGenerator.Instance.PFTestDirectionForMovement(x, y, 4, step))        
            SetVisited(x+1, y, step);       
    }

/*
    public bool TestDirection(int x, int y,int height, int step, int direction)
    {
        switch (direction)
        {
            #region case 1
            case 1:
                if(y+1<columns && grid[x,y+1] && grid[x,y+1].step == step && grid[x,y+1].height <= grid[x,y].height+1 && grid[x, y + 1].height >= grid[x, y].height - 1 && grid[x, y + 1].walkable)
                {
                    return true;
                }
                else
                {
                    if (y + 1 < columns)
                    {

                        if(grid[x, y + 1].door != 0)
                        {
                            foreach(string obj in playerS.Inventory)
                            {
                                    if (obj == "key" + grid[x, y + 1].door)
                                    {
                                          grid[x, y + 1].door = 0;
                                          grid[x, y + 1].walkable = true;
                                          return true;
                                    }
                            }
                        
                        }
                    }
                    
                    return false;
                }
            #endregion
            #region case 2
            case 2:
                if(x+1<row && grid[x+1,y] && grid[x+1,y].step == step && grid[x+1, y].height <= grid[x, y].height + 1 && grid[x+1, y].height >= grid[x, y].height - 1 && grid[x+1, y].walkable)
                {
                    return true;
                }
                else
                {
                    if(x + 1 < row)
                    {
                        if (grid[x + 1, y].door != 0)
                        {
                            foreach (string obj in playerS.Inventory)
                            {
                                if (obj == "key" + grid[x + 1, y].door)
                                {
                                    grid[x + 1, y].door = 0;
                                    grid[x + 1, y].walkable = true;
                                    return true;
                                }
                            }

                        }
                    }
                
                    return false;
                }
            #endregion
            #region case 3
            case 3:
                if(y-1>-1 && grid[x,y-1] && grid[x,y-1].step == step && grid[x, y - 1].height <= grid[x, y].height + 1 && grid[x, y - 1].height >= grid[x, y].height - 1 && grid[x, y - 1].walkable)
                {
                    return true;
                }
                else
                {
                    if (y - 1 > -1)
                    {

                    if (grid[x, y - 1].door != 0)
                    {
                        foreach (string obj in playerS.Inventory)
                        {
                            if (obj == "key" + grid[x, y - 1].door)
                            {
                                grid[x, y - 1].door = 0;
                                grid[x, y - 1].walkable = true;
                                return true;
                            }
                        }

                    }
                    }
                    return false;
                }
            #endregion
            #region case 4
            case 4:
                if(x-1>-1 && grid[x-1,y] && grid[x-1,y].step == step && grid[x-1, y].height <= grid[x, y].height + 1 && grid[x-1, y].height >= grid[x, y].height - 1 && grid[x-1, y].walkable)
                {
                    return true;
                }
                else
                {
                    if (x - 1 > -1)
                    {

                    if (grid[x-1, y].door != 0)
                    {
                        foreach (string obj in playerS.Inventory)
                        {
                            if (obj == "key" + grid[x-1, y].door)
                            {
                                grid[x-1, y].door = 0;
                                grid[x-1, y].walkable = true;
                                return true;
                            }
                        }

                    }
                    }
                    return false;
                }
    #endregion
        }
        return false;
    }
*/
    void SetVisited(int x, int y, int step)
    {
        if(grid[x, y])
        {
            grid[x,y].step = step + 1;
        }


    }
}