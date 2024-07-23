using System;

public interface IMenuWindowController : IWindow
{
    public void Setup(Action OnClickPlay, Action onClickLeaderboards);
}
