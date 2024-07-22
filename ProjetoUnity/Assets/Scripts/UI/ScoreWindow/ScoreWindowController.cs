public class ScoreWindowController : WindowController<IScoreWindow>, IScoreWindowController
{
    public void SetPoint(int value)
    {
        window.SetPoint(value);
    }
}
