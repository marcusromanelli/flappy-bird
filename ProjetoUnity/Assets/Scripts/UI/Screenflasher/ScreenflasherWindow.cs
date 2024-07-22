using UnityEngine;
using UnityEngine.UI;

public class ScreenflasherWindow : WindowPanel, IScreenflasherWindow
{
    [SerializeField] Image background;

    public void SetOpacity(float opacity)
    {
        canvasGroup.alpha = opacity;
    }

    public void Setup(Color color)
    {
        background.color = color;
    }
}
