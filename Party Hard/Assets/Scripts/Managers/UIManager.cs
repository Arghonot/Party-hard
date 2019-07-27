using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UIContent
{
    public Canvas canvas;
    public TextMesh MainText;
}

public enum CanvasesIndex
{
    Runtime,
    Score,
    Rules 
}

public class UIManager : MonoBehaviour
{
    public UIContentHandler[] contents;

    #region Singleton

    static UIManager instance = null;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }

            return instance;
        }
    }

    #endregion

    public void Init()
    {
        for (int i = 0; i < contents.Length; i++)
        {
            contents[i].Init();
        }
    }
}
