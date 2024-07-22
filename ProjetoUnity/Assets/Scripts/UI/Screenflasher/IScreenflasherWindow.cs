using UnityEngine;

public interface IScreenflasherWindow : IWindow
{
    public void Setup(Color color);
    public void SetOpacity(float opacity);
}
