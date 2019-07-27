using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RuleDisplayer : UIContentHandler
{
    public TextMeshProUGUI rule;

    #region UNITY API

    public override void Init()
    {
        print("rules displayer INIT");
        gameObject.SetActive(false);

        // register to game manager here
        GameManager.Instance.RegisterToInitRound(new CustomActions()
        {
            DebugDefinition = "UI RULE DISPLAYER INIT ROUND",
            action = new System.Action(Show),
            SourceType = typeof(UIContent),
            weight = -100
        });
        GameManager.Instance.RegisterToStartRound(new CustomActions()
        {
            DebugDefinition = "UI RULE DISPLAYER START ROUND",
            action = new System.Action(Hide),
            SourceType = typeof(UIContent),
            weight = -100
        });
    }

    #endregion

    protected override void Show()
    {
        print("Rules show");
        rule.text = GameManager.Instance.GetCurrentRoundManager().Rules;

        base.Show();
    }

    protected override void Hide()
    {
        base.Hide();
    }
}
