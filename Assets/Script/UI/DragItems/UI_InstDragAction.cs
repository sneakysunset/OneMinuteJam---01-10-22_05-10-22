using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UI_InstDragAction : MonoBehaviour
{
    public GameObject dragItemPref;
    UI_ActionManager actionManager;

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
        dragItem.name = "DragItem - " + typeOfAction.ToString();
        dragItem.transform.GetComponentInChildren<TextMeshProUGUI>().text = typeOfAction.ToString();
        switch (playerTarget)
        {
            case UI_Actions.PlayerTarget.Avatar_A:
                dragItem.GetComponent<Image>().color = actionManager.colorAvatarA;
                break;

            case UI_Actions.PlayerTarget.Avatar_B:
                dragItem.transform.GetComponent<Image>().color = actionManager.colorAvatarB;
                break;

            case UI_Actions.PlayerTarget.Both:
                dragItem.transform.GetComponent<Image>().color = actionManager.colorBoth;
                break;

        }
        dragItem.actionType = typeOfAction;
        dragItem.playerTarget = playerTarget;
        actionManager.dragging = true;
    }
}
