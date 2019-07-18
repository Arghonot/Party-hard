using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public float Timer;
    public AudioClip LevelOST;
    public List<Transform> Spawns;
    public string Rules;

    private void Awake()
    {
//        GameManager.Instance.SetCurrentRoundManager(this);
    }

    public void GenericRoundStart()
    {
        PlacePlayers();
    }

    public void GenericRoundEnd()
    {
        for (int i = 0; i < PlayerManager.Instance.GetAmountOfPlayer(); i++)
        {
            ScoreManager.Instance.UpdateScore(
                i,
                PlayerManager.Instance.isPlayerAlive(i) ? 0 : 1);

            GameManager.Instance.TriggerEndOfRound();
        }
    }

    protected void PlacePlayers()
    {
        for (int i = 0; i < PlayerManager.Instance.GetAmountOfPlayer(); i++)
        {
            PlayerManager.Instance.GetPlayer(i).WarpPlayer(Spawns[i].position);
        }
    }

    public virtual void OnEnterDeathZone(Transform player)
    {
        PlayerManager.Instance.ManagePlayerDeath(player);
    }
}