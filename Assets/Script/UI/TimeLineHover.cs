using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class TimeLineHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    UI_ActionManager actionManager;
    [HideInInspector] public RectTransform rectTransform;
    private void Start()
    {
        actionManager = FindObjectOfType<UI_ActionManager>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (actionManager.dragging)
        {
            actionManager.currentHoveredItem = this;
            actionManager.hovering = true;
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
            actionManager.hovering = false;
    }
}
