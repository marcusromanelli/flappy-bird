using System.Collections.Generic;

public interface IScoreController
{
    public MedalData GetMedal(int score);
    public int GetHighestScore();
    public List<int> GetHighscores();
    public void Store(int score);
}
