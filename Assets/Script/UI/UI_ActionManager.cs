using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_ActionManager : MonoBehaviour
{
    public List<UI_Actions> actions;
    public float yPos;
    public float xInBetween;
    public float xLeftMostPos;
    public bool setUpActions;

    private void Start()
    {
        for (int i = 0; i < actions.Count; i++)
        {
            var xPos = xLeftMostPos + i * xInBetween;
            actions[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
            actions[i].transform.Find("Action - Text").GetComponent<TextMeshProUGUI>().text = actions[i].actionType.ToString();
        }
    }

    private void OnDrawGizmos()
    {
        if (setUpActions)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                var xPos = xLeftMostPos + i * xInBetween;
                actions[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
                actions[i].name = "Action - " + actions[i].actionType.ToString();
                actions[i].transform.Find("Action - Text").GetComponent<TextMeshProUGUI>().text = actions[i].actionType.ToString();
            }
            setUpActions = false;
        }


        Gizmos.color = Color.red;
        var target = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        Gizmos.DrawSphere(target, .2f);
    }
}
