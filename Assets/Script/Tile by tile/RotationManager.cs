using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{

    Transform player = null;
    Transform playerA;
    Transform playerB;
    GridTiles[,] grid;
    public UI_TimeLineManager timeLineManager;
    public AnimationCurve rotationAnimCurve;
    public float timeForRotation;

    private void Awake()
    {
        playerA = GridGenerator.Instance.player_A;
        playerB = GridGenerator.Instance.player_B;

        grid = GridGenerator.Instance.grid;
    }

    IEnumerator Rotater(float rotateNumber, Transform player, bool both)
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


        yield return new WaitForSeconds(1);
        if (!both)
        {
            timeLineManager.currentIndex++;
            timeLineManager.LaunchTimeline();
        }
    }

    void Rotate(float rotateNumber)
    {
        player.Rotate(0, rotateNumber, 0);
        timeLineManager.currentIndex++;
        timeLineManager.LaunchTimeline();
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
                rotateBoth(right);
                return;
        }


        switch (right)
        {
            //right
            case true:
                StartCoroutine(Rotater(90, player,false));
                break;

            //left
            case false:
                StartCoroutine(Rotater(-90, player, false));
                break;
        }


        void rotateBoth(bool right)
        {
            player = playerA;
            bool both = true;
            for (int i = 0; i < 2; i++)
            {
                if (i == 1)
                {
                    player = playerB;
                    both = false;
                }
                switch (right)
                {
                    //right
                    case true:
                        StartCoroutine(Rotater(90, player, both));
                        break;

                    //left
                    case false:
                        StartCoroutine(Rotater(-90, player, both));
                        break;
                }
            }
            timeLineManager.currentIndex++;
            timeLineManager.LaunchTimeline();
        }
    }
}
