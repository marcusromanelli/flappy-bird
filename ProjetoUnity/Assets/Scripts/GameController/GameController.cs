using AYellowpaper;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour, IGameController
{
    public UnityEvent OnPlayerDie;
    public UnityEvent OnGameStart;
    public UnityEvent OnPlayerScore;
    
    enum GameState
    {
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

    [SerializeField] private GameObject startButton;

    private IStage stage;
    private IPlayer player;
    private IScoreWindowController scoreWindowController;
    private IGameOverController gameOverController;
    private IScoreController scoreController;
    private IScreenflasherController screenflasherController;
    private IStartWindowController startController;
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
        stage = (IStage)stageObject;
    }
    private void Start()
    {
        InstantiateStage();

        InitializePlayer();

        InitializeScore();

        gameState = GameState.Tutorial;

        startController.Show();
    }
    private void InitializeScore()
    {
        SetScore(0);
    }
    private void InitializePlayer()
    {
        player.Setup(this);
    }

    StageData SelectRandomStage()
    {
        return stagePrefabs[Random.Range(0, stagePrefabs.Length)];
    }
    private void InstantiateStage()
    {
        var randomStage = SelectRandomStage();

        var stageData = Instantiate(randomStage, transform);

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
        var data = stage.GetScreenflashData();
        screenflasherController.Show(data.color, data.time, () => {
            ShowGameOverScreen();
        });
    }
    private void ShowGameOverScreen()
    {
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
        if (gameState != GameState.Tutorial)
            return;

        StartGame();
    }
    public void StartGame()
    {
        startButton.gameObject.SetActive(false);
        stage.Run();

        player.Run();

        gameState = GameState.Playing;

        startController.Hide();
    }
    public void AddScore()
    {
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
    }
}
