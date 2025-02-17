using AYellowpaper;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour, IGameController
{    
    enum GameState
    {
        Menu,
        Tutorial,
        Playing,
        Death
    }
    [RequireInterface(typeof(IStage))]
    [SerializeField] private MonoBehaviour stageObject;
    [SerializeField] private StageData[] stagePrefabs;
    [RequireInterface(typeof(IPlayer))]
    [SerializeField] private MonoBehaviour playerObject;
    [RequireInterface(typeof(IScoreWindowController))]
    [SerializeField] private MonoBehaviour pointCounterControllerObject;
    [RequireInterface(typeof(IGameOverController))]
    [SerializeField] private MonoBehaviour gameOverControllerObject;
    [RequireInterface(typeof(IScoreController))]
    [SerializeField] private MonoBehaviour scoreControllerObject;
    [RequireInterface(typeof(IScreenflasherController))]
    [SerializeField] private MonoBehaviour screenflasherObject;
    [RequireInterface(typeof(IStartWindowController))]
    [SerializeField] private MonoBehaviour startObject;
    [RequireInterface(typeof(ISoundController))]
    [SerializeField] private MonoBehaviour soundObject;
    [RequireInterface(typeof(IMenuWindowController))]
    [SerializeField] private MonoBehaviour menuObject;
    [RequireInterface(typeof(IPauseWindowController))]
    [SerializeField] private MonoBehaviour pauseObject;
    [RequireInterface(typeof(ILeaderboardsController))]
    [SerializeField] private MonoBehaviour leaderboardObject;
    [SerializeField] private GameObject startButton;

#if UNITY_EDITOR
    [Header("Debug")]
    [SerializeField] private bool OverrideScore;
    [SerializeField] private int OverridenScore = 150;
#endif


    private IStage stage;
    private IPlayer player;
    private IScoreWindowController scoreWindowController;
    private IGameOverController gameOverController;
    private IScoreController scoreController;
    private IScreenflasherController screenflasherController;
    private IStartWindowController startController;
    private ISoundController soundController;
    private IMenuWindowController menuController;
    private IPauseWindowController pauseController;
    private ILeaderboardsController leaderboardsController;
    private int currentScore;
    private GameState gameState;
    private void Awake()
    {
        player = (IPlayer)playerObject;
        scoreWindowController = (IScoreWindowController)pointCounterControllerObject;
        gameOverController = (IGameOverController)gameOverControllerObject;
        scoreController = (IScoreController)scoreControllerObject;
        screenflasherController = (IScreenflasherController)screenflasherObject;
        startController = (IStartWindowController)startObject;
        soundController = (ISoundController)soundObject;
        menuController = (IMenuWindowController)menuObject;
        pauseController = (IPauseWindowController)pauseObject;
        leaderboardsController = (ILeaderboardsController)leaderboardObject;
        stage = (IStage)stageObject;
    }
    private void Start()
    {
        InitializeControllers();

        InstantiateStage();

        InitializePlayer();

        InitializeScore();

        gameState = GameState.Menu;
    }
    private void InitializeControllers()
    {
        menuController.Setup(HandleOnClickPlay, HandleOnClickLeaderboards);

        menuController.Show();

        scoreWindowController.Setup(HandleOnClickPause);

        pauseController.Setup(HandleClickMenu, HandleClickContinue);
    }
    public void HandleOnClickPause()
    {
        if (gameState != GameState.Playing) 
            return;

        pauseController.Show();
        stage.TogglePause(true);
        player.TogglePause(true);
    }
    private void HandleClickMenu()
    {
        ResetGame();
    }
    private void HandleClickContinue()
    {
        pauseController.Hide();
        stage.TogglePause(false);
        player.TogglePause(false);
    }
    private void HandleOnClickPlay()
    {
        gameState = GameState.Tutorial;

        menuController.Hide();

        startController.Show();
    }
    private void HandleOnClickLeaderboards()
    {
        var highscores = scoreController.GetHighscores();

        leaderboardsController.Setup(highscores, HandleClickCloseLeaderboards);
        leaderboardsController.Show();
    }
    private void HandleClickCloseLeaderboards()
    {
        leaderboardsController.Hide();
    }
    public void OnFlapInput(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        TouchScreen();

        player.Flap();
    }
    private void InitializeScore()
    {
        SetScore(0);
    }
    private void InitializePlayer()
    {
        player.Setup(this, stage.GetStageData().availableSkins);
    }

    StageData SelectRandomStage()
    {
        return stagePrefabs[Random.Range(0, stagePrefabs.Length)];
    }
    private void InstantiateStage()
    {
        var randomStage = SelectRandomStage();

        var stageData = Instantiate(randomStage, transform);

        soundController.Setup(stageData.soundData);

        stage.Setup(stageData);
    }
    private void SetScore(int value)
    {
        currentScore = value;
        RefreshScore();
    }
    private void RefreshScore()
    {
        scoreWindowController.SetPoint(currentScore);
    }
    private void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
    private void ShowScreenFlash()
    {
        var data = stage.GetStageData().screenflashData;
        screenflasherController.Show(data.color, data.time, () => {
            ShowGameOverScreen();
        });
    }
    private void ShowGameOverScreen()
    {
#if UNITY_EDITOR
        if (OverrideScore)
            currentScore = OverridenScore;
#endif

        Sprite medal = null;
        var medalData = scoreController.GetMedal(currentScore);

        if (medalData != null)
            medal = medalData.medalSprite;

        scoreController.Store(currentScore);

        gameOverController.Setup(currentScore, scoreController.GetHighestScore(), medal, ResetGame);
        gameOverController.Show();
    }
    public void TouchScreen()
    {
        switch (gameState)
        {
            case GameState.Tutorial:
                StartGame();
                soundController.PlayFlap();
                break;
            case GameState.Playing:
                soundController.PlayFlap();
                break;
            default:
                break;
        }
    }
    public void StartGame()
    {
        scoreWindowController.Show();

        startButton.gameObject.SetActive(false);
        stage.Run();

        player.Run();

        gameState = GameState.Playing;

        startController.Hide();
    }
    public void AddScore()
    {
        soundController.PlayerScored();

        SetScore(++currentScore);
    }
    public void Death()
    {
        if (gameState != GameState.Playing)
            return;

        stage.Stop();
        player.Stop();

        gameState = GameState.Death;

        ShowScreenFlash();

        soundController.PlayDied();
    }
}
