using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class LaunchTimeline : MonoBehaviour
{
    UnityEvent clickLaunchTimeline = new UnityEvent();
    UI_TimeLineManager timeLineManager;
    Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        timeLineManager = FindObjectOfType<UI_TimeLineManager>();
        clickLaunchTimeline.AddListener(() => timeLineManager.preLaunchTimeline());
    }

    private void Update()
    {
        if (timeLineManager.playin)
            button.interactable = false;
        else
            button.interactable = true;
    }

    public void onClick()
    {
        clickLaunchTimeline.Invoke();
    }
}
