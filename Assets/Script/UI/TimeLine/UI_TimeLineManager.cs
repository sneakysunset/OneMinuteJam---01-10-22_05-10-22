using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using FMODUnity;
using UnityEngine.Events;

public class UI_TimeLineManager : MonoBehaviour
{
    public float timerAfterActionEnd;
    public TimeLineHover[] spots;
    public float yPos;
    public float xInBetween;
    public float xLeftMostPos;
    public bool setUpActions;
    public Timeline_Item[] timeline_Items;
    public int currentIndex;
    public bool playerAready, playerBready;
    public bool endA, endB;
    public bool playin;
    public bool stunnedA, stunnedB;
    public AnimationCurve InsertAnimationCurve;
    public float insertAnimationLength;
    public UnityEvent endLevel;
    public GameObject spot;
    public RectTransform spotFolder;
    public enum scenes { };
    //public ObservableCollection<Timeline_Item> timeline_Items;
    private void Start()
    {
        SetUpSpots();
        timeline_Items = new Timeline_Item[spots.Length];
    }

    IEnumerator timerForNextAction()
    {
        yield return new WaitForSeconds(timerAfterActionEnd);
        currentIndex++;
        LaunchTimeline();
    }

    bool flag;

    private void Update()
    {
        if(playerAready && playerBready)
        {
            playerAready = false;
            playerBready = false;
            StartCoroutine(timerForNextAction());
        }

        if(endA && endB && !flag)
        {
            flag = true;
            RuntimeManager.PlayOneShot("event:/Game/Simulation Phase/Win");
            endLevel.Invoke();
            print("Success");
        }
    }

    public void ResetLevel()
    {
        playin = false;
        if(currentIndex != timeline_Items.Length)
        {
            currentIndex = 0;
            StartCoroutine(WaitForEndOfMovement());
        }
        else
        {
            currentIndex = 0;
            GridGenerator.Instance.player_A.position = GridGenerator.Instance.playerOGPosA;
            GridGenerator.Instance.player_B.position = GridGenerator.Instance.playerOGPosB;
            GridGenerator.Instance.player_A.rotation = GridGenerator.Instance.playerOGRotA;
            GridGenerator.Instance.player_B.rotation = GridGenerator.Instance.playerOGRotB;
            stunnedA = false;
            stunnedB = false;
            foreach(GridTiles tile in GridGenerator.Instance.grid)
            {
                tile.transform.position = new Vector3(tile.transform.position.x, tile.originalY, tile.transform.position.z);
                stunnedA = false;
                stunnedB = false;
            }
            endA = false;
            endB = false;
        }
    }

    IEnumerator WaitForEndOfMovement()
    {
        yield return new WaitUntil(() => currentIndex == 0);
        GridGenerator.Instance.player_A.position = GridGenerator.Instance.playerOGPosA;
        GridGenerator.Instance.player_B.position = GridGenerator.Instance.playerOGPosB;
        GridGenerator.Instance.player_A.rotation = GridGenerator.Instance.playerOGRotA;
        GridGenerator.Instance.player_B.rotation = GridGenerator.Instance.playerOGRotB;
        foreach (GridTiles tile in GridGenerator.Instance.grid)
        {
            tile.transform.position = new Vector3(tile.transform.position.x, tile.originalY, tile.transform.position.z);
            stunnedA = false;
            stunnedB = false;
        }
        endA = false;
        endB = false;
    }

    public void ResetTimeline()
    {
        foreach(Timeline_Item item in timeline_Items)
        {
            Destroy(item?.gameObject);
        }
    }

    public void preLaunchTimeline()
    {
        currentIndex = 0;
        for (int i = 0; i < spots.Length; i++)
        {
/*            if (timeline_Items[i] == null)
            {
                return;
            }*/
        }
        playin = true;
        LaunchTimeline();
    }

    public void LaunchTimeline()
    {
        for (int i = 0; i < timeline_Items.Length; i++)
        {
            if(i != currentIndex && timeline_Items[i] != null)
            {
                timeline_Items[i].highlight.enabled = false;
            }


        }
        if(currentIndex < timeline_Items.Length &&  timeline_Items[currentIndex] != null)
        {
            timeline_Items[currentIndex].highlight.enabled = true;
            playerAready = false;
            playerBready = false;
            timeline_Items[currentIndex].invokeTimeLineEvent();
        }
        else
        {
            currentIndex = timeline_Items.Length;
            //playin = false;
        }
    }


    public void SetUpSpots()
    {
        for (int i = 0; i < spots.Length; i++)
        {
            var xPos = xLeftMostPos + i * xInBetween;
            if(spots[i] == null)
            {
                RectTransform inst = Instantiate(spot, spotFolder).GetComponent<RectTransform>();
                inst.anchoredPosition = new Vector2(xPos, yPos);
                spots[i] = inst.GetComponent<TimeLineHover>();
            }
            else
            {
            spots[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);

            }
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
            StartCoroutine(InsertAnim(timeline_Items[i + 1].rectTransform.anchoredPosition, spots[i + 1].rectTransform.anchoredPosition, timeline_Items[i + 1].rectTransform));
            //timeline_Items[i+1].rectTransform.anchoredPosition = spots[i+1].rectTransform.anchoredPosition;
        }
        timeline_Items[index] = itemToAdd;
    }

    IEnumerator InsertAnim(Vector2 startPos, Vector2 endPos, RectTransform rTransform )
    {
        float i = 0;
        while(i < 1)
        {
            i += Time.deltaTime * (1/insertAnimationLength);

            rTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, InsertAnimationCurve.Evaluate(i));
            yield return null;
        }
        rTransform.anchoredPosition = endPos;
        yield return null;
    }

/*    private void OnDrawGizmos()
    {
        if (setUpActions)
        {
            SetUpSpots();
            setUpActions = false;
        }
    }*/
}
