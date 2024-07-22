using UnityEngine;

public class ScoreController : MonoBehaviour, IScoreController
{
    [SerializeField] MedalData[] medals;


    private const string storeKey = "HighestScore";
    private int highestScore;

    private void Awake()
    {
        highestScore = PlayerPrefs.GetInt(storeKey,0);
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
        return highestScore;
    }

    public void Store(int score)
    {
        if (score > highestScore) 
            highestScore = score;

        PlayerPrefs.SetInt(storeKey, highestScore);
    }
}
