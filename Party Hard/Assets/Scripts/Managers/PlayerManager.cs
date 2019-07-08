using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<Transform> players;

    public Transform playerContainer;

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

    public void ActivatePlayer(int index)
    {
        if (index > players.Count || index < 0)
        {
            return;
        }

        players[index].gameObject.SetActive(true);
        CameraManager.Instance.RegisterPlayer(players[index]);
    }
}
