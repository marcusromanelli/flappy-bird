using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardsWindow : WindowPanel, ILeaderboardsWindow
{
    [SerializeField] private TMP_Text[] scoresLabel;
    
    [SerializeField] private Button menuButton;

    private Action OnClickMenu;

    private void Awake()
    {
        base.Awake();
        menuButton.onClick.AddListener(HandleClickMenuButton);
    }
    public void Setup(List<int> scores, Action OnClickMenu)
    {
        ResetAllScores();

        if(scores != null)
            for(int i = 0; i < scores.Count; i++)
            {
                scoresLabel[i].text = scores[i].ToString();
            }

        this.OnClickMenu = OnClickMenu;
    }

    private void HandleClickMenuButton()
    {
        OnClickMenu?.Invoke();
    }

    private void ResetAllScores()
    {
        foreach (var score in scoresLabel)
            score.text = "0";
    }
}
