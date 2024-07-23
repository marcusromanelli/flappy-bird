using System;

using System.Collections.Generic;
public interface ILeaderboardsWindow : IWindow
{
    public void Setup(List<int> highscores, Action OnClickMenu);
}
