using System;

public interface IPauseWindowController : IWindow
{
    public void Setup(Action onClickMenu, Action onClickContinue);
}
