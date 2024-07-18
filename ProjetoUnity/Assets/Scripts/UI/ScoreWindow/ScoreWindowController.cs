public class ScoreWindowController : WindowController<IScoreWindowWindow>, IScoreWindowController
{
    public void SetPoint(int value)
    {
        window.SetPoint(value);
    }
}
