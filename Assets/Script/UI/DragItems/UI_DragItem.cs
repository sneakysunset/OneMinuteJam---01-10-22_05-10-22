using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FMODUnity;

public class UI_DragItem : MonoBehaviour
{
    public UI_Actions.Action actionType;
    public UI_Actions.PlayerTarget playerTarget;
    public Image RootI, ChildI;

    public RectTransform rectTransform;
    UI_ActionManager actionManager;
    public bool dragged, anchored;
    public GameObject Timeline_Item;
    UI_TimeLineManager timeLineManager;
    Scroller canvas;
    public float yOffSet;
    public Sprite forwardRootI, forwardChildI, RotateRightRootI, RotateRightChildI, RotateLeftRootI, RotateLeftChildI;
    private void Start()
    {
        actionManager = FindObjectOfType<UI_ActionManager>();
        timeLineManager = FindObjectOfType<UI_TimeLineManager>();
        RuntimeManager.PlayOneShot("event:/Game/Programming phase/UI_Pick");
        canvas = transform.parent.parent.GetComponent<Scroller>();
    }

    private void Update()
    {
        if (actionManager.dragging && dragged)
        {
            if (actionManager.hovering)
            {
                anchored = true;
                Vector3 anchPos = actionManager.currentHoveredItem.rectTransform.anchoredPosition;
                rectTransform.anchoredPosition = new Vector3(anchPos.x + canvas.scrollDataTimeline, anchPos.y + yOffSet, anchPos.z);
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
            RuntimeManager.PlayOneShot("event:/Game/Programming phase/UI_Drop");
            Destroy(this.gameObject);
        }
        else if (anchored)
        {
            RectTransform timelineItem = Instantiate(Timeline_Item, GameObject.FindGameObjectWithTag("ActionFolder").transform).GetComponent<RectTransform>();
            Timeline_Item item = timelineItem.GetComponent<Timeline_Item>();
            item.actionType = actionType;
            item.playerTarget = playerTarget;
            //timelineItem.GetComponentInChildren<TextMeshProUGUI>().text = actionType.ToString();

            switch (item.actionType)
            {
                case UI_Actions.Action.MoveForward:
                    item.rootImage.sprite = forwardRootI;
                    item.childImage.sprite = forwardChildI;
                    item.highlight.sprite = forwardChildI;
                    break;

                case UI_Actions.Action.RotateLeft:
                    item.rootImage.sprite = RotateLeftRootI;
                    item.childImage.sprite = RotateLeftChildI;
                    item.highlight.sprite = RotateLeftChildI;
                    break;

                case UI_Actions.Action.RotateRight:
                    item.rootImage.sprite = RotateRightRootI;
                    item.childImage.sprite = RotateRightChildI;
                    item.highlight.sprite = RotateRightChildI;
                    break;
            }

            switch (playerTarget)
            {
                case UI_Actions.PlayerTarget.Avatar_A:
                    item.childImage.color = actionManager.colorAvatarA;
                    item.highlight.color = actionManager.colorAvatarA;
                    break;

                case UI_Actions.PlayerTarget.Avatar_B:
                    item.childImage.color = actionManager.colorAvatarB;
                    item.highlight.color = actionManager.colorAvatarB;
                    break;

                case UI_Actions.PlayerTarget.Both:
                    item.childImage.color = actionManager.colorBoth;
                    item.highlight.color = actionManager.colorBoth;
                    break;

            }
            timelineItem.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x - canvas.scrollDataTimeline, rectTransform.anchoredPosition.y - yOffSet);
            timeLineManager.InsertAction(actionManager.currentHoveredItem, timelineItem.GetComponent<Timeline_Item>());
            RuntimeManager.PlayOneShot("event:/Game/Programming phase/UI_Drop");
            Destroy(this.gameObject);
        }
    }


}
