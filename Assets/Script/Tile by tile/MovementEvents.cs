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
        
        //calculate next positions relative to player forward
        Vector3 fwdPos = player.position + player.forward;
        Vector3 RightPos = player.position + player.right;
        Vector3 LeftPos = player.position - player.right;
        Vector3 BackPos = player.position - player.forward;



        switch (direction)
        {
            //up
            case 1:
                if (GridGenerator.Instance.TestDirectionForMovement((int)player.position.x, (int)player.position.z, direction))
                {
                    MoveEvent?.Invoke(grid[(int)fwdPos.x, (int)fwdPos.z].transform.position, player);
                }
                    return;

            //down
            case 2:
                if (GridGenerator.Instance.TestDirectionForMovement((int)player.position.x, (int)player.position.z, direction))
                {
                    MoveEvent?.Invoke(grid[(int)BackPos.x, (int)BackPos.z].transform.position, player);
                }
                    return;

            //left
            case 3:
                if (GridGenerator.Instance.TestDirectionForMovement((int)player.position.x, (int)player.position.z, direction))
                {
                    MoveEvent?.Invoke(grid[(int)LeftPos.x, (int)LeftPos.z].transform.position, player);
                }
                return;

            //right
            case 4:
                if (GridGenerator.Instance.TestDirectionForMovement((int)player.position.x, (int)player.position.z, direction))
                {
                    MoveEvent?.Invoke(grid[(int)RightPos.x, (int)RightPos.z].transform.position, player);
                }
                return;
            default:
                return;

        }

    }
    
    void moveBoth(int direction)
    {
        player = playerA;
        for (int i = 0; i < 2; i++)
        {
            if (i == 1) player = playerB;


            Vector3 fwdPos = player.position + player.forward;
            Vector3 RightPos = player.position + player.right;
            Vector3 LeftPos = player.position - player.right;
            Vector3 BackPos = player.position - player.forward;

            switch (direction)
            {
                //up
                case 1:
                    if (GridGenerator.Instance.TestDirectionForMovement((int)player.position.x, (int)player.position.z, direction))
                    {
                        MoveEvent?.Invoke(grid[(int)fwdPos.x, (int)fwdPos.z].transform.position, player);
                    }
                    break;

                //down
                case 2:
                    if (GridGenerator.Instance.TestDirectionForMovement((int)player.position.x, (int)player.position.z, direction))
                    {
                        MoveEvent?.Invoke(grid[(int)BackPos.x, (int)BackPos.z].transform.position, player);
                    }
                    break;

                //left
                case 3:
                    if (GridGenerator.Instance.TestDirectionForMovement((int)player.position.x, (int)player.position.z, direction))
                    {
                        MoveEvent?.Invoke(grid[(int)LeftPos.x, (int)LeftPos.z].transform.position, player);
                    }
                    break;

                //right
                case 4:
                    if (GridGenerator.Instance.TestDirectionForMovement((int)player.position.x, (int)player.position.z, direction))
                    {
                        MoveEvent?.Invoke(grid[(int)RightPos.x, (int)RightPos.z].transform.position, player);
                    }
                    break;
                default:
                    break;

            }
        }

    }
}

