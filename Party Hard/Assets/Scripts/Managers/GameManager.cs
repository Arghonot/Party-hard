using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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


    /// <summary>
    /// This action will trigger the end of the round.
    /// This shall be called only by the round manager.
    /// </summary>
    public void TriggerEndOfRound()
    {

    }
}
