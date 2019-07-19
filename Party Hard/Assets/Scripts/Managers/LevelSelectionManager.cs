using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is made to choose the next level everytime a level ended.
/// </summary>
public class LevelSelectionManager : MonoBehaviour
{

    #region Singleton

    static LevelSelectionManager instance = null;
    public static LevelSelectionManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LevelSelectionManager>();
            }

            return instance;
        }
    }

    #endregion

}
