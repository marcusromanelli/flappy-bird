using System;
using UnityEngine;

public interface IGameOverController : IWindow
{
    public void Setup(int currentScore, int maxScore, Sprite medal, Action onClickRestart);
}
