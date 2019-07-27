using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public float Timer;
    public AudioClip LevelOST;
    public List<Transform> Spawns;
    public string Rules;
    //public string NextLevel;
    public Vector3 DesiredCameraOffset;
    public Vector3 DesiredCameraRotation;

    #region Called by GameManager

    public virtual void GenericInit()
    {
        // Camera management
        HandleCameraOffset();
        HandleCameraRotation();

        //GameManager.Instance.RegisterToInitRound(new CustomActions()
        //{
        //    DebugDefinition = "Generic round START",
        //    action = new System.Action(GenericRoundInit),
        //    SourceType = typeof(RoundManager),
        //    weight = 1
        //});

        GameManager.Instance.RegisterToEndRound(new CustomActions()
        {
            DebugDefinition = "Generic round END",
            action = new System.Action(GenericRoundEnd),
            SourceType = typeof(RoundManager),
            weight = 1
        });
    }

    #endregion

    #region ROUND EVENTS

    ///// <summary>
    ///// This is the base implementation for the event "RoundStart"
    ///// </summary>
    //public virtual void GenericRoundInit()
    //{
    //    print("RoundManager.GenericRoundStart");
    //    PlacePlayers();
    //}

    /// <summary>
    /// This is the base implementation for the event "RoundEnd"
    /// </summary>
    public virtual void GenericRoundEnd()
    {
        for (int i = 0; i < PlayerManager.Instance.GetAmountOfAcivatedPlayer(); i++)
        {
            ScoreManager.Instance.UpdateScore(
                i,
                PlayerManager.Instance.isPlayerAlive(i) ? 0 : 1);

            //GameManager.Instance.TriggerEndOfRound();
        }
    }

    #endregion

    #region MISCS

    void HandleCameraOffset()
    {
        if (DesiredCameraOffset == Vector3.zero)
        {
            CameraManager.Instance.RevertToOriginOffset();
        }
        else
        {
            CameraManager.Instance.SetOffset(DesiredCameraOffset);
        }
    }

    void HandleCameraRotation()
    {
        if (DesiredCameraRotation == Vector3.zero)
        {
            CameraManager.Instance.RevertToOriginalRotation();
        }
        else
        {
            CameraManager.Instance.SetRotation(DesiredCameraRotation);
        }
    }

    ///// <summary>
    ///// This function is called in order to place the players on their proper spawn.
    ///// </summary>
    //protected void PlacePlayers()
    //{
    //    for (int i = 0; i < PlayerManager.Instance.GetAmountOfAcivatedPlayer(); i++)
    //    {
    //        PlayerManager.Instance.GetPlayer(i).WarpPlayer(Spawns[i].position);
    //    }
    //}

    /// <summary>
    /// This function is called whenever a player fall from the level's platforms.
    /// </summary>
    /// <param name="player"></param>
    public virtual void BasicOnEnterDeathZone(Transform player)
    {
        PlayerManager.Instance.ManagePlayerDeath(player);
    }

    /// <summary>
    /// This function handle the realBehavior for when a player fall on the death zone.
    /// This implementation is the most likely to be called.
    /// </summary>
    /// <param name="player">The player that fell.</param>
    public virtual void FunctionalOnEnterDeathZone(Transform player)
    {
        BasicOnEnterDeathZone(player);

        if (PlayerManager.Instance.AmountOfAlivePlayer() <= 1)
        {
            GenericRoundEnd();

            GameManager.Instance.TriggerEndOfRound();
        }
    }

    #endregion

}