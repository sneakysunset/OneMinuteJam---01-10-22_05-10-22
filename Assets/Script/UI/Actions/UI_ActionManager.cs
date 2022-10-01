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
    public TimeLineHover currentHoveredItem;
    public bool dragging, hovering;
    public Color colorAvatarA, colorAvatarB, colorBoth;

    private void Start()
    {
        for (int i = 0; i < actions.Count; i++)
        {
            var xPos = xLeftMostPos + i * xInBetween;
            actions[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
            actions[i].transform.GetComponentInChildren<TextMeshProUGUI>().text = actions[i].actionType.ToString();
    
            switch (actions[i].Avatar)
            {
                case UI_Actions.PlayerTarget.Avatar_A:
                    actions[i].transform.GetComponentInChildren<Image>().color = colorAvatarA;
                    break;

                case UI_Actions.PlayerTarget.Avatar_B:
                    actions[i].transform.GetComponentInChildren<Image>().color = colorAvatarB;
                    break;

                case UI_Actions.PlayerTarget.Both:
                    actions[i].transform.GetComponentInChildren<Image>().color = colorBoth;
                    break;
            }
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
                actions[i].transform.GetComponentInChildren<TextMeshProUGUI>().text = actions[i].actionType.ToString();

                switch (actions[i].Avatar)
                {
                    case UI_Actions.PlayerTarget.Avatar_A:
                        actions[i].transform.GetComponentInChildren<Image>().color = colorAvatarA;
                        break;

                    case UI_Actions.PlayerTarget.Avatar_B:
                        actions[i].transform.GetComponentInChildren<Image>().color = colorAvatarB;
                        break;

                    case UI_Actions.PlayerTarget.Both:
                        actions[i].transform.GetComponentInChildren<Image>().color = colorBoth;
                        break;
                        
                }
            }
            setUpActions = false;
        }
    }
}
