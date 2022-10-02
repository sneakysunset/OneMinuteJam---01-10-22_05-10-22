using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ResetLevelButton : MonoBehaviour
{
    UnityEvent clickResetLevel = new UnityEvent();
    UI_TimeLineManager timeLineManager;
    private void Start()
    {
        timeLineManager = FindObjectOfType<UI_TimeLineManager>();
        clickResetLevel.AddListener(()=> timeLineManager.ResetLevel());
    }

    public void onClick()
    {
        clickResetLevel.Invoke();
    }
}
