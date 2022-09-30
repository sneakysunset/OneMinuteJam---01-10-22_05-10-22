using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_InstDragAction : MonoBehaviour
{
    public GameObject dragItemPref;
    public Transform dragItemFolder;
    public void InstatiateDragItem(Vector2 pos, UI_Actions.Action typeOfAction)
    {
        pos.x -= (Screen.width / 2);
        pos.y -= (Screen.height / 2);
        UI_DragItem dragItem = Instantiate(dragItemPref, pos, Quaternion.identity, dragItemFolder).GetComponent<UI_DragItem>();
        dragItem.name = "DragItem - " + typeOfAction.ToString();
        dragItem.transform.Find("Action - Text").GetComponent<TextMeshProUGUI>().text = typeOfAction.ToString();
        dragItem.actionType = typeOfAction;
        dragItem.dragged = true;
    }
}
