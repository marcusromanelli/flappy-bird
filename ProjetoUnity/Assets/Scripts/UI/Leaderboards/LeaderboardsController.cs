using System;
using System.Collections.Generic;

public class LeaderboardsController : WindowController<ILeaderboardsWindow>, ILeaderboardsController
{
    public void Setup(List<int> highscores, Action OnClickMenu)
    {
        window.Setup(highscores, OnClickMenu);
    }
}
