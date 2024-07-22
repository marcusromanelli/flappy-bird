using System;
using System.Collections;
using UnityEngine;

public class GameOverController : WindowController<IGameOverWindow>, IGameOverController
{
    private Action onClickRestart;
    private int highscore;
    public void Setup(int currentScore, int highscore, Sprite medal, Action onClickRestart)
    {
        this.onClickRestart = onClickRestart;
        this.highscore = highscore;

        window.Setup(OnClickRestart);
        window.SetMaxScore(highscore);
        window.SetCurrentScore(currentScore);

        StartCoroutine(CountScore(medal, currentScore));
    }
    private void OnClickRestart()
    {
        onClickRestart?.Invoke();
    }
    private IEnumerator CountScore(Sprite medal, int currentScore)
    {
        var i = 0;
        var increaseBy = Mathf.CeilToInt(currentScore / 5f);
        while(i <= currentScore)
        {
            window.SetCurrentScore(i);
            i+= increaseBy;
            yield return new WaitForSeconds(0.1f);
        }

        window.SetMedal(medal);

        if (currentScore >= highscore)
            window.SetNewScore();
    }
}
