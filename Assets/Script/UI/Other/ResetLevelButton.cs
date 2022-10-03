using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class ResetLevelButton : MonoBehaviour
{
    UnityEvent clickResetLevel = new UnityEvent();
    UI_TimeLineManager timeLineManager;
    Button button;
    private void Start()
    {
        timeLineManager = FindObjectOfType<UI_TimeLineManager>();
        clickResetLevel.AddListener(()=> timeLineManager.ResetLevel());
        button = GetComponent<Button>();
    }

    private void Update()
    {
        if (timeLineManager.currentIndex == 0)
            button.interactable = false;
        else
            button.interactable = true;
    }

    public void onClick()
    {
        clickResetLevel.Invoke();
    }
}
