using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdLevel : RoundManager
{
    public float mapBorder;
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
            Random.Range(Floor.position.x - (Floor.lossyScale.x / 2f), Floor.position.x + (Floor.lossyScale.x / 2f)),
            Floor.position.y,
            Random.Range(Floor.position.z - (Floor.lossyScale.z / 2f), Floor.position.z + (Floor.lossyScale.z / 2f))),
            mapBorder);
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

    public override void BasicOnEnterDeathZone(Transform player)
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
