using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeline_Item : MonoBehaviour
{
    [HideInInspector] public UI_Actions.Action actionType;
    [HideInInspector] public UI_Actions.PlayerTarget playerTarget;
    [HideInInspector] public RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public virtual void invokeTimeLineEvent()
    {
        
    }
}
