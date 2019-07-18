using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdLevel : RoundManager
{
    public int amountOfWalkers;
    public Transform Floor;
    public GameObject WalkerPrefab;

    List<Walker> walkers;

    private void Start()
    {
        InstantiateWalkers();
    }

    #region RoundStart

    void InstantiateWalkers()
    {
        walkers = new List<Walker>();

        for (int i = 0; i < amountOfWalkers; i++)
        {
            walkers.Add(Instantiate(WalkerPrefab).GetComponent<Walker>());
            walkers[i].gameObject.SetActive(false);
            walkers[i].InitWalker(Floor, new Vector3(
            Random.Range(Floor.position.x - Floor.lossyScale.x, Floor.position.x + Floor.lossyScale.x),
            Floor.position.y,
            Random.Range(Floor.position.z - Floor.lossyScale.z, Floor.position.z + Floor.lossyScale.z)));
        }
    }

    #endregion

    #region Death

    /// <summary>
    /// Code the behavior for the catched player here.
    /// </summary>
    public void OnPlayerCatched()
    {

    }

    public override void OnEnterDeathZone(Transform player)
    {
        PlayerManager.Instance.GetPlayer(player).WarpPlayer(Spawns[0].position);
    }

    #endregion

    #region UTILS

    public Bounds  GetCurrentArea()
    {
        var bounds = new Bounds(Floor.position, Floor.localScale);

        return bounds;
    }

    #endregion
}
