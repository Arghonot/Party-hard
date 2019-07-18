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

    #region UNITY API

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // First we load the menu
        NextLevel = "Menu";
        StartCoroutine(LoadYourAsyncScene());

        StartRound = new List<CustomActions>();
        EndRound = new List<CustomActions>();
    }

    #endregion

    #region GETTERS

    public RoundManager GetCurrentRoundManager()
    {
        return CurrentRoundManager;
    }

    #endregion

    #region SETTERS

    public void SetCurrentRoundManager(RoundManager manager)
    {
        CurrentRoundManager = manager;
    }

    #endregion

    #region LevelManagement

    private void OnLevelWasLoaded()
    {
        CurrentRoundManager = FindObjectOfType<RoundManager>();

        MusicManager.Instance.SetupMusic(CurrentRoundManager.LevelOST);
        ReinitActions();
        CurrentRoundManager.GenericInit();

        SortActions();
        CallStartRoundActions();
    }

    IEnumerator LoadYourAsyncScene()
    {
        print("Load scene");

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(NextLevel);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        OnLevelWasLoaded();
    }

    #endregion

    public void SetupNextLevel(string nextlevel)
    {
        NextLevel = nextlevel;
    }

    #region IN GAME ACTIONS

    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void Settings()
    {
        print("Calling settings");
    }

    /// <summary>
    /// This action will trigger the end of the round.
    /// This shall be called only by the round manager.
    /// </summary>
    public void TriggerEndOfRound()
    {
        CallEndRoundActions();

        StartCoroutine(LoadYourAsyncScene());
        //SceneManager.LoadScene(NextLevel);
    }

    #endregion

    #region ACTIONS

    void CallStartRoundActions()
    {
        for (int i = 0; i < StartRound.Count; i++)
        {
            StartRound[i].action();
        }
    }

    void CallEndRoundActions()
    {
        for (int i = 0; i < EndRound.Count; i++)
        {
            EndRound[i].action();
        }
    }

    void ReinitActions()
    {
        StartRound = new List<CustomActions>();//.Clear();
        EndRound = new List<CustomActions>();//.Clear();
    }

    void SortActions()
    {
        StartRound = StartRound.OrderBy(x => x.weight).ToList();
        EndRound = EndRound.OrderBy(x => x.weight).ToList();
    }

    public void RegisterToStartRound(CustomActions action)
    {
        StartRound.Add(action);
    }

    public void RegisterToEndRound(CustomActions action)
    {
        EndRound.Add(action);
    }

    #endregion
}
