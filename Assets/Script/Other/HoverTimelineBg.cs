using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class HoverTimelineBg : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    UI_ActionManager actionManager;

    void Start()
    {
        actionManager = FindObjectOfType<UI_ActionManager>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        actionManager.hover = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        actionManager.hover = false;
    }
}
