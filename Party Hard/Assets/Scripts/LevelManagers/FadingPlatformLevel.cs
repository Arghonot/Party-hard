using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingPlatformLevel : RoundManager
{
    public float PlatformFadeTime;
    public LayerMask DetectionMask;


    private void Start()
    {
        MusicManager.Instance.SetupMusic(LevelOST);

        GameManager.Instance.SetupNextLevel("Menu");
    }

    private void Update()
    {
    }
}
