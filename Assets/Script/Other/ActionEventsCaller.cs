using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class ActionEventsCaller : MonoBehaviour
{
    public UnityEvent<int, UI_Actions.PlayerTarget, Vector3> Move;
    public UnityEvent<bool, UI_Actions.PlayerTarget> Rotate;


    public void MoveEventCaller(int direction, UI_Actions.PlayerTarget playerTarget)
    {
        Move.Invoke(direction, playerTarget, Vector3.zero);
    }

    public void RotateEventCaller(bool right, UI_Actions.PlayerTarget playerTarget)
    {
        Rotate.Invoke(right, playerTarget);
    }
}
