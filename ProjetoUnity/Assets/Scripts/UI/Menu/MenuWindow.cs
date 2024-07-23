using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuWindow : WindowPanel, IMenuWindow
{
    [SerializeField] private Button playButton;

    private Action onClickPlay;

    private void Awake()
    {
        base.Awake();

        playButton.onClick.AddListener(HandleOnClickPlay);
    }

    public void Setup(Action onClickPlay)
    {
        this.onClickPlay = onClickPlay;
    }

    private void HandleOnClickPlay()
    {
        onClickPlay?.Invoke();
    }
}
