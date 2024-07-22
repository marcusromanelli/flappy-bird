using System;
using UnityEngine;

public interface IScreenflasherController : IWindow
{
    public void Show(Color color, float time, Action onFinished);
}
