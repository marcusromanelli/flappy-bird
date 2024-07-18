using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameOverController : WindowController<IGameOverWindow>, IGameOverController
{
    private Action onClickRestart;

    public void Setup(int currentScore, int maxScore, Sprite medal, Action onClickRestart)
    {
        this.onClickRestart = onClickRestart;

        window.Setup(OnClickRestart);
        window.SetMaxScore(maxScore);
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
        while(i <= currentScore)
        {
            window.SetCurrentScore(i);
            i++;
            yield return null;
        }

        window.SetMedal(medal);
    }
}
