using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContentHandler : MonoBehaviour
{
    public Canvas canvas;

    public virtual void Init()
    {

    }

    protected virtual void Show()
    {
        if (GameManager.Instance.CurrentLevel != "Menu")
        {
            gameObject.SetActive(true);
        }
    }

    protected virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
