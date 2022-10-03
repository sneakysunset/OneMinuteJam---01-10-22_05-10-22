using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UI_InstDragAction : MonoBehaviour
{
    public GameObject dragItemPref;
    UI_ActionManager actionManager;
    public Color blue, red;
    public Sprite forwardArrow, rotateRightArrow, rotateLeftArrow;

    private void Start()
    {
        actionManager = FindObjectOfType<UI_ActionManager>();
    }

    public void InstatiateDragItem(Vector2 pos, UI_Actions.Action typeOfAction, UI_Actions.PlayerTarget playerTarget)
    {
        pos.x -= (Screen.width / 2);
        pos.y -= (Screen.height / 2);
        UI_DragItem dragItem = Instantiate(dragItemPref, pos, Quaternion.identity, GameObject.FindGameObjectWithTag("DragItemFolder").transform).GetComponent<UI_DragItem>();
        dragItem.dragged = true;
        if (actionManager.currentDraggedItem != null)
        {
            Destroy(actionManager.currentDraggedItem.gameObject);
        }
        actionManager.currentDraggedItem = dragItem;
        dragItem.name = "DragItem - " + typeOfAction.ToString();
        //dragItem.transform.GetComponentInChildren<TextMeshProUGUI>().text = typeOfAction.ToString();

        switch (typeOfAction)
        {
            case UI_Actions.Action.MoveForward:
                dragItem.ChildI.sprite = forwardArrow;
                break;

            case UI_Actions.Action.RotateLeft:
                dragItem.ChildI.sprite = rotateLeftArrow;
                break;

            case UI_Actions.Action.RotateRight:
                dragItem.ChildI.sprite = rotateRightArrow;
                break;
        }

        switch (playerTarget)
        {
            case UI_Actions.PlayerTarget.Avatar_A:
                dragItem.RootI.color =/* actionManager.colorAvatarA*/blue;
                break;

            case UI_Actions.PlayerTarget.Avatar_B:
                dragItem.RootI.color = /*actionManager.colorAvatarB*/red;
                break;

            case UI_Actions.PlayerTarget.Both:
                dragItem.RootI.color = /*actionManager.colorBoth*/Color.white;
                break;

        }
        dragItem.actionType = typeOfAction;
        dragItem.playerTarget = playerTarget;
        actionManager.dragging = true;
    }
}
