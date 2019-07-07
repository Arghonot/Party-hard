using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public AudioClip LevelOST;

    private void Awake()
    {
        GameManager.Instance.SetCurrentRoundManager(this);
    }
}