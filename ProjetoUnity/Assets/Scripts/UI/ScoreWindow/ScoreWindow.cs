using TMPro;
using UnityEngine;

public class ScoreWindow : WindowPanel, IScoreWindow
{
    [SerializeField] private TMP_Text pointLabel;
    public void SetPoint(int value)
    {
        pointLabel.text = value.ToString();
    }
}
