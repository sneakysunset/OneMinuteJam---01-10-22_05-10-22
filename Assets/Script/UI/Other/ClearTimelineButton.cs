using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClearTimelineButton : MonoBehaviour
{
    UnityEvent clickClearTimeline = new UnityEvent();
    UI_TimeLineManager timeLineManager;
    private void Start()
    {
        timeLineManager = FindObjectOfType<UI_TimeLineManager>();
        clickClearTimeline.AddListener(() => timeLineManager.ResetTimeline());
    }

    public void onClick()
    {
        clickClearTimeline.Invoke();
    }
}
