using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public List<int> Scores;

    #region Singleton

    static ScoreManager instance = null;
    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScoreManager>();
            }

            return instance;
        }
    }

    #endregion

    public void UpdateScore(int playerIndex, int pointsToAdd)
    {
        if (Scores == null || Scores.Count == 0)
        {
            Scores = new List<int>();
            for (int i = 0; i < PlayerManager.Instance.GetAmountOfPlayer(); i++)
            {
                Scores.Add(0);
            }
        }

        Scores[playerIndex] += pointsToAdd;
    }
}
