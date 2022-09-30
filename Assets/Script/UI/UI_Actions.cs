using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_Actions : MonoBehaviour
{
    public enum Action { Move, Switch, Rotate, Wait, DropCube, PushCube };
    public Action actionType;
}
