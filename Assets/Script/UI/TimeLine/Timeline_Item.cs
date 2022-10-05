using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timeline_Item : MonoBehaviour
{
    public UI_Actions.Action actionType;
    public UI_Actions.PlayerTarget playerTarget;
    [HideInInspector] public RectTransform rectTransform;
    ActionEventsCaller actionEventCaller;
    public Image highlight;
    public UI_TimeLineManager timeLineManager;
    public Image rootImage, childImage, highlighter;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        actionEventCaller = FindObjectOfType<ActionEventsCaller>();
        timeLineManager = FindObjectOfType<UI_TimeLineManager>();
    }

    public void invokeTimeLineEvent()
    {
        if((playerTarget == UI_Actions.PlayerTarget.Avatar_A || playerTarget == UI_Actions.PlayerTarget.Both) && (timeLineManager.endA /*|| timeLineManager.stunnedA*/))
        {
            timeLineManager.playerAready = true;
            if(playerTarget == UI_Actions.PlayerTarget.Both)
                playerTarget = UI_Actions.PlayerTarget.Avatar_B;
        }
        else if((playerTarget == UI_Actions.PlayerTarget.Avatar_B || playerTarget == UI_Actions.PlayerTarget.Both) && (timeLineManager.endB /*|| timeLineManager.stunnedB*/))
        {
            timeLineManager.playerBready = true;
            if (playerTarget == UI_Actions.PlayerTarget.Both)
                playerTarget = UI_Actions.PlayerTarget.Avatar_A;
        }

        if(!timeLineManager.playerAready || !timeLineManager.playerBready)
        {
            switch (actionType)
            {
                case UI_Actions.Action.MoveForward:
                    actionEventCaller.MoveEventCaller(1, playerTarget);

                    if (playerTarget == UI_Actions.PlayerTarget.Avatar_B)
                        timeLineManager.playerAready = true;
                    else if (playerTarget == UI_Actions.PlayerTarget.Avatar_A)
                        timeLineManager.playerBready = true;

                    break;

                case UI_Actions.Action.MoveDown:
                    actionEventCaller.MoveEventCaller(2, playerTarget);

                    if (playerTarget == UI_Actions.PlayerTarget.Avatar_B)
                        timeLineManager.playerAready = true;
                    else if (playerTarget == UI_Actions.PlayerTarget.Avatar_A)
                        timeLineManager.playerBready = true;

                    break;

                case UI_Actions.Action.MoveLeft:
                    actionEventCaller.MoveEventCaller(3, playerTarget);

                    if (playerTarget == UI_Actions.PlayerTarget.Avatar_B)
                        timeLineManager.playerAready = true;
                    else if (playerTarget == UI_Actions.PlayerTarget.Avatar_A)
                        timeLineManager.playerBready = true;

                    break;

                case UI_Actions.Action.MoveRight:
                    actionEventCaller.MoveEventCaller(4, playerTarget);

                    if (playerTarget == UI_Actions.PlayerTarget.Avatar_B)
                        timeLineManager.playerAready = true;
                    else if (playerTarget == UI_Actions.PlayerTarget.Avatar_A)
                        timeLineManager.playerBready = true;

                    break;

                case UI_Actions.Action.RotateLeft:
                    actionEventCaller.RotateEventCaller(false, playerTarget);

                    if (playerTarget == UI_Actions.PlayerTarget.Avatar_B)
                        timeLineManager.playerAready = true;
                    else if (playerTarget == UI_Actions.PlayerTarget.Avatar_A)
                        timeLineManager.playerBready = true;

                    break;

                case UI_Actions.Action.RotateRight:
                    actionEventCaller.RotateEventCaller(true, playerTarget);

                    if (playerTarget == UI_Actions.PlayerTarget.Avatar_B)
                        timeLineManager.playerAready = true;
                    else if (playerTarget == UI_Actions.PlayerTarget.Avatar_A)
                        timeLineManager.playerBready = true;

                    break;

                default:
                    break;
            }
        }
    }
}
