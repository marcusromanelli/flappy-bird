using System;

public class PauseWindowController : WindowController<IPauseWindow>, IPauseWindowController
{
    private void Awake()
    {
        base.Awake();
    }
    public void Setup(Action onClickMenu, Action onClickContinue)
    {
        window.Setup(onClickMenu, onClickContinue);
    }
}
