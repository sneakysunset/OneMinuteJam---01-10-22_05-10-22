using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class MovementManager : MonoBehaviour
{
    [Range(.1f, 10)]
    public float timeForMovement;
    public AnimationCurve movementAnimCurve, cancelledMovementAnimCurve;
    public UI_TimeLineManager timeLineManager;


    private void Awake()
    {
        timeLineManager = FindObjectOfType<UI_TimeLineManager>();
    }

    public void Move(Vector3 destination, Transform player, UI_Actions.PlayerTarget playerTarget)
    {
        StartCoroutine(smoothMovement(player.position, destination, player, playerTarget));

    }

    public void CancelledMovement(Vector3 destination, Transform player, UI_Actions.PlayerTarget playerTarget, bool recoil)
    {
        StartCoroutine(smoothCancelledMovement(player.position, destination, player, playerTarget,  recoil));
    }

    IEnumerator smoothMovement(Vector3 startPos, Vector3 endPos, Transform player, UI_Actions.PlayerTarget playerTarget)
    {
        float i = 0;
        while (i < 1)
        {
            player.position = Vector3.Lerp(startPos, endPos, movementAnimCurve.Evaluate(i));

            i += Time.deltaTime * (1 / .35f);
            yield return null;
        }
        player.position = endPos;
            yield return null;

        GridTiles previousTile = GridGenerator.Instance.grid[Mathf.RoundToInt(startPos.x), Mathf.RoundToInt(startPos.z)];

        if (previousTile.tileType == GridTiles.TileVariant.Plaque_De_Pression)
        {
            Transform porteT = GridGenerator.Instance.grid[Mathf.RoundToInt(previousTile.plaqueDePressionCoordinates.x), Mathf.RoundToInt(previousTile.plaqueDePressionCoordinates.y)].transform;
            int startY = Mathf.RoundToInt(porteT.position.y);
            int endY = startY - previousTile.porteHeightChange;
            StartCoroutine(leavePlaque(startY, endY, porteT, playerTarget, previousTile, player, startPos));
        }
        else
        {
            GridGenerator.Instance.grid[Mathf.RoundToInt(player.position.x), Mathf.RoundToInt(player.position.z)].TileEffect(playerTarget, startPos);
        }

    }

    IEnumerator smoothCancelledMovement(Vector3 startPos, Vector3 endPos, Transform player, UI_Actions.PlayerTarget playerTarget, bool recoil)
    {

        float i = 0;
        while (i < 1)
        {
            player.position = Vector3.Lerp(startPos, endPos, cancelledMovementAnimCurve.Evaluate(i));

            i += Time.deltaTime * (1 / .35f);
            yield return null;
        }
        player.position = startPos;
        yield return null;


        GridTiles previousTile = GridGenerator.Instance.grid[Mathf.RoundToInt(startPos.x), Mathf.RoundToInt(startPos.z)];
        if (recoil)
        {
            GridGenerator.Instance.grid[Mathf.RoundToInt(player.position.x), Mathf.RoundToInt(player.position.z)].TileEffect(playerTarget, endPos);
        }
        else
        {

            if(playerTarget == UI_Actions.PlayerTarget.Avatar_A)
            {
                timeLineManager.playerAready = true;
            }
            else
            {
                timeLineManager.playerBready = true;   
            }
        }
    }





    IEnumerator leavePlaque(int startY, int endY, Transform porte, UI_Actions.PlayerTarget playerTarget, GridTiles previousTile, Transform player, Vector3 startPos)
    {
        RuntimeManager.PlayOneShot("event:/Elements/Pressure plate leave");
        float i = 0;
        while (i < 1)
        {
            i += Time.deltaTime * (1 / previousTile.animSpeed);

            porte.position = new Vector3(porte.position.x, Mathf.Lerp(startY, endY, previousTile.animCurve.Evaluate(i)), porte.position.z);
            yield return null;
        }
        porte.position = new Vector3(porte.position.x, endY, porte.position.z);

        yield return null;
        GridGenerator.Instance.grid[Mathf.RoundToInt(player.position.x), Mathf.RoundToInt(player.position.z)].TileEffect(playerTarget, startPos);

    }
}
