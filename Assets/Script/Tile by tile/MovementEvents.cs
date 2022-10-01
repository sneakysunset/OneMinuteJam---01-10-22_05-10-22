using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementEvents : MonoBehaviour
{
    //public Avatar playerTarget = Avatar.Both;
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
    public void MovementActivation(int direction, UI_Actions.PlayerTarget playerTarget)
    {

        

        switch (playerTarget)
        {
            case UI_Actions.PlayerTarget.Avatar_A:
                player = playerA;
                break;
            case UI_Actions.PlayerTarget.Avatar_B:
                player = playerB;
                break;
            case UI_Actions.PlayerTarget.Both:
                moveBoth(direction);
                return;
        }

        //calculate next positions relative to player forward
        Vector3 fwdPos = player.position + player.forward;
        Vector3 RightPos = player.position + player.right;
        Vector3 LeftPos = player.position - player.right;
        Vector3 BackPos = player.position - player.forward;

        Vector3 nextPos = Vector3.zero;

        switch (direction)
        {
            //up
            case 1:
                nextPos = fwdPos;
                break;
                
            //down
            case 2:
                nextPos = BackPos;
                break;

            //left
            case 3:
                nextPos = LeftPos;
                break;

            //right
            case 4:
                nextPos = RightPos;
                break;
            default:
                break;

        }


        if (GridGenerator.Instance.TestDirectionForMovement((int)player.position.x, (int)player.position.y, (int)nextPos.x, (int)nextPos.z, direction))
        {
            MoveEvent?.Invoke(grid[(int)nextPos.x, (int)nextPos.z].transform.position, player);
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


            Vector3 nextPos = Vector3.zero;

            switch (direction)
            {
                //up
                case 1:
                    nextPos = fwdPos;
                    break;

                //down
                case 2:
                    nextPos = BackPos;
                    break;

                //left
                case 3:
                    nextPos = LeftPos;
                    break;

                //right
                case 4:
                    nextPos = RightPos;
                    break;
                default:
                    break;

            }


            if (GridGenerator.Instance.TestDirectionForMovement((int)player.position.x, (int)player.position.y, (int)nextPos.x, (int)nextPos.z, direction))
            {
                MoveEvent?.Invoke(grid[(int)nextPos.x, (int)nextPos.z].transform.position, player);
            }
        }

    }
}

