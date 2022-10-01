using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_DragItem : MonoBehaviour
{
    public UI_Actions.Action actionType;
    public UI_Actions.PlayerTarget playerTarget;
    
    public RectTransform rectTransform;
    UI_ActionManager actionManager;
    public bool dragged, anchored;
    public GameObject Timeline_Item;
    UI_TimeLineManager timeLineManager;
    private void Start()
    {
        actionManager = FindObjectOfType<UI_ActionManager>();
        timeLineManager = FindObjectOfType<UI_TimeLineManager>();
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
            RectTransform timelineItem = Instantiate(Timeline_Item, GameObject.FindGameObjectWithTag("ActionFolder").transform).GetComponent<RectTransform>();
            Timeline_Item item = timelineItem.GetComponent<Timeline_Item>();
            item.actionType = actionType;
            item.playerTarget = playerTarget;
            timelineItem.GetComponentInChildren<TextMeshProUGUI>().text = actionType.ToString();

            switch (playerTarget)
            {
                case UI_Actions.PlayerTarget.Avatar_A:
                    timelineItem.GetComponentInChildren<Image>().color = actionManager.colorAvatarA;
                    break;

                case UI_Actions.PlayerTarget.Avatar_B:
                    timelineItem.transform.GetComponentInChildren<Image>().color = actionManager.colorAvatarB;
                    break;

                case UI_Actions.PlayerTarget.Both:
                    timelineItem.transform.GetComponentInChildren<Image>().color = actionManager.colorBoth;
                    break;

            }
            timelineItem.anchoredPosition = rectTransform.anchoredPosition;
            timeLineManager.InsertAction(actionManager.currentHoveredItem, timelineItem.GetComponent<Timeline_Item>());
            Destroy(this.gameObject);
        }
    }


}