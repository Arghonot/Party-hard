using System.Linq;
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

    public BasicPlayerBehavior GetPlayer(Transform player)
    {
        return playerBehaviors.Where(x => x.transform == player).First();
    }
}
