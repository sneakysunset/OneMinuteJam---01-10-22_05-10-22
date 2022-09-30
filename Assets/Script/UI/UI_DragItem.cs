using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_DragItem : MonoBehaviour
{
    public UI_Actions.Action actionType;
    public bool dragged;
    public RectTransform rectTransform;


    private void Update()
    {
        if (dragged)
        {
            var pos = rectTransform.anchoredPosition;
            pos.x = Input.mousePosition.x - (Screen.width / 2);
            pos.y = Input.mousePosition.y - (Screen.height / 2);
            rectTransform.anchoredPosition = pos;

            if (Input.GetMouseButtonUp(0))
            {
                dragged = false;
            }
        }
    }


}
