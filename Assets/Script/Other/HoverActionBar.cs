using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class HoverActionBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    UI_ActionManager actionManager;

    void Start()
    {
        actionManager = FindObjectOfType<UI_ActionManager>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        actionManager.hoverAction = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        actionManager.hoverAction = false;
    }
}
