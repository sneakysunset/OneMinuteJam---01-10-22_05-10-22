using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decor : MonoBehaviour
{


    public Material decor0, decor1, decor2A, decor2O, decor3, decor4;

    private void Start()
    {
        SetMat();
    }
    public void SetMat()
    {
        GridTiles[,] grid = GridGenerator.Instance.grid;
        int rows = GridGenerator.Instance.rows;
        int columns = GridGenerator.Instance.columns;
        int x = Mathf.RoundToInt(transform.position.x);
        int z = Mathf.RoundToInt(transform.position.z);
        MeshRenderer meshR = GetComponent<MeshRenderer>();

        if (x >= 0 && x < rows && z >= 0 && z< columns)
        {
            if(grid[x, z].walkable)
                gameObject.SetActive(false);

        }

        #region 4Dir
        if (testDir(x, z + 1, grid, rows, columns) && 
            testDir(x, z - 1, grid, rows, columns) && 
            testDir(x - 1, z, grid, rows, columns) && 
            testDir(x + 1, z, grid, rows, columns))
        {
            gameObject.SetActive(false);
            //meshR.material = decor4;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        #endregion

        #region 3Dir
        else if (!testDir(x, z + 1, grid, rows, columns) && 
                 testDir(x, z - 1, grid, rows, columns) && 
                 testDir(x - 1, z, grid, rows, columns) &&
                 testDir(x + 1, z, grid, rows, columns))
        {
            meshR.material = decor3;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (testDir(x, z + 1, grid, rows, columns) &&
                 !testDir(x, z - 1, grid, rows, columns) &&
                 testDir(x - 1, z, grid, rows, columns) &&
                 testDir(x + 1, z, grid, rows, columns))
        {
            meshR.material = decor3;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (testDir(x, z + 1, grid, rows, columns) &&
                 testDir(x, z - 1, grid, rows, columns) &&
                 !testDir(x - 1, z, grid, rows, columns) &&
                 testDir(x + 1, z, grid, rows, columns))
        {
            meshR.material = decor3;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (testDir(x, z + 1, grid, rows, columns) &&
                 testDir(x, z - 1, grid, rows, columns) &&
                 testDir(x - 1, z, grid, rows, columns) &&
                 !testDir(x + 1, z, grid, rows, columns))
        {
            meshR.material = decor3;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        #endregion

        #region 2DirO
        else if (!testDir(x, z + 1, grid, rows, columns) &&
                 !testDir(x, z - 1, grid, rows, columns) &&
                 testDir(x - 1, z, grid, rows, columns) &&
                 testDir(x + 1, z, grid, rows, columns))
        {
            meshR.material = decor2O;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (testDir(x, z + 1, grid, rows, columns) &&
                 testDir(x, z - 1, grid, rows, columns) &&
                 !testDir(x - 1, z, grid, rows, columns) &&
                 !testDir(x + 1, z, grid, rows, columns))
        {
            meshR.material = decor2O;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        #endregion

        #region 2DirA
        else if (!testDir(x, z + 1, grid, rows, columns) &&
                 testDir(x, z - 1, grid, rows, columns) &&
                 !testDir(x - 1, z, grid, rows, columns) &&
                 testDir(x + 1, z, grid, rows, columns))
        {

            meshR.material = decor2A;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (!testDir(x, z + 1, grid, rows, columns) &&
                 testDir(x, z - 1, grid, rows, columns) &&
                 testDir(x - 1, z, grid, rows, columns) &&
                 !testDir(x + 1, z, grid, rows, columns))
        {

            meshR.material = decor2A;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (testDir(x, z + 1, grid, rows, columns) &&
                 !testDir(x, z - 1, grid, rows, columns) &&
                 !testDir(x - 1, z, grid, rows, columns) &&
                 testDir(x + 1, z, grid, rows, columns))
        {

            meshR.material = decor2A;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (testDir(x, z + 1, grid, rows, columns) &&
                 !testDir(x, z - 1, grid, rows, columns) &&
                 testDir(x - 1, z, grid, rows, columns) &&
                 !testDir(x + 1, z, grid, rows, columns))
        {

            meshR.material = decor2A;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        #endregion

        #region 1Dir
        else if (testDir(x, z + 1, grid, rows, columns) &&
                 !testDir(x, z - 1, grid, rows, columns) &&
                 !testDir(x - 1, z, grid, rows, columns) &&
                 !testDir(x + 1, z, grid, rows, columns))
        {

            meshR.material = decor1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (!testDir(x, z + 1, grid, rows, columns) &&
                 testDir(x, z - 1, grid, rows, columns) &&
                 !testDir(x - 1, z, grid, rows, columns) &&
                 !testDir(x + 1, z, grid, rows, columns))
        {

            meshR.material = decor1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (!testDir(x, z + 1, grid, rows, columns) &&
                 !testDir(x, z - 1, grid, rows, columns) &&
                 testDir(x - 1, z, grid, rows, columns) &&
                 !testDir(x + 1, z, grid, rows, columns))
        {

            meshR.material = decor1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (!testDir(x, z + 1, grid, rows, columns) &&
                 !testDir(x, z - 1, grid, rows, columns) &&
                 !testDir(x - 1, z, grid, rows, columns) &&
                 testDir(x + 1, z, grid, rows, columns))
        {

            meshR.material = decor1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        #endregion
        
        #region 0Dir
        else
        {
            
            meshR.material = decor0;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        #endregion
    }


    bool testDir(int newX, int newZ, GridTiles[,] grid, int rows, int columns)
    {
        if (newX >= 0 && newX < rows && newZ >= 0 && newZ < columns)
        {
            if (grid[newX, newZ] != null && grid[newX, newZ].walkable)
                return true;
            else
                return false;
        }
        else return false;
    }
}
