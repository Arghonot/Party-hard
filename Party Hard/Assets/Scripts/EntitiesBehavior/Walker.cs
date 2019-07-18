using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Walker : MonoBehaviour
{
    public float mapBorder;
    NavMeshAgent agent;
    Transform floor;

    Vector3 target = Vector3.negativeInfinity;


    private void Update()
    {
        float dist = agent.remainingDistance;

        if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
        {
            //Arrived.
            // set new target
            SetNewTargetForAgent();
        }
    }

    void SetNewTargetForAgent()
    {
        agent.SetDestination(new Vector3(
            Random.Range(floor.position.x - floor.localScale.x + mapBorder, floor.position.x + floor.localScale.x - mapBorder),
            floor.position.y,
            Random.Range(floor.position.z - floor.localScale.z + mapBorder, floor.position.z + floor.localScale.z - mapBorder)));
    }

    #region INITIALIZATION

    public void InitWalker(Transform floorbound, Vector3 spawnpos)
    {
        print(spawnpos);
        agent = GetComponent<NavMeshAgent>();

       // DisableAgent();
        floor = floorbound;
        transform.position = spawnpos;
        //WarpPlayer(spawnpos);
        gameObject.SetActive(true);

        EnableAgent();

        agent.enabled = true;
        SetNewTargetForAgent();
    }

    #endregion

    #region POSITIONNING

    void WarpPlayer(Vector3 newpos)
    {
        NavMeshHit hit = new NavMeshHit();

        if (NavMesh.SamplePosition(newpos, out hit, 4f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
        }

        agent.Warp(newpos);
    }

    #endregion

    #region MISC

    void DisableAgent()
    {
        agent.isStopped = true;
        agent.updatePosition = false;
        agent.updateRotation = false;
        agent.enabled = false;
    }

    void EnableAgent()
    {
        // We enable it's agent back
        agent.updatePosition = true;
        agent.updateRotation = true;
        agent.enabled = true;
    }

    #endregion
}
