using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
public class UI_TimeLineManager : MonoBehaviour
{
    public TimeLineHover[] spots;
    public float yPos;
    public float xInBetween;
    public float xLeftMostPos;
    public bool setUpActions;
    public Timeline_Item[] timeline_Items;
    public int currentIndex;
    public bool playerAready, playerBready;
    //public ObservableCollection<Timeline_Item> timeline_Items;
    private void Start()
    {
        SetUpSpots();
        timeline_Items = new Timeline_Item[spots.Length];
    }

    private void Update()
    {
        if(playerAready && playerBready)
        {
            currentIndex++;
            LaunchTimeline();
        }
    }

    public void preLaunchTimeline()
    {
        for (int i = 0; i < spots.Length; i++)
        {
            if (timeline_Items[i] == null)
            {
                return;
            }
        }

        LaunchTimeline();
    }

    public void LaunchTimeline()
    {
        for (int i = 0; i < timeline_Items.Length; i++)
        {
            if(i != currentIndex)
            {
                timeline_Items[i].highlight.enabled = false;
            }
        }
        if(currentIndex < timeline_Items.Length)
        {
            timeline_Items[currentIndex].highlight.enabled = true;
            playerAready = false;
            playerBready = false;
            timeline_Items[currentIndex].invokeTimeLineEvent();


        }
    }


    public void SetUpSpots()
    {
        for (int i = 0; i < spots.Length; i++)
        {
            var xPos = xLeftMostPos + i * xInBetween;
            spots[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
        }
    }

    public void RemoveAction(Timeline_Item item)
    {
        for (int i = 0; i < spots.Length; i++)
        {
            if (timeline_Items[i] == item)
            {
                timeline_Items[i] = null;
                break;
            }
        }
    }

    public void InsertAction(TimeLineHover slot, Timeline_Item itemToAdd)
    {
        int index = -1;
        int lastindex = -1;
        //FindIndex of Item
        for (int i = 0; i < spots.Length; i++)
        {
            if (spots[i] == slot)
            {
                if (timeline_Items[i] == null)
                {
                    timeline_Items[i] = itemToAdd;
                    return;
                }
                else
                {
                    index = i;
                }
            }
            if(timeline_Items[i] == null && index != -1)
            {
                lastindex = i;
                break;
            }
        }
        if (lastindex == -1)
        {
            Destroy(itemToAdd.gameObject);
            return;
        }

        //Move Items to right
        for (int i = lastindex - 1; i >= index ; i--)
        {
            timeline_Items[i + 1] = timeline_Items[i];
            timeline_Items[i+1].rectTransform.anchoredPosition = spots[i+1].rectTransform.anchoredPosition;
        }
        timeline_Items[index] = itemToAdd;
    }

    private void OnDrawGizmos()
    {
        if (setUpActions)
        {
            SetUpSpots();
            setUpActions = false;
        }
    }
}
