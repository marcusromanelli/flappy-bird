using System;
using UnityEngine;

public interface IGameOverWindow : IWindow
{
    public void Setup(Action onClickRestart);
    public void SetMedal(Sprite medal);
    public void SetCurrentScore(int currentScore);
    public void SetMaxScore(int maxScore);
    public void SetNewScore();
}
