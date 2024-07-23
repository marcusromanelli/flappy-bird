using System;

public interface IScoreWindowController : IWindow
{
    public void Setup(Action onClickPauseButton);
    public void SetPoint(int value);
}
