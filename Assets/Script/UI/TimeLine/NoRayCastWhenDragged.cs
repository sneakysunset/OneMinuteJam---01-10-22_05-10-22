using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NoRayCastWhenDragged : MonoBehaviour
{
    UI_ActionManager actionManager;
    Image image;
    void Start()
    {
        actionManager = FindObjectOfType<UI_ActionManager>();
        image = GetComponent<Image>();
    }


    void Update()
    {
        if (actionManager.dragging)
        {
            image.raycastTarget = false;
        }
        else
        {
            image.raycastTarget = true ;

        }
    }
}
