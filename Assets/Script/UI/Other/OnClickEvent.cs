using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnClickEvent : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent OnActionClickEvent;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        OnActionClickEvent?.Invoke();
    }
}
