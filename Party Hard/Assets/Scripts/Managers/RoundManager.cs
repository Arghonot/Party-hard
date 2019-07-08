using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public AudioClip LevelOST;
    public List<Transform> Spawns;

    private void Awake()
    {
        GameManager.Instance.SetCurrentRoundManager(this);
    }

    public virtual void OnEnterDeathZone(Transform player)
    {
        PlayerManager.Instance.ManagePlayerDeath(player);
    }
}