﻿using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<Transform> players;
    public List<bool> ActivatedPlayers;
    
    public Transform playerContainer;

    BasicPlayerBehavior[] playerBehaviors;

    #region Singleton

    static PlayerManager instance = null;
    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerManager>();
            }

            return instance;
        }
    }

    #endregion

    private void Start()
    {
        playerBehaviors = new BasicPlayerBehavior[4];

        for (int i = 0; i < players.Count; i++)
        {
            ActivatedPlayers.Add(false);

            playerBehaviors[i] = players[i].gameObject.GetComponent<BasicPlayerBehavior>();
        }

        // We place the players
        GameManager.Instance.RegisterToInitRound(new CustomActions()
        {
            DebugDefinition = "PLAYER MANAGER START -> place players",
            action = new System.Action(PlacePlayers),
            SourceType = typeof(PlayerManager),
            weight = -100
        });
        
        // We place the players
        GameManager.Instance.RegisterToStartRound(new CustomActions()
        {
            DebugDefinition = "PLAYER MANAGER START -> place players",
            action = new System.Action(ActivatePlayingPlayers),
            SourceType = typeof(PlayerManager),
            weight = -100
        });
    }

    #region Custom Functions

    void PlacePlayers()
    {
        List<Transform> Spawns = GameManager.Instance.GetCurrentRoundManager().Spawns;

        for (int i = 0; i < GetAmountOfAcivatedPlayer(); i++)
        {
            playerBehaviors[i].WarpPlayer(Spawns[i].position);
            playerBehaviors[i].enabled = false;
        }
    }

    public void ActivatePlayingPlayers()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (ActivatedPlayers[i])
            {
                players[i].gameObject.SetActive(true);
                playerBehaviors[i].enabled = true;
                CameraManager.Instance.RegisterPlayer(players[i]);
            }
        }
    }

    public void ActivatePlayer(int index)
    {
        if (index > players.Count || index < 0)
        {
            return;
        }

        players[index].gameObject.SetActive(true);
        ActivatedPlayers[index] = true;
        CameraManager.Instance.RegisterPlayer(players[index]);
    }

    public void ManagePlayerDeath(Transform player)
    {
        if (player.gameObject.activeSelf)
        {
            player.gameObject.SetActive(false);
            CameraManager.Instance.UnRegisterPlayer(player);
        }
    }

    #endregion

    #region GETTERS

    /// <summary>
    /// Return The height of the player that is the Highest.
    /// </summary>
    /// <returns>The highest height for all the players.</returns>
    public float GetHighestPlayerPosition()
    {
        float height = float.NegativeInfinity;

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].transform.position.y > height)
            {
                height = players[i].transform.position.y;
            }
        }

        return height;
    }

    #region BOOL

    public bool isPlayerAlive(int index)
    {
        if (index < 0 || index > players.Count)
            return false;

        return players[index].gameObject.activeSelf;
    }

    #endregion

    #region INT

    public int AmountOfAlivePlayer()
    {
        int amount = 0;

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].gameObject.activeSelf)
                amount++;
        }

        return amount;
    }

    public int GetAmountOfPlayer()
    {
        return players.Count;
    }

    public int GetAmountOfAcivatedPlayer()
    {
        int x = 0;

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].gameObject.activeSelf)
                x++;
        }

        return x;
    }

    #endregion

    #region BasicPlayerBehavior

    public BasicPlayerBehavior GetPlayer(Transform player)
    {
        return playerBehaviors.Where(x => x.transform == player).First();
    }

    public BasicPlayerBehavior GetPlayer(int index)
    {
        if (index < 0 || index > playerBehaviors.Length)
            return null;

        return playerBehaviors[index];
    }

    #endregion

    #endregion
}
