using System;
using System.Collections.Generic;

public interface ILeaderboardsController : IWindow
{
    public void Setup(List<int> highscores, Action OnClickMenu);
}
