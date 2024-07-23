using System;
using System.Collections;
using UnityEngine;

public class GameOverController : WindowController<IGameOverWindow>, IGameOverController
{
    private int highscore;
    public void Setup(int currentScore, int highscore, Sprite medal, Action onClickRestart)
    {
        this.highscore = highscore;

        window.Setup(onClickRestart);

        if (currentScore < highscore)
            window.SetMaxScore(highscore);

        StartCoroutine(CountScore(medal, currentScore));
    }
    private IEnumerator CountScore(Sprite medal, int currentScore)
    {
        var i = 0;
        var increaseBy = Mathf.CeilToInt(currentScore / 10f);

        yield return new WaitForSeconds(1);

        while (i <= currentScore)
        {
            window.SetCurrentScore(i);
            i += increaseBy;
            yield return new WaitForSeconds(0.1f);
        }

        window.SetMedal(medal);

        if (currentScore >= highscore)
        {
            window.SetNewScore();

            window.SetMaxScore(currentScore);
        }
    }
}
