public class ScoreCounterController : WindowController<IScoreCounterWindow>, IScoreCounterController
{
    public void SetPoint(int value)
    {
        window.SetPoint(value);
    }
}
