using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingPlatformLevel : RoundManager
{
    public float PlatformFadeTime;
    public LayerMask DetectionMask;

    #region ROUND EVENTS

    public override void GenericInit()
    {
        GameManager.Instance.SetupNextLevel("Menu");

        base.GenericInit();

        // We place the players
        GameManager.Instance.RegisterToStartRound(new CustomActions()
        {
            DebugDefinition = "Generic round START",
            action = new System.Action(base.GenericRoundStart),
            weight = 1
        });

        GameManager.Instance.RegisterToStartRound(new CustomActions()
        {
            DebugDefinition = "Init the platforms",
            action = new System.Action(InitPlatforms),
            weight = 2
        });

        GameManager.Instance.RegisterToEndRound(new CustomActions()
        {
            DebugDefinition = "Generic round END",
            action = new System.Action(GenericRoundEnd),
            weight = 1
        });
    }

    #endregion

    #region Round Start

    void InitPlatforms()
    {
        var platforms = FindObjectsOfType<GameProps>();

        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i].Init();
        }
    }

    #endregion
}
