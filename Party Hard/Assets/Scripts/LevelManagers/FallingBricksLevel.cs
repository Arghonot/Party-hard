using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBricksLevel : RoundManager
{    
    /// <summary>
    /// This is the object used as a frame for us to spawn bricks.
    /// </summary>
    public Transform Sky;
    /// <summary>
    /// This is the prefab of the object that will be instantiated.
    /// </summary>
    public GameObject BrickPrefab;

    public float HeightOffset;

    public float MaxSize;
    public float TimeBetweenSpawn;
    float TimeSinceSpawn;

    /// <summary>
    /// This bool is set to true only when the round began.
    /// </summary>
    bool shouldRun = false;

    #region UNITY API

    private void Update()
    {
        if (!shouldRun)
        {
            return;
        }

        TimeSinceSpawn += Time.deltaTime;

        if (TimeSinceSpawn > TimeBetweenSpawn)
        {
            InstantiateBrick();
            TimeSinceSpawn = 0f;
        }

        UpdateSkyHeight();
    }

    #endregion

    #region Runtime

    /// <summary>
    /// We set the sky height at runtime so the level can keep going without the players
    /// reaching a 'roof'.
    /// </summary>
    void UpdateSkyHeight()
    {
        float HighestPlayer = PlayerManager.Instance.GetHighestPlayerPosition();

        if (HighestPlayer + HeightOffset > Sky.position.y)
        {
            Sky.position = new Vector3(
                Sky.position.x,
                HighestPlayer + HeightOffset,
                Sky.position.z);
        }
    }

    void InstantiateBrick()
    {
        float Size = Random.Range(1f, MaxSize);
        var brick = Instantiate(BrickPrefab);

        brick.transform.position = new Vector3(
            Random.Range(Sky.position.x - (Sky.localScale.x / 2f), Sky.position.x + (Sky.localScale.x / 2f)),
            Random.Range(Sky.position.y - (Sky.localScale.y / 2f), Sky.position.y + (Sky.localScale.y / 2f)),
            Random.Range(Sky.position.z - (Sky.localScale.z / 2f), Sky.position.z + (Sky.localScale.z / 2f)));
        brick.transform.localScale = Vector3.one * Size;

        brick.transform.SetParent(transform);
    }

    #endregion

    #region ROUND EVENTS

    public override void GenericInit()
    {
        GameManager.Instance.SetupNextLevel("Menu");

        base.GenericInit();

        // We place the players
        GameManager.Instance.RegisterToStartRound(new CustomActions()
        {
            DebugDefinition = "Generic round START",
            action = new System.Action(base.GenericRoundStart),
            weight = 1
        });


        // We prepare the current round
        GameManager.Instance.RegisterToStartRound(new CustomActions()
        {
            DebugDefinition = "Generic round START",
            action = new System.Action(PrepareLevel),
            weight = 1
        });

        GameManager.Instance.RegisterToEndRound(new CustomActions()
        {
            DebugDefinition = "Generic round END",
            action = new System.Action(GenericRoundEnd),
            weight = 1
        });
    }

    #endregion

    #region Round Start

    void PrepareLevel()
    {
        shouldRun = true;
    }

    #endregion
}
