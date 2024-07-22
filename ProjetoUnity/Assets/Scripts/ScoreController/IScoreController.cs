public interface IScoreController
{
    public MedalData GetMedal(int score);
    public int GetHighestScore();
    public void Store(int score);
}
