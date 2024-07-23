using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWindow : WindowPanel, IScoreWindow
{
    [SerializeField] private TMP_Text pointLabel;
    [SerializeField] private Button pauseButton;

    private Action onClickPause;

    private void Awake()
    {
        base.Awake();
        pauseButton.onClick.AddListener(HandleClickPauseButton);
    }
    public void Setup(Action onClickPause)
    {
        this.onClickPause = onClickPause;
    }
    public void SetPoint(int value)
    {
        pointLabel.text = value.ToString();
    }
    private void HandleClickPauseButton()
    {
        onClickPause?.Invoke();
    }
}
