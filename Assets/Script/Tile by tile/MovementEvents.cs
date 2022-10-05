using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FMODUnity;

public class MovementEvents : MonoBehaviour
{
    //public Avatar playerTarget = Avatar.Both;
    Transform player = null;
    Transform playerA;
    Transform playerB;
    GridTiles[,] grid;
    public Vector3 nextPos1, nextPos2;
    Vector3 playerPos1, playerPos2;
    UI_TimeLineManager timeLineManager;
    Vector3 previousPosA, previousPosB;

    public enum Avatar
    {
        Avatar_A,
        Avatar_B,
        Both
    }
    private void Awake()
    {
        nextPos1 = new Vector3(-1, -1, -1);
        playerA = GridGenerator.Instance.player_A;
        playerB = GridGenerator.Instance.player_B;
        timeLineManager = FindObjectOfType<UI_TimeLineManager>();
        grid = GridGenerator.Instance.grid;
    }

    public UnityEvent<Vector3, Transform, UI_Actions.PlayerTarget> MoveEvent;
    public UnityEvent<Vector3, Transform, UI_Actions.PlayerTarget, bool> cancelledMoveEvent;
    public void MovementActivation(int direction, UI_Actions.PlayerTarget playerTarget, Vector3 previousPos)
    {
        Transform otherPlayer = null;

        switch (playerTarget)
        {
            case UI_Actions.PlayerTarget.Avatar_A:
                player = playerA;
                otherPlayer = playerB;
                previousPosA = playerA.position;
                break;
            case UI_Actions.PlayerTarget.Avatar_B:
                player = playerB;
                otherPlayer = playerA;
                previousPosB = playerB.position;
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

        if(grid[Mathf.RoundToInt(otherPlayer.position.x), Mathf.RoundToInt(otherPlayer.position.z)] != grid[Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z)])
        {
            if (GridGenerator.Instance.TestDirectionForMovement(Mathf.RoundToInt(player.position.x), Mathf.RoundToInt(player.position.z), Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z), playerTarget, new Vector3(nextPos.x, player.position.y, nextPos.z), player))
            {
                RuntimeManager.PlayOneShot("event:/Avatar/Blue/Step");
                MoveEvent?.Invoke(grid[Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z)].transform.position, player, playerTarget);
            }
        }
        else if (playerTarget == UI_Actions.PlayerTarget.Avatar_A)
        {
            timeLineManager.playerAready = true;
        }
        else
        {
            timeLineManager.playerBready = true;
        }


    }
    
    public void TapisRoulantMovement(int direction, UI_Actions.PlayerTarget playerTarget,Vector3 previousPos)
    {
        Transform otherPlayer = null;

        print(timeLineManager.playerAready);
        print(timeLineManager.playerBready);
        if(!timeLineManager.playerAready || !timeLineManager.playerBready)
        {
            timeLineManager.both = false;
        }


        switch (playerTarget)
        {
            case UI_Actions.PlayerTarget.Avatar_A:
                player = playerA;
                previousPosA = playerA.position;
                otherPlayer = playerB;
                break;
            case UI_Actions.PlayerTarget.Avatar_B:
                player = playerB;
                previousPosB = playerB.position;
                otherPlayer = playerA;
                break;
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
        GridGenerator gridG = GridGenerator.Instance;
        if (nextPos.x >= 0 && nextPos.x < gridG.rows && nextPos.z >= 0 && nextPos.z < gridG.columns)
        { 
            if (timeLineManager.both)
            {
                print(4);
                if (playerTarget == UI_Actions.PlayerTarget.Avatar_A)
                {
                    nextPos1 = nextPos;
                    playerPos1 = player.position;
                    timeLineManager.bothA = true;
                    StartCoroutine(bothSpecialMovementResolver(playerTarget, nextPos, player));
                }
                else
                {
                    nextPos2 = nextPos;
                    playerPos2 = player.position;
                    timeLineManager.bothB = true;
                    StartCoroutine(bothSpecialMovementResolver(playerTarget, nextPos, player));
                }
            }
            else if (grid[Mathf.RoundToInt(otherPlayer.position.x), Mathf.RoundToInt(otherPlayer.position.z)] == grid[Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z)] && !timeLineManager.both)
            {
                print(5);
                if (playerTarget == UI_Actions.PlayerTarget.Avatar_A)
                {
                    timeLineManager.playerAready = true;
                }
                else
                {
                    timeLineManager.playerBready = true;
                }
            }
            else
            {
                print(6);
                if (GridGenerator.Instance.TestDirectionForMovement(Mathf.RoundToInt(player.position.x), Mathf.RoundToInt(player.position.z), Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z), playerTarget, new Vector3(nextPos.x, player.position.y, nextPos.z), player))
                {
                    print(3);
                    RuntimeManager.PlayOneShot("event:/Elements/Ice");
                    MoveEvent?.Invoke(grid[Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z)].transform.position, player, playerTarget);
                }
            }
        }
        else
        {
            print(7);
            if (GridGenerator.Instance.TestDirectionForMovement(Mathf.RoundToInt(player.position.x), Mathf.RoundToInt(player.position.z), Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z), playerTarget, new Vector3(nextPos.x, player.position.y, nextPos.z), player))
            {
                RuntimeManager.PlayOneShot("event:/Elements/Ice");
                MoveEvent?.Invoke(grid[Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z)].transform.position, player, playerTarget);
            }
        }
    }

    public void GlaceMovement(UI_Actions.PlayerTarget playerTarget, Vector3 previousPos)
    {
        if (!timeLineManager.playerAready || !timeLineManager.playerBready)
        {
            timeLineManager.both = false;
        }
        Vector3 prevPos = Vector3.zero;
        Transform otherPlayer = null;
        switch (playerTarget)
        {
            case UI_Actions.PlayerTarget.Avatar_A:
                player = playerA;
                prevPos = previousPosA;
                otherPlayer = playerB;
                break;
            case UI_Actions.PlayerTarget.Avatar_B:
                player = playerB;
                prevPos = previousPosB;
                otherPlayer = playerA;
                break;
        }

        Vector3 nextPos = Vector3.zero;

        nextPos.x =  2 * player.position.x - previousPos.x;
        nextPos.z = 2 * player.position.z - previousPos.z;

        GridGenerator gridG = GridGenerator.Instance;
        if (nextPos.x >= 0 && nextPos.x < gridG.rows && nextPos.z >= 0 && nextPos.z < gridG.columns)
        { 
            if (timeLineManager.both)
            {
                    if(playerTarget == UI_Actions.PlayerTarget.Avatar_A)
                    {
                        nextPos1 = nextPos;
                        playerPos1 = player.position;
                        timeLineManager.bothA = true;
                        StartCoroutine(bothSpecialMovementResolver(playerTarget, nextPos, player));
                    }
                    else
                    {
                        nextPos2 = nextPos;
                        playerPos2 = player.position;
                        timeLineManager.bothB = true;
                        StartCoroutine(bothSpecialMovementResolver(playerTarget, nextPos, player));
                    }
            }
            else if (grid[Mathf.RoundToInt(otherPlayer.position.x), Mathf.RoundToInt(otherPlayer.position.z)] == grid[Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z)] && !timeLineManager.both)
            {
                if (playerTarget == UI_Actions.PlayerTarget.Avatar_A)
                {
                    timeLineManager.playerAready = true;
                }
                else
                {
                    timeLineManager.playerBready = true;
                }
            }
            else
            {
                if (GridGenerator.Instance.TestDirectionForMovement(Mathf.RoundToInt(player.position.x), Mathf.RoundToInt(player.position.z), Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z), playerTarget, new Vector3(nextPos.x, player.position.y, nextPos.z), player))
                {
                    RuntimeManager.PlayOneShot("event:/Elements/Ice");
                    MoveEvent?.Invoke(grid[Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z)].transform.position, player, playerTarget);
                }
            }
        }
        else
        {
            if (GridGenerator.Instance.TestDirectionForMovement(Mathf.RoundToInt(player.position.x), Mathf.RoundToInt(player.position.z), Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z), playerTarget, new Vector3(nextPos.x, player.position.y, nextPos.z), player))
            {
                RuntimeManager.PlayOneShot("event:/Elements/Ice");
                MoveEvent?.Invoke(grid[Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z)].transform.position, player, playerTarget);
            }
        }
    }


    void moveBoth(int direction)
    {
        Transform otherPlayer = null;

        var playerTarget = UI_Actions.PlayerTarget.Avatar_A;
        player = playerA;
        otherPlayer = playerB;
        timeLineManager.both = true;
        for (int i = 0; i < 2; i++)
        {
            if (i == 1) 
            {
                player = playerB;
                otherPlayer = playerA;
                playerTarget = UI_Actions.PlayerTarget.Avatar_B;

            }


            Vector3 fwdPos = player.position + player.forward;
            Vector3 RightPos = player.position + player.right;
            Vector3 LeftPos = player.position + player.right * -1;
            Vector3 BackPos = player.position + player.forward * -1;


            Vector3 nextPos = Vector3.zero;
            Vector3 otherNextPos = Vector3.zero;
            switch (direction)
            {
                //up
                case 1:
                    nextPos = fwdPos;
                    otherNextPos = otherPlayer.position + otherPlayer.forward;
                    break;

                //down
                case 2:
                    nextPos = BackPos;
                    otherNextPos = otherPlayer.position - otherPlayer.forward;
                    break;

                //left
                case 3:
                    nextPos = LeftPos;
                    otherNextPos = otherPlayer.position - otherPlayer.right;
                    break;

                //right
                case 4:
                    nextPos = RightPos;
                    otherNextPos = otherPlayer.position + otherPlayer.right;
                    break;
                default:
                    break;

            }
            //print(grid[Mathf.RoundToInt(player.position.x), Mathf.RoundToInt(player.position.z)].name + "      " + grid[Mathf.RoundToInt(otherNextPos.x), Mathf.RoundToInt(otherNextPos.z)].name);
            //print(grid[Mathf.RoundToInt(otherPlayer.position.x), Mathf.RoundToInt(otherPlayer.position.z)].name + "      " + grid[Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z)].name);

            GridGenerator gridG = GridGenerator.Instance; 
            if(nextPos.x >= 0  && nextPos.x < gridG.rows && nextPos.z >= 0 && nextPos.z <gridG.columns && otherNextPos.x >= 0 && otherNextPos.x < gridG.rows && otherNextPos.z >= 0 && otherNextPos.z < gridG.columns)
            {
                bool bothmeetingInSameTile = grid[Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z)] == grid[Mathf.RoundToInt(otherNextPos.x), Mathf.RoundToInt(otherNextPos.z)];
                bool goingOntoOthersTile = grid[Mathf.RoundToInt(otherPlayer.position.x), Mathf.RoundToInt(otherPlayer.position.z)] == grid[Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z)];
                bool otherWayAround = grid[Mathf.RoundToInt(player.position.x), Mathf.RoundToInt(player.position.z)] == grid[Mathf.RoundToInt(otherNextPos.x), Mathf.RoundToInt(otherNextPos.z)];

                if (bothmeetingInSameTile || (goingOntoOthersTile && otherWayAround))
                {
                    if (playerTarget == UI_Actions.PlayerTarget.Avatar_A)
                    {
                        timeLineManager.playerAready = true;
                    }
                    else
                    {
                        timeLineManager.playerBready = true;
                    }
                    cancelledMoveEvent?.Invoke(grid[Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z)].transform.position, player, playerTarget, true);
                }
                else if (GridGenerator.Instance.TestDirectionForMovement(Mathf.RoundToInt(player.position.x), Mathf.RoundToInt(player.position.y), Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z), playerTarget, new Vector3(nextPos.x, player.position.y, nextPos.z), player))
                {
                    print(1);
                    RuntimeManager.PlayOneShot("event:/Avatar/Blue/Step");
                    MoveEvent?.Invoke(grid[Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z)].transform.position, player, playerTarget);
                }
            }
            else
            {
                if (GridGenerator.Instance.TestDirectionForMovement(Mathf.RoundToInt(player.position.x), Mathf.RoundToInt(player.position.y), Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z), playerTarget, new Vector3(nextPos.x, player.position.y, nextPos.z), player))
                {
                    RuntimeManager.PlayOneShot("event:/Avatar/Blue/Step");
                    MoveEvent?.Invoke(grid[Mathf.RoundToInt(nextPos.x), Mathf.RoundToInt(nextPos.z)].transform.position, player, playerTarget);
                }
            }
        }
    }


    IEnumerator bothSpecialMovementResolver(UI_Actions.PlayerTarget playerTarget, Vector3 currentNextPos, Transform currentPlayer)
    {
        yield return new WaitUntil(() => timeLineManager.bothA && timeLineManager.bothB);

        bool comingOntoYou1 = grid[Mathf.RoundToInt(playerPos1.x), Mathf.RoundToInt(playerPos1.z)] == grid[Mathf.RoundToInt(nextPos2.x), Mathf.RoundToInt(nextPos2.z)];
        bool comingOntoYou2 = grid[Mathf.RoundToInt(playerPos2.x), Mathf.RoundToInt(playerPos2.z)] == grid[Mathf.RoundToInt(nextPos1.x), Mathf.RoundToInt(nextPos1.z)];
        bool meetAtCenter = grid[Mathf.RoundToInt(nextPos1.x), Mathf.RoundToInt(nextPos1.z)] == grid[Mathf.RoundToInt(nextPos2.x), Mathf.RoundToInt(nextPos2.z)];
        if (meetAtCenter || (comingOntoYou1 && comingOntoYou2))
        {
            if (playerTarget == UI_Actions.PlayerTarget.Avatar_A)
            {
                timeLineManager.playerAready = true;
                playerTarget = UI_Actions.PlayerTarget.Avatar_A;
            }
            else
            {
                timeLineManager.playerBready = true;
                playerTarget = UI_Actions.PlayerTarget.Avatar_B;

            }


            cancelledMoveEvent?.Invoke(grid[Mathf.RoundToInt(currentNextPos.x), Mathf.RoundToInt(currentNextPos.z)].transform.position, currentPlayer, playerTarget, true);
            
        }
        else
        {
            if (GridGenerator.Instance.TestDirectionForMovement(Mathf.RoundToInt(currentPlayer.position.x), Mathf.RoundToInt(currentPlayer.position.z), Mathf.RoundToInt(currentNextPos.x), Mathf.RoundToInt(currentNextPos.z), playerTarget, new Vector3(currentNextPos.x, currentPlayer.position.y, currentNextPos.z), currentPlayer))
            {
                RuntimeManager.PlayOneShot("event:/Elements/Ice");
                MoveEvent?.Invoke(grid[Mathf.RoundToInt(currentNextPos.x), Mathf.RoundToInt(currentNextPos.z)].transform.position, currentPlayer, playerTarget);
            }
        }
    }

}

