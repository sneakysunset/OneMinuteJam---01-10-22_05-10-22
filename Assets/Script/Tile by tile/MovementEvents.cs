using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementEvents : MonoBehaviour
{
    public Avatar avatar = Avatar.Both;
    Transform player = null;
    Transform playerA;
    Transform playerB;
    GridTiles[,] grid;

    public enum Avatar
    {
        Avatar_A,
        Avatar_B,
        Both
    }

    private void Awake()
    {
        playerA = GridGenerator.Instance.player_A;
        playerB = GridGenerator.Instance.player_B;

        grid = GridGenerator.Instance.grid;
    }

    public UnityEvent<Vector3, Transform> MoveEvent;
    public void MovementActivation(int direction)
    {

        

        switch (avatar)
        {
            case Avatar.Avatar_A:
                player = playerA;
                break;
            case Avatar.Avatar_B:
                player = playerB;
                break;
            case Avatar.Both:
                moveBoth(direction);
                return;
        }

        switch (direction)
        {
            //up
            case 1:
                if (GridGenerator.Instance.TestDirectionForMovement((int)player.position.x, (int)player.position.z, direction))
                {
                    MoveEvent?.Invoke(grid[(int)player.position.x, (int)player.position.z + 1].transform.position, player);
                }
                    return;

            //down
            case 2:
                if (GridGenerator.Instance.TestDirectionForMovement((int)player.position.x, (int)player.position.z, direction))
                {
                    MoveEvent?.Invoke(grid[(int)player.position.x, (int)player.position.z - 1].transform.position, player);
                }
                    return;

            //left
            case 3:
                if (GridGenerator.Instance.TestDirectionForMovement((int)player.position.x, (int)player.position.z, direction))
                {
                    MoveEvent?.Invoke(grid[(int)player.position.x - 1, (int)player.position.z].transform.position, player);
                }
                return;

            //right
            case 4:
                if (GridGenerator.Instance.TestDirectionForMovement((int)player.position.x, (int)player.position.z, direction))
                {
                    MoveEvent?.Invoke(grid[(int)player.position.x + 1, (int)player.position.z].transform.position, player);
                }
                return;
            default:
                return;

        }

    }
    
    void moveBoth(int direction)
    {
        switch (direction)
        {
            //up
            case 1:
                if (GridGenerator.Instance.TestDirectionForMovement((int)playerA.position.x, (int)playerA.position.z, direction))
                {
                    MoveEvent?.Invoke(grid[(int)playerA.position.x, (int)playerA.position.z + 1].transform.position, playerA);
                }
                break;

            //down
            case 2:
                if (GridGenerator.Instance.TestDirectionForMovement((int)playerA.position.x, (int)playerA.position.z, direction))
                {
                    MoveEvent?.Invoke(grid[(int)playerA.position.x, (int)playerA.position.z - 1].transform.position, playerA);
                }
                break;

            //left
            case 3:
                if (GridGenerator.Instance.TestDirectionForMovement((int)playerA.position.x, (int)playerA.position.z, direction))
                {
                    MoveEvent?.Invoke(grid[(int)playerA.position.x - 1, (int)playerA.position.z].transform.position, playerA);
                }
                break;

            //right
            case 4:
                if (GridGenerator.Instance.TestDirectionForMovement((int)playerA.position.x, (int)playerA.position.z, direction))
                {
                    MoveEvent?.Invoke(grid[(int)playerA.position.x + 1, (int)playerA.position.z].transform.position, playerA);
                }
                break;
            default:
                break;

        }

        switch (direction)
        {
            //up
            case 1:
                if (GridGenerator.Instance.TestDirectionForMovement((int)playerB.position.x, (int)playerB.position.z, direction))
                {
                    MoveEvent?.Invoke(grid[(int)playerB.position.x, (int)playerB.position.z + 1].transform.position, playerB);
                }
                return;

            //down
            case 2:
                if (GridGenerator.Instance.TestDirectionForMovement((int)playerB.position.x, (int)playerB.position.z, direction))
                {
                    MoveEvent?.Invoke(grid[(int)playerB.position.x, (int)playerB.position.z - 1].transform.position, playerB);
                }
                return;

            //left
            case 3:
                if (GridGenerator.Instance.TestDirectionForMovement((int)playerB.position.x, (int)playerB.position.z, direction))
                {
                    MoveEvent?.Invoke(grid[(int)playerB.position.x - 1, (int)playerB.position.z].transform.position, playerB);
                }
                return;

            //right
            case 4:
                if (GridGenerator.Instance.TestDirectionForMovement((int)playerB.position.x, (int)playerB.position.z, direction))
                {
                    MoveEvent?.Invoke(grid[(int)playerB.position.x + 1, (int)playerB.position.z].transform.position, playerB);
                }
                return;
            default:
                return;

        }

    }
}
