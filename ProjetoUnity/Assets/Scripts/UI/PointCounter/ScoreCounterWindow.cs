using TMPro;
using UnityEngine;

public class ScoreCounterWindow : WindowPanel, IScoreCounterWindow
{
    [SerializeField] private TMP_Text pointLabel;
    public void SetPoint(int value)
    {
        pointLabel.text = value.ToString();
    }
}
