using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuWindow : WindowPanel, IMenuWindow
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button leaderboardsButton;

    private Action onClickPlay;
    private Action onClickLeaderboards;

    private void Awake()
    {
        base.Awake();

        playButton.onClick.AddListener(HandleOnClickPlay);
        leaderboardsButton.onClick.AddListener(HandleOnClickLeaderboards);
    }

    public void Setup(Action onClickPlay, Action onClickLeaderboards)
    {
        this.onClickPlay = onClickPlay;
        this.onClickLeaderboards = onClickLeaderboards;
    }

    private void HandleOnClickPlay()
    {
        onClickPlay?.Invoke();
    }
    private void HandleOnClickLeaderboards()
    {
        onClickLeaderboards?.Invoke();
    }
}
