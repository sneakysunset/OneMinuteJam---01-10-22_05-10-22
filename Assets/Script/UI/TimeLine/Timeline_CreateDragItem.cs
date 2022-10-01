using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Timeline_CreateDragItem : MonoBehaviour
{
    public UnityEvent<Vector2, UI_Actions.Action, UI_Actions.PlayerTarget> OnActionClickEvent;

    public void InstantiateDragItem()
    {
        Timeline_Item timelineItem = GetComponent<Timeline_Item>();
        UI_Actions.Action actionType = timelineItem.actionType;
        UI_Actions.PlayerTarget playerTarget = timelineItem.playerTarget;
        OnActionClickEvent?.Invoke(Input.mousePosition, actionType, playerTarget);
        FindObjectOfType<UI_TimeLineManager>().RemoveAction(timelineItem);
        //FindObjectOfType<UI_TimeLineManager>().timeline_Items.Remove(timelineItem);
        Destroy(this.gameObject);
    }
}
