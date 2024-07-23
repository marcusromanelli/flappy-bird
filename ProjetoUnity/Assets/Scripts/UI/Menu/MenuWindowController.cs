using System;

public class MenuWindowController : WindowController<IMenuWindow>, IMenuWindowController
{
    private Action onClickPlay;
    
    public void Setup(Action onClickPlay)
    {
        this.onClickPlay = onClickPlay;
    }

    private void Awake()
    {
        base.Awake();

        window.Setup(HandleOnClickPlay);
    }

    private void HandleOnClickPlay()
    {
        onClickPlay?.Invoke();
    }

}
