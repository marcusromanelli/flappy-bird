using AYellowpaper;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour, IGameController
{
    enum GameState
    {
        Tutorial,
        Playing,
        Death
    }
    [RequireInterface(typeof(IStage))]
    [SerializeField] private MonoBehaviour stagePrefab;
    [RequireInterface(typeof(IPlayer))]
    [SerializeField] private MonoBehaviour playerObject;
    [RequireInterface(typeof(IScoreCounterController))]
    [SerializeField] private MonoBehaviour pointCounterControllerObject;
    [RequireInterface(typeof(IGameOverController))]
    [SerializeField] private MonoBehaviour gameOverControllerObject;

    [SerializeField] private GameObject startButton;

    private IStage stage;
    private IPlayer player;
    private IScoreCounterController scoreController;
    private IGameOverController gameOverController;
    private int currentScore;
    private GameState gameState;
    private void Awake()
    {
        player = (IPlayer)playerObject;
        scoreController = (IScoreCounterController)pointCounterControllerObject;
        gameOverController = (IGameOverController)gameOverControllerObject;
    }
    private void Start()
    {
        InstantiateStage();

        InitializePlayer();

        InitializeScore();

        gameState = GameState.Tutorial;
    }
    private void InitializeScore()
    {
        SetScore(0);
    }
    private void InitializePlayer()
    {
        player.Setup(this);
    }
    private void InstantiateStage()
    {
        stage = Instantiate(stagePrefab, transform).GetComponent<IStage>();

        stage.Setup();
    }
    private void SetScore(int value)
    {
        currentScore = value;
        RefreshScore();
    }
    private void RefreshScore()
    {
        scoreController.SetPoint(currentScore);
    }
    private void ResetGame()
    {
        SceneManager.LoadScene(0);
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
    }
    public void AddScore()
    {
        SetScore(++currentScore);
    }
    public void Death()
    {
        stage.Stop();
        player.Stop();

        gameState = GameState.Death;

        gameOverController.Setup(currentScore, 999, null, ResetGame);
        gameOverController.Show();
    }
}
