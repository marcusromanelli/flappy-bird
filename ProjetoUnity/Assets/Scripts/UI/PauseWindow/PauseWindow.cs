using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseWindow : WindowPanel, IPauseWindow
{
    [SerializeField] private Button menuButton;
    [SerializeField] private Button continueButton;

    private Action onClickMenu;
    private Action onClickContinue;

    private void Awake()
    {
        base.Awake();
        menuButton.onClick.AddListener(HandleClickMenuButton);
        continueButton.onClick.AddListener(HandleClickContinueButton);
    }
    public void Setup(Action onClickMenu, Action onClickContinue)
    {
        this.onClickMenu = onClickMenu;
        this.onClickContinue = onClickContinue;
    }
    private void HandleClickMenuButton()
    {
        onClickMenu?.Invoke();
    }
    private void HandleClickContinueButton()
    {
        onClickContinue?.Invoke();
    }
}
