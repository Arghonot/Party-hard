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

    float TimeOnZone = 0f;
    public Zone TriggeredZone;

    private void Start()
    {
        print("MenuManager");
        MusicManager.Instance.SetupMusic(LevelOST);

        GameManager.Instance.SetupNextLevel("PlatformLevel");
    }

    #region RUNTIME

    private void Update()
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

        if (PlayerInQuit.Length > 0)
        {
            if (ManageZone(Zone.Quit))
            {
                Quit();
            }
        }

        if (PlayerInPlay.Length > 0)
        {
            if (ManageZone(Zone.Play))
            {
                StartGame();
            }
        }

        if (PlayerInSettings.Length > 0)
        {
            if (ManageZone(Zone.Settings))
            {
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
}