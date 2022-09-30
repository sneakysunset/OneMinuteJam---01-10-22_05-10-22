using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementEvents : MonoBehaviour
{
    public UnityEvent<Vector3, Transform> MoveEvent;
    public void MovementActivation(int direction)
    {
        Transform playerT = GridGenerator.Instance.playerT;
        GridTiles[,] grid = GridGenerator.Instance.grid;
        switch (direction)
        {
            //up
            case 1:
                if (GridGenerator.Instance.TestDirectionForMovement((int)playerT.position.x, (int)playerT.position.z, direction))
                {
                    MoveEvent?.Invoke(grid[(int)playerT.position.x, (int)playerT.position.z + 1].transform.position, playerT);
                }
                    return;

            //down
            case 2:
                if (GridGenerator.Instance.TestDirectionForMovement((int)playerT.position.x, (int)playerT.position.z, direction))
                {
                    MoveEvent?.Invoke(grid[(int)playerT.position.x, (int)playerT.position.z - 1].transform.position, playerT);
                }
                    return;

            //left
            case 3:
                if (GridGenerator.Instance.TestDirectionForMovement((int)playerT.position.x, (int)playerT.position.z, direction))
                {
                    MoveEvent?.Invoke(grid[(int)playerT.position.x - 1, (int)playerT.position.z].transform.position, playerT);
                }
                return;

            //right
            case 4:
                if (GridGenerator.Instance.TestDirectionForMovement((int)playerT.position.x, (int)playerT.position.z, direction))
                {
                    MoveEvent?.Invoke(grid[(int)playerT.position.x + 1, (int)playerT.position.z].transform.position, playerT);
                }
                return;
            default:
                return;

        }

    }
}
