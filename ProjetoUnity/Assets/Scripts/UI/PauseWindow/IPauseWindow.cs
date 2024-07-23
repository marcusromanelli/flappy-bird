using System;

public interface IPauseWindow : IWindow
{
    public void Setup(Action onClickMenu, Action onClickContinue);
}
