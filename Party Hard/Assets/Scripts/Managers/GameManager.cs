using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class CustomActions
{
    /// <summary>
    /// The action that is meant to be executed.
    /// </summary>
    public Action action;
    /// <summary>
    /// The action weight, defining it's running order relative to
    /// the other actions on the list containing this instance.
    /// </summary>
    public int weight;
    /// <summary>
    /// Only for debug, contains a brief description of the action.
    /// </summary>
    public string DebugDefinition;
}

public class GameManager : MonoBehaviour
{
    #region ACTIONS

    public List<CustomActions> LaunchMenu;
    public List<CustomActions> StartRound;
    public List<CustomActions> EndRound;
    public List<CustomActions> Entract;

    #endregion

    #region LEVELS

    string MenuLevel;
    string EntractLevel;
    string NextLevel;

    #endregion

    #region Singleton

    static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }

    #endregion

    RoundManager CurrentRoundManager;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public RoundManager GetCurrentRoundManager()
    {
        return CurrentRoundManager;
    }

    public void SetCurrentRoundManager(RoundManager manager)
    {
        CurrentRoundManager = manager;
    }

    private void OnLevelWasLoaded(int level)
    {
        //CurrentRoundManager.GenericRoundStart();
    }

    /// <summary>
    /// This action will trigger the end of the round.
    /// This shall be called only by the round manager.
    /// </summary>
    public void TriggerEndOfRound()
    {
        SceneManager.LoadScene(NextLevel);
    }

    public void SetupNextLevel(string nextlevel)
    {
        NextLevel = nextlevel;
    }

    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void Settings()
    {
        print("Calling settings");
    }
}
