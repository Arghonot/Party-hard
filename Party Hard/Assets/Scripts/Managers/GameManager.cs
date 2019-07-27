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
    /// The type of the object that embed the action that will be called.
    /// </summary>
    public Type SourceType;
    /// <summary>
    /// Only for debug, contains a brief description of the action.
    /// </summary>
    public string DebugDefinition;
}

public class GameManager : MonoBehaviour
{
    public string currentphase;

    public float TimeBeforeStartRound;
    public float TimeBeforeEndRound;

    #region ACTIONS

    public List<CustomActions> InitRound;
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

        InitRound = new List<CustomActions>();
        StartRound = new List<CustomActions>();
        EndRound = new List<CustomActions>();
    }

    private void Start()
    {
        // First we load the menu
        NextLevel = "Menu";
        UIManager.Instance.Init();
        StartCoroutine(LoadYourAsyncScene());
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

    private void CustomOnLevelWasLoaded()
    {
        print("OnLevelWasLoaded");
        CurrentRoundManager = FindObjectOfType<RoundManager>();

        MusicManager.Instance.SetupMusic(CurrentRoundManager.LevelOST);

        CurrentRoundManager.GenericInit();

        SortActions();
        CallInitRoundActions();
    }

    IEnumerator LoadYourAsyncScene()
    {
        // we don't want pauses when going to the menu
        bool ShallApplyPauses = !(NextLevel == "Menu");

        currentphase = "END ROUND";

        if (ShallApplyPauses)
        {
            yield return new WaitForSeconds(TimeBeforeEndRound);
        }

        ReinitActions();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(NextLevel);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        currentphase = "INIT ROUND";
        // We prepare the level and register the actions
        CustomOnLevelWasLoaded();

        if (ShallApplyPauses)
        {
            yield return new WaitForSeconds(TimeBeforeStartRound);
        }

        currentphase = "START ROUND";
        // we now call the actions
        CallStartRoundActions();

        yield return null;

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
        print("TriggerEndOfRound");
        StartCoroutine(LoadYourAsyncScene());
        //SceneManager.LoadScene(NextLevel);
    }

    #endregion

    #region ACTIONS

    #region CALL

    void CallInitRoundActions()
    {
        for (int i = 0; i < StartRound.Count; i++)
        {
            InitRound[i].action();
        }
    }

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

    #endregion

    void ReinitActions()
    {
        InitRound.RemoveAll(x => x.SourceType == typeof(RoundManager));
        StartRound.RemoveAll(x => x.SourceType == typeof(RoundManager));
        EndRound.RemoveAll(x => x.SourceType == typeof(RoundManager));
        //StartRound = new List<CustomActions>();//.Clear();
        //EndRound = new List<CustomActions>();//.Clear();
    }

    void SortActions()
    {
        InitRound = InitRound.OrderBy(x => x.weight).ToList();
        StartRound = StartRound.OrderBy(x => x.weight).ToList();
        EndRound = EndRound.OrderBy(x => x.weight).ToList();
    }

    #region REGISTER

    public void RegisterToStartRound(CustomActions action)
    {
        StartRound.Add(action);
    }

    public void RegisterToInitRound(CustomActions action)
    {
        InitRound.Add(action);
    }

    public void RegisterToEndRound(CustomActions action)
    {
        EndRound.Add(action);
    }

    #endregion

    #endregion
}
