using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingMovement : MonoBehaviour
{
/*    [Range(.1f, 10)]
    public float timeForMovement;
    public AnimationCurve movementAnimCurve, firstMAC, lastMAC;
    List<GridTiles> path;
    public Transform player;
    StepAssignement stepA;

    private void Start()
    {
        stepA = GetComponent<StepAssignement>();
    }

    public void Move()
    {
        path = GridGenerator.Instance.highlighter.highlightedTiles;
        StartCoroutine(smoothMovement(player.position, path[path.Count - 1].transform.position, player, path.Count - 1, firstMAC));
    }

    IEnumerator smoothMovement(Vector3 startPos, Vector3 endPos, Transform player, int index, AnimationCurve animC)
    {
        float i = 0;
        GridGenerator.Instance.inAnim = true;
        while (i < 1)
        {
            player.position = Vector3.Lerp(startPos, endPos, animC.Evaluate(i));

            i += Time.deltaTime * (1 / timeForMovement);
            yield return null;
        }
        player.position = endPos;
        GridGenerator.Instance.inAnim = false;

        if (index == 0)
        {
            GridGenerator.Instance.inAnim = false;
            stepA.Initialisation();
            foreach (GridTiles tile in GridGenerator.Instance.highlighter.highlightedTiles)
            {
                tile.highlight = false;
            }
            GridGenerator.Instance.highlighter.highlightedTiles.Clear();
            yield return null;
        }
        else if(index > 0) 
        {
            StartCoroutine(smoothMovement(player.position, path[index - 1].transform.position, player, index - 1, movementAnimCurve));
        }
        else
        {
            StartCoroutine(smoothMovement(player.position, path[index - 1].transform.position, player, index - 1, lastMAC));
        }
    }*/
}
