using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TimeLineManager : MonoBehaviour
{
    public List<TimeLineHover> spots;
    public float yPos;
    public float xInBetween;
    public float xLeftMostPos;
    public bool setUpActions;

    private void Start()
    {
        for (int i = 0; i < spots.Count; i++)
        {
            var xPos = xLeftMostPos + i * xInBetween;
            spots[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
        }
    }

    private void OnDrawGizmos()
    {
        if (setUpActions)
        {
            for (int i = 0; i < spots.Count; i++)
            {
                var xPos = xLeftMostPos + i * xInBetween;
                spots[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
            }
            setUpActions = false;
        }
    }
}
