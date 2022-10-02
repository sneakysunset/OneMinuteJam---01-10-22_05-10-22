using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public float maxX;
    public float minXTimeline, minXAction;
    public RectTransform timelineBar, actionBar;
    public UI_ActionManager actionManager;
    public UI_TimeLineManager timeLineManager;
    [HideInInspector] public float scrollDataTimeline, scrollDataAction;
    public float scrollSpeed;
    public float maxLength;
    private void Start()
    {
        scrollDataTimeline = maxX;
        scrollDataAction = maxX;

        minXTimeline = maxX - Mathf.Clamp((timeLineManager.spots.Length * timeLineManager.xInBetween - 1920), 30, 3000);
        minXAction = maxX - Mathf.Clamp((actionManager.actions.Length * actionManager.xInBetween - 1920), 30, 3000);
    }

    public void ScrollBarTimeline()
    {
        float xDelta = maxX - minXTimeline;
        scrollDataTimeline = scrollDataTimeline + Input.mouseScrollDelta.y * Time.deltaTime * xDelta * scrollSpeed;
        scrollDataTimeline = Mathf.Clamp(scrollDataTimeline, minXTimeline, maxX);



            Vector3 anchPos = timelineBar.anchoredPosition;
            anchPos.x = scrollDataTimeline;
            timelineBar.anchoredPosition = anchPos;
    }
    
    public void ScrollBarAction()
    {
        float xDelta = maxX - minXAction;
        scrollDataAction = scrollDataAction + Input.mouseScrollDelta.y * Time.deltaTime * xDelta * scrollSpeed;
        scrollDataAction = Mathf.Clamp(scrollDataAction, minXAction, maxX);

            Vector3 anchPos = actionBar.anchoredPosition;
            anchPos.x = scrollDataAction;
            actionBar.anchoredPosition = anchPos;
    }

    private void Update()
    {
        if (actionManager.hover)
        {
            ScrollBarTimeline();
        }
        if (actionManager.hoverAction)
        {
            ScrollBarAction();
        }
    }
}
