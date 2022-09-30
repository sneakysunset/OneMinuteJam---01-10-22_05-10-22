using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_DragItem : MonoBehaviour
{
    public UI_Actions.Action actionType;
    
    public RectTransform rectTransform;
    UI_ActionManager actionManager;
    public bool dragged, anchored;

    private void Start()
    {
        actionManager = FindObjectOfType<UI_ActionManager>();
    }

    private void Update()
    {
        if (actionManager.dragging && dragged)
        {
            if (actionManager.hovering)
            {
                anchored = true;
                rectTransform.anchoredPosition = actionManager.currentHoveredItem.rectTransform.anchoredPosition;
            }
            else if (!actionManager.hovering)
            {
                anchored = false;
                var pos = rectTransform.anchoredPosition;
                pos.x = Input.mousePosition.x - (Screen.width / 2);
                pos.y = Input.mousePosition.y - (Screen.height / 2);
                rectTransform.anchoredPosition = pos;
            }

            if (Input.GetMouseButtonUp(0))
            {
                dragged = false;
                actionManager.dragging = false;
            }
        }
        else if(!anchored)
        {
            Destroy(this.gameObject);
        }
        else if (anchored)
        {
            
        }
    }


}
