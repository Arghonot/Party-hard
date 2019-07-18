using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingPlatformLevel : RoundManager
{
    public float PlatformFadeTime;
    public LayerMask DetectionMask;


    private void Start()
    {
        //GameManager.Instance.SetCurrentRoundManager(this);
        //MusicManager.Instance.SetupMusic(LevelOST);

        GameManager.Instance.SetupNextLevel("Menu");
    }

    #region ROUND EVENTS

    public override void GenericRoundStart()
    {
        print("welsh on fading platform");
    }

    #endregion

    public override void OnEnterDeathZone(Transform player)
    {

        base.OnEnterDeathZone(player);

        if (PlayerManager.Instance.AmountOfAlivePlayer() <= 1)
        {
            print("round end");
            GenericRoundEnd();

            GameManager.Instance.TriggerEndOfRound();

        }

        print("On enter death zone " + PlayerManager.Instance.AmountOfAlivePlayer());
    }
}
