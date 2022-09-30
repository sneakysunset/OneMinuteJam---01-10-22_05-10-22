using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [Range(.1f, 10)]
    public float timeForMovement;
    public AnimationCurve movementAnimCurve;
    
    public void Move(Vector3 destination, Transform player)
    {
        StartCoroutine(smoothMovement(player.position, destination, player));

    }

    IEnumerator smoothMovement(Vector3 startPos, Vector3 endPos, Transform player)
    {
        float i = 0;
        GridGenerator.Instance.inAnim = true;
        while(i < 1)
        {
            player.position = Vector3.Lerp(startPos, endPos, movementAnimCurve.Evaluate(i));

            i += Time.deltaTime * (1 / timeForMovement);
            yield return null;
        }
        player.position = endPos;
        GridGenerator.Instance.inAnim = false;

        yield return null;
    }
}
