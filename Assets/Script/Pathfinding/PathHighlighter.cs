using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathHighlighter : MonoBehaviour
{
    [TextArea]
    [SerializeField] string Notes = "Comment Here.";
    #region variables
    GridGenerator gridGenerator;
    GridTiles[,] grid;
    StepAssignement stepAssignement;
   /* [HideInInspector]*/public List<GridTiles> highlightedTiles; 
    #endregion

    private void Awake()
    {
        grid = GridGenerator.Instance.grid;
        stepAssignement = GetComponent<StepAssignement>();      
    }

    public void PathAssignment(int x, int y,int step)
    {
        int i = step;
        if (i>0) 
            TestFourDirection(x, y, i - 1);
        //grid[x, y].highlight = true;
    }

    void TestFourDirection(int x, int y, int step)
    {
        if (TestDirectionForMovement(x, y, 1, step))
        {
            if (grid[x, y+1])
            {
                grid[x, y].highlight = true;
                highlightedTiles.Add(grid[x, y]);
                PathAssignment(x, y+1,step);
            }
        }
        else if (TestDirectionForMovement(x, y, 2, step))
        {
            if (grid[x, y-1])
            {
                
                grid[x, y].highlight = true;
                highlightedTiles.Add(grid[x, y]);

                PathAssignment(x, y-1, step);
            }
        }           
        else if (TestDirectionForMovement(x, y, 3, step))
        {
            if (grid[x-1, y])
            {
                grid[x, y].highlight = true;
                highlightedTiles.Add(grid[x, y]);

                PathAssignment(x-1, y,step);
            }
        }            
        else if (TestDirectionForMovement(x, y, 4, step))
        {
            if (grid[x+1, y])
            {
                grid[x, y].highlight = true;
                highlightedTiles.Add(grid[x, y]);

                PathAssignment(x+1, y,step);
            }
        }           
    }



    public bool TestDirectionForMovement(int x, int y, int direction, int step)
    {
        if (grid[x, y] != null)
        {
            var columns = GridGenerator.Instance.columns;
            var rows = GridGenerator.Instance.rows;
            var stepHeight = GridGenerator.Instance.stepHeight;
            var dropHeight = GridGenerator.Instance.dropHeight;
            switch (direction)
            {
                //up
                case 1:
                    if (y + 1 < columns && grid[x, y + 1].step == step && grid[x, y] && grid[x, y + 1].transform.position.y - grid[x, y+1].transform.position.y <=  stepHeight && grid[x, y].transform.position.y - grid[x, y + 1].transform.position.y >= dropHeight && grid[x, y + 1].walkable)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                //down
                case 2:
                    if (y - 1 > -1 && grid[x, y - 1].step == step && grid[x, y - 1] && grid[x, y].transform.position.y - grid[x, y-1].transform.position.y <= stepHeight && grid[x, y].transform.position.y - grid[x, y - 1].transform.position.y >= dropHeight && grid[x, y - 1].walkable)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                //left
                case 3:
                    if (x - 1 > -1 && grid[x - 1, y].step == step && grid[x - 1, y] && grid[x, y].transform.position.y - grid[x-1, y].transform.position.y <= stepHeight && grid[x, y].transform.position.y - grid[x - 1, y].transform.position.y >= dropHeight && grid[x - 1, y].walkable)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                //right
                case 4:
                    if (x + 1 < rows && grid[x + 1, y].step == step && grid[x + 1, y] && grid[x, y].transform.position.y - grid[x+1, y].transform.position.y <= stepHeight && grid[x, y].transform.position.y - grid[x + 1, x].transform.position.y >= dropHeight && grid[x + 1, y].walkable)
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
