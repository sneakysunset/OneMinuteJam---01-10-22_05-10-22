using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class UI_CreateDragItem : MonoBehaviour
{
    public UnityEvent<Vector2, UI_Actions.Action, UI_Actions.PlayerTarget> OnActionClickEvent;

    public void InstantiateDragItem()
    {
        UI_Actions.Action actionType = GetComponent<UI_Actions>().actionType;
        UI_Actions.PlayerTarget playerTarget = GetComponent<UI_Actions>().Avatar;
        OnActionClickEvent?.Invoke(Input.mousePosition, actionType, playerTarget);
    }
}
