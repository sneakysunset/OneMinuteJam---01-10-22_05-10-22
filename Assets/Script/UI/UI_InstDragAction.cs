using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InstDragAction : MonoBehaviour
{
    public GameObject dragItem;
    public void InstatiateDragItem(Vector2 pos, string typeOfAction)
    {
        Instantiate(dragItem)
    }
}
