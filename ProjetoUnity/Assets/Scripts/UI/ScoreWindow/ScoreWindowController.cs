using System;

public class ScoreWindowController : WindowController<IScoreWindow>, IScoreWindowController
{
    private void Awake()
    {
        base.Awake();

    }
    public void SetPoint(int value)
    {
        window.SetPoint(value);
    }

    public void Setup(Action onClickPause)
    {
        window.Setup(onClickPause);
    }
}
