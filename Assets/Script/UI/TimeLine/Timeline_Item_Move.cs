using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Timeline_Item_Move : Timeline_Item
{
    UnityEvent<int> MoveEvent;
/*    public void invokeTimeLineEvent()
    {
        //MoveEvent.AddListener(FindObjectOfType<MovementEvents>().MovementActivation());
        base.invokeTimeLineEvent();
    }*/
}
