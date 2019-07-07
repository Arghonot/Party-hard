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
    public List<CustomActions> LaunchMenu;
    public List<CustomActions> StartRound; 
    public List<CustomActions> EndRound;
    public List<CustomActions> Entract;

    string MenuLevel;
    string EntractLevel;
    string NextLevel;

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

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// This action will trigger the end of the round.
    /// This shall be called only by the round manager.
    /// </summary>
    public void TriggerEndOfRound()
    {
        print("Calling Play");
        SceneManager.LoadScene(NextLevel);
    }

    public void SetupNextLevel(string nextlevel)
    {
        NextLevel = nextlevel;
    }

    public void Quit()
    {
        print("quit");
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void Settings()
    {
        print("Calling settings");
    }
}
