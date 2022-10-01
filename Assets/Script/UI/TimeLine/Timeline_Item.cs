using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeline_Item : MonoBehaviour
{
    public UI_Actions.Action actionType;
    public UI_Actions.PlayerTarget playerTarget;
    [HideInInspector] public RectTransform rectTransform;
    ActionEventsCaller actionEventCaller;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        actionEventCaller = FindObjectOfType<ActionEventsCaller>();
    }

    public void invokeTimeLineEvent()
    {
        switch (actionType)
        {
            case UI_Actions.Action.MoveForward:
                actionEventCaller.MoveEventCaller(1, playerTarget);
                break;

            case UI_Actions.Action.MoveDown:
                actionEventCaller.MoveEventCaller(2, playerTarget);
                break;

            case UI_Actions.Action.MoveLeft:
                actionEventCaller.MoveEventCaller(3, playerTarget);
                break;

            case UI_Actions.Action.MoveRight:
                actionEventCaller.MoveEventCaller(4, playerTarget);
                break;

            case UI_Actions.Action.RotateLeft:
                actionEventCaller.RotateEventCaller(false, playerTarget);
                break;

            case UI_Actions.Action.RotateRight:
                actionEventCaller.RotateEventCaller(true, playerTarget);
                break;

            default:
                break;
        }
    }
}
