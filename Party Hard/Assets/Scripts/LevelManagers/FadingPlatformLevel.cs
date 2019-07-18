using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingPlatformLevel : RoundManager
{
    public float PlatformFadeTime;
    public LayerMask DetectionMask;


    private void Start()
    {
        GameManager.Instance.SetCurrentRoundManager(this);
        MusicManager.Instance.SetupMusic(LevelOST);

        GameManager.Instance.SetupNextLevel("Menu");
    }

    public override void OnEnterDeathZone(Transform player)
    {
        base.OnEnterDeathZone(player);

        if (PlayerManager.Instance.AmountOfAlivePlayer() <= 1)
        {
            GenericRoundEnd();
        }
    }
}
