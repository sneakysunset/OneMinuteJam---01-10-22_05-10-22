using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class RotationManager : MonoBehaviour
{
    public float timer;
    Transform player = null;
    Transform playerA;
    Transform playerB;
    GridTiles[,] grid;
    UI_TimeLineManager timeLineManager;
    public AnimationCurve rotationAnimCurve;
    public float timeForRotation;

    public EventReference Rotate;

    private void Awake()
    {
        playerA = GridGenerator.Instance.player_A;
        playerB = GridGenerator.Instance.player_B;
        timeLineManager = FindObjectOfType<UI_TimeLineManager>();
        grid = GridGenerator.Instance.grid;
    }

    IEnumerator Rotater(float rotateNumber, Transform player, UI_Actions.PlayerTarget playerTarget)
    {
        Quaternion startRot = player.rotation;
        Transform transformN = player;
        transformN.Rotate(0, rotateNumber, 0);
        Quaternion endRot = transformN.rotation;
        float i = 0;
        while (i < 1)
        {
            player.rotation = Quaternion.Lerp(startRot, endRot, rotationAnimCurve.Evaluate(i));

            i += Time.deltaTime * (1 / timeForRotation);
            yield return null;
        }
        player.rotation = endRot;


        yield return null;
        if (playerTarget == UI_Actions.PlayerTarget.Avatar_A)
            timeLineManager.playerAready = true;
        else if (playerTarget == UI_Actions.PlayerTarget.Avatar_B)
            timeLineManager.playerBready = true;

        RuntimeManager.PlayOneShot(Rotate);
    }



    public void RotationActivation(bool right, UI_Actions.PlayerTarget playerTarget)
    {
        switch (playerTarget)
        {
            case UI_Actions.PlayerTarget.Avatar_A:
                player = playerA;
                break;
            case UI_Actions.PlayerTarget.Avatar_B:
                player = playerB;
                break;
            case UI_Actions.PlayerTarget.Both:
                rotateBoth(right, playerTarget);
                return;
        }


        switch (right)
        {
            //right
            case true:
                StartCoroutine(Rotater(90, player,playerTarget));
                break;

            //left
            case false:
                StartCoroutine(Rotater(-90, player, playerTarget));
                break;
        }


        void rotateBoth(bool right, UI_Actions.PlayerTarget playerTarget)
        {
            player = playerA;
            playerTarget = UI_Actions.PlayerTarget.Avatar_A;
            for (int i = 0; i < 2; i++)
            {
                if (i == 1)
                {
                    player = playerB;
                    playerTarget = UI_Actions.PlayerTarget.Avatar_B;

                }
                switch (right)
                {
                    //right
                    case true:
                        StartCoroutine(Rotater(90, player, playerTarget));
                        break;

                    //left
                    case false:
                        StartCoroutine(Rotater(-90, player, playerTarget));
                        break;
                }
            }

        }
    }
}
