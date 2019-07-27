using System;
using System.Collections.Generic;
using UnityEngine;

public enum Zone
{
    None,
    Play,
    Quit,
    Settings
}

public class MenuManager : RoundManager
{
    public Transform PlayZone;
    public Transform SettingsZone;
    public Transform QuitZone;

    public LayerMask mask;

    public float TimeToTrigger;
    public float ZoneWidth;
    bool didTriggerAZoneAlready = false;

    float TimeOnZone = 0f;
    public Zone TriggeredZone;

    #region ROUND EVENTS

    public override void GenericInit()
    {
        GameManager.Instance.SetupNextLevel("FallingBricksLevel");

        base.GenericInit();

        // We place the players
        GameManager.Instance.RegisterToInitRound(new CustomActions()
        {
            DebugDefinition = "Generic round START",
            action = new System.Action(base.GenericRoundInit),
            SourceType = typeof(RoundManager),
            weight = 1
        });

        // We enable them
        GameManager.Instance.RegisterToStartRound(new CustomActions()
        {
            DebugDefinition = "Enable Playable Players",
            action = new System.Action(EnablePlayablePlayers),
            SourceType = typeof(RoundManager),
            weight = 2
        });

        // we call the generic round end
        GameManager.Instance.RegisterToEndRound(new CustomActions()
        {
            DebugDefinition = "Generic round END",
            action = new System.Action(GenericRoundEnd),
            SourceType = typeof(RoundManager),
            weight = 1
        });
    }

    #endregion

    #region INIT

    void EnablePlayablePlayers()
    {
        base.PlacePlayers();
        PlayerManager.Instance.ActivatePlayingPlayers();
    }

    #endregion

    #region RUNTIME

    private void Update()
    {
        ActivatePlayers();
        ManageZones();
    }

    void ActivatePlayers()
    {
        if (Input.GetButton("J1AButton"))
        {
            PlayerManager.Instance.ActivatePlayer(0);
        }
        if (Input.GetButton("J2AButton"))
        {
            PlayerManager.Instance.ActivatePlayer(1);
        }
        if (Input.GetButton("J3AButton"))
        {
            PlayerManager.Instance.ActivatePlayer(2);
        }
        if (Input.GetButton("J4AButton"))
        {
            PlayerManager.Instance.ActivatePlayer(3);
        }
    }

    #region ZONES

    void ManageZones()
    {
        var PlayerInPlay = Physics.OverlapBox(PlayZone.position, Vector3.one * ZoneWidth, Quaternion.identity, mask);
        var PlayerInQuit = Physics.OverlapBox(QuitZone.position, Vector3.one * ZoneWidth, Quaternion.identity, mask);
        var PlayerInSettings = Physics.OverlapBox(SettingsZone.position, Vector3.one * ZoneWidth, Quaternion.identity, mask);

        if ((PlayerInPlay.Length > 0 && PlayerInQuit.Length > 0) ||
            (PlayerInQuit.Length > 0 && PlayerInSettings.Length > 0) ||
            (PlayerInPlay.Length > 0 && PlayerInSettings.Length > 0))
        {
            return;
        }
        if (didTriggerAZoneAlready)
        {
            return;
        }

        if (PlayerInQuit.Length > 0)
        {
            if (ManageZone(Zone.Quit))
            {
                didTriggerAZoneAlready = true;
                Quit();
            }
        }

        if (PlayerInPlay.Length > 0)
        {
            if (ManageZone(Zone.Play))
            {
                didTriggerAZoneAlready = true;
                StartGame();
            }
        }

        if (PlayerInSettings.Length > 0)
        {
            if (ManageZone(Zone.Settings))
            {
                didTriggerAZoneAlready = true;
                Settings();
            }
        }
    }

    bool ManageZone(Zone CurrentZone)
    {
        if (TriggeredZone == CurrentZone)
        {
            TimeOnZone += Time.deltaTime;

            if (TimeOnZone > TimeToTrigger)
            {
                return true;
            }
        }
        else
        {
            TriggeredZone = CurrentZone;
            TimeOnZone = 0f;
        }

        return false;
    }

    public void StartGame()
    {
        GameManager.Instance.TriggerEndOfRound();
    }

    public void Settings()
    {
        GameManager.Instance.Settings();
    }

    public void Quit()
    {
        GameManager.Instance.Quit();
    }
    #endregion

    #endregion

    #region DEATH

    public override void FunctionalOnEnterDeathZone(Transform player)
    {
        PlayerManager.Instance.GetPlayer(player).WarpPlayer(Spawns[0].position);
    }

    #endregion
}