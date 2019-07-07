using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    AudioSource source;

    #region Singleton

    static MusicManager instance = null;
    public static MusicManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MusicManager>();
            }

            return instance;
        }
    }

    #endregion

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void SetupMusic(AudioClip levelOST)
    {
        source.Stop();
        source.clip = levelOST;
        source.Play();
    }

    public void debug()
    {
        print("dsfqjh");
    }
}
