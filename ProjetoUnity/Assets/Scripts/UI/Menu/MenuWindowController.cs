using System;

public class MenuWindowController : WindowController<IMenuWindow>, IMenuWindowController
{    
    public void Setup(Action onClickPlay)
    {
        window.Setup(onClickPlay);
    }

    private void Awake()
    {
        base.Awake();

    }
}
