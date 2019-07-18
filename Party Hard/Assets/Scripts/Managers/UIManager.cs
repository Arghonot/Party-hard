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
    public UIContent[] contents;

}
