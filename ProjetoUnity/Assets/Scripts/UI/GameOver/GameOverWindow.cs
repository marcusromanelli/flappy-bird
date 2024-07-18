using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOverWindow : WindowPanel, IGameOverWindow
{
    [SerializeField] private TMP_Text currentScoreLabel;
    [SerializeField] private TMP_Text maxScoreLabel;
    [SerializeField] private Image medal;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject newScoreMarker;
    [SerializeField] private Animation gameOverAnimation;
    [SerializeField] private Animation popupAnimation;

    private Action onClickedRestart;
    private void Awake()
    {
        base.Awake();

        restartButton.onClick.AddListener(OnClickedRestart);
    }
    private void OnClickedRestart()
    {
        onClickedRestart?.Invoke();
    }
    public void Setup(Action onClickedRestart)
    {
        this.onClickedRestart = onClickedRestart;
    }
    public override void Show()
    {
        base.Show();

        gameOverAnimation.Play("Show");
        popupAnimation.Play("Show");
    }
    public override void Hide()
    {
        base.Hide();

        popupAnimation.Play("Hide");
    }

    public void SetMedal(Sprite medalSprite)
    {
        if (medalSprite == null)
            return;

        medal.sprite = medalSprite;
    }
    public void SetCurrentScore(int currentScore)
    {
        currentScoreLabel.text = currentScore.ToString();
    }
    public void SetMaxScore(int maxScore)
    {
        maxScoreLabel.text = maxScore.ToString();
    }
    public void SetNewScore()
    {
        newScoreMarker.SetActive(true);
    }
}
