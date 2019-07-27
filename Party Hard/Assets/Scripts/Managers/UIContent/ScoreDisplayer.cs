using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplayer : UIContentHandler
{

    #region UNITY API

    public override void Init()
    {
        print("Score displayer init");

        gameObject.SetActive(false);

        // register to game manager here
        GameManager.Instance.RegisterToEndRound(new CustomActions()
        {
            DebugDefinition = "SCORE DISPLAYER DISPLAY SCORE",
            action = new System.Action(Show),
            SourceType = typeof(UIContent),
            weight = -100
        });
        GameManager.Instance.RegisterToInitRound(new CustomActions()
        {
            DebugDefinition = "SCORE DISPLAYER HIDE SCORE",
            action = new System.Action(Hide),
            SourceType = typeof(UIContent),
            weight = -100
        });
    }

    #endregion

    protected override void Show()
    {
        base.Show();
    }

    protected override void Hide()
    {
        base.Hide();
    }
}
