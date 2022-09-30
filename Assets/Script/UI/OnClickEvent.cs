using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnClickEvent : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent<Vector2, UI_Actions.Action> OnActionClickEvent;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        print(1);
        UI_Actions.Action actionType = GetComponent<UI_Actions>().actionType;
        OnActionClickEvent?.Invoke(Input.mousePosition, actionType);
    }
}
