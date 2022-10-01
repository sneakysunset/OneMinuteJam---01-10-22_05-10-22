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

    public UnityEvent<Vector3, Transform, UI_Actions.PlayerTarget> MoveEvent;
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
        Vector3 LeftPos = player.position + player.right * -1;
        Vector3 BackPos = player.position + player.forward * -1;

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


        if (GridGenerator.Instance.TestDirectionForMovement((int)player.position.x, (int)player.position.z, (int)nextPos.x, (int)nextPos.z, direction, playerTarget))
        {
            MoveEvent?.Invoke(grid[Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z)].transform.position, player, playerTarget);
        }

    }
    
    public void TapisRoulantMovement(int direction, UI_Actions.PlayerTarget playerTarget)
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

        Vector3 nextPos = Vector3.zero;

        switch (direction)
        {
            //up
            case 1:
                nextPos = player.position + Vector3.forward;
                break;

            //down
            case 2:
                nextPos = player.position + Vector3.back;
                break;

            //left
            case 3:
                nextPos = player.position + Vector3.left;
                break;

            //right
            case 4:
                nextPos = player.position + Vector3.right ;
                break;
            default:
                break;

        }

        if (GridGenerator.Instance.TestDirectionForMovement(Mathf.RoundToInt(player.position.x), Mathf.RoundToInt(player.position.z), Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z), direction, playerTarget))
        {
            
            MoveEvent?.Invoke(grid[Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z)].transform.position, player, playerTarget);
        }
    }

    void moveBoth(int direction)
    {
        var playerTarget = UI_Actions.PlayerTarget.Avatar_A;
        player = playerA;
        for (int i = 0; i < 2; i++)
        {
            if (i == 1) 
            {
                player = playerB; 
                playerTarget = UI_Actions.PlayerTarget.Avatar_B;

            }


            Vector3 fwdPos = player.position + player.forward;
            Vector3 RightPos = player.position + player.right;
            Vector3 LeftPos = player.position + player.right * -1;
            Vector3 BackPos = player.position + player.forward * -1;


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


            if (GridGenerator.Instance.TestDirectionForMovement(Mathf.RoundToInt(player.position.x), Mathf.RoundToInt(player.position.y), Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z), direction, playerTarget))
            {
                MoveEvent?.Invoke(grid[Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z)].transform.position, player, playerTarget);
            }
        }

    }
}

