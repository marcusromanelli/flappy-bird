using DG.Tweening;
using NaughtyAttributes;
using System;
using UnityEngine;

public class ScreenflasherController : WindowController<ScreenflasherWindow>, IScreenflasherController
{

    [Header("Internal")]
    [SerializeField] float test_Time;
    private Tween tween;

    [Button("Test Show")]
    public void TestShow()
    {
        base.Awake();

        Show(Color.white, test_Time);
    }
    public void Show(Color color, float time, Action onFinished = null)
    {
        window.Show();

        if(tween != null && tween.IsPlaying())
        {
            tween.Kill();
            tween = null;
        }

        var sequence = DOTween.Sequence();

        sequence.Append(
        DOTween.To(x => { window.SetOpacity(x); }, 0f, 1f, time / 2f)
        ); 
        
        sequence.Append(
        DOTween.To(x => { window.SetOpacity(x); }, 1f, 0f, time / 2f)
        );

        sequence.Play().OnComplete(() =>
        {
            this.Hide();
            onFinished?.Invoke();
        });
    }
}
