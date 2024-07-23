using System;

public interface IScoreWindow : IWindow
{
    public void Setup(Action OnClickPause);
    public void SetPoint(int value);
}
