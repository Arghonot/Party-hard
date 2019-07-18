using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public float Timer;
    public AudioClip LevelOST;
    public List<Transform> Spawns;
    public string Rules;
    //public string NextLevel;

    private void Awake()
    {
//        GameManager.Instance.SetCurrentRoundManager(this);
    }

    public void GenericInit()
    {
        GameManager.Instance.RegisterToStartRound(new CustomActions()
        {
            DebugDefinition = "Generic round START",
            action = new System.Action(GenericRoundStart),
            weight = 1
        });

        GameManager.Instance.RegisterToEndRound(new CustomActions()
        {
            DebugDefinition = "Generic round END",
            action = new System.Action(GenericRoundEnd),
            weight = 1
        });
    }

    public virtual void GenericRoundStart()
    {
        PlacePlayers();
    }

    public virtual void GenericRoundEnd()
    {
        for (int i = 0; i < PlayerManager.Instance.GetAmountOfAcivatedPlayer(); i++)
        {
            ScoreManager.Instance.UpdateScore(
                i,
                PlayerManager.Instance.isPlayerAlive(i) ? 0 : 1);

            //GameManager.Instance.TriggerEndOfRound();
        }
    }

    protected void PlacePlayers()
    {
        for (int i = 0; i < PlayerManager.Instance.GetAmountOfAcivatedPlayer(); i++)
        {
            PlayerManager.Instance.GetPlayer(i).WarpPlayer(Spawns[i].position);
        }
    }

    public virtual void OnEnterDeathZone(Transform player)
    {
        PlayerManager.Instance.ManagePlayerDeath(player);
    }
}