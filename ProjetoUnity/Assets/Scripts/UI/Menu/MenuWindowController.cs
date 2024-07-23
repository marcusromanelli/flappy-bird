using System;

public class MenuWindowController : WindowController<IMenuWindow>, IMenuWindowController
{    
    public void Setup(Action onClickPlay, Action onClickLeaderboards)
    {
        window.Setup(onClickPlay, onClickLeaderboards);
    }

    private void Awake()
    {
        base.Awake();

    }
}
