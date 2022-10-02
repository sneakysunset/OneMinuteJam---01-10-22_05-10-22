using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct action
{
    public UI_Actions.Action actionType;
    public UI_Actions.PlayerTarget playerTarget;
}

public class UI_ActionManager : MonoBehaviour
{
    public List<action> actionss;

    public UI_Actions[] actions;
    public float yPos;
    public float xInBetween;
    public float xLeftMostPos;
    public bool SetUpActions;

    public UI_DragItem currentDraggedItem;
    public TimeLineHover currentHoveredItem;
    public bool dragging, hovering, hover, hoverAction;
    public Color colorAvatarA, colorAvatarB, colorBoth;
    public GameObject action;
    public RectTransform actionFolder;


    private void Start()
    {

            foreach (UI_Actions item in actions)
            {
                if (item != null)
                    Destroy(item?.gameObject);
            }

        actions = new UI_Actions[actionss.Count];
        for (int i = 0; i < actionss.Count; i++)
        {
            var xPos = xLeftMostPos + i * xInBetween;
            GameObject inst = Instantiate(action, actionFolder);
            actions[i] = inst.GetComponent<UI_Actions>();
            actions[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
            actions[i].actionType = actionss[i].actionType;
            actions[i].Avatar = actionss[i].playerTarget;
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
    }

/*
    private void OnDrawGizmos()
    {
        if (SetUpActions && !Application.isPlaying)
        {
            print(1);
            foreach (UI_Actions item in actions)
            {
                if (item != null)
                    DestroyImmediate(item.gameObject);
            }

            actions = new UI_Actions[actionss.Count];
            for (int i = 0; i < actionss.Count; i++)
            {
                var xPos = xLeftMostPos + i * xInBetween;
                GameObject inst = Instantiate(action, actionFolder);
                actions[i] = inst.GetComponent<UI_Actions>();
                actions[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
                actions[i].actionType = actionss[i].actionType;
                actions[i].Avatar = actionss[i].playerTarget;
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
            SetUpActions = false;
        }
    }*/
}
