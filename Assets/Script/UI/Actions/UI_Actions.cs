using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_Actions : MonoBehaviour
{
    public enum Action { MoveForward, MoveRight, MoveLeft, MoveDown, RotateRight, RotateLeft };
    public enum PlayerTarget
    {
        Avatar_A,
        Avatar_B,
        Both
    }

    public PlayerTarget Avatar;
    public Action actionType;

    public Image rootImage, childImage;
}
