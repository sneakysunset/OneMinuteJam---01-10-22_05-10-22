using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public void Move(Vector3 destination, Transform player)
    {
        player.position = destination;
    }
}
