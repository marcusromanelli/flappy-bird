using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreController : MonoBehaviour, IScoreController
{
    [SerializeField] MedalData[] medals;

    private const int maxNumberHighscores = 3;

    private IScoreStorage scoreStorage;
    private List<int> highestScores;

    private void Awake()
    {
        scoreStorage = GetComponent<IScoreStorage>();

        LoadStoredScores();
    }
    public MedalData GetMedal(int score)
    {
        MedalData foundMedal = null;

        foreach(var medal in medals)
        {
            if (score >= medal.requiredPoints)
                foundMedal = medal;
        }

        return foundMedal;
    }

    public int GetHighestScore()
    {
        return highestScores.Count == 0 ? 0 : highestScores[0];
    }

    public void Store(int score)
    {
        highestScores.Add(score);
        highestScores = highestScores.OrderByDescending(a => a).Take(3).ToList();

        scoreStorage.Save(highestScores);
    }

    private void LoadStoredScores()
    {
        highestScores = scoreStorage.Load();

        if (highestScores == null)
            highestScores = new List<int>();
    }

    public List<int> GetHighscores()
    {
        return highestScores;
    }

}
