using AYellowpaper;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameController : MonoBehaviour, IGameController
{
    [RequireInterface(typeof(IStage))]
    [SerializeField] private MonoBehaviour stagePrefab;
    [RequireInterface(typeof(IPlayer))]
    [SerializeField] private MonoBehaviour playerObject;
    [SerializeField] private GameObject startButton;

    private IStage stage;
    private IPlayer player;
    private bool isRunning;
    private void Awake()
    {
        player = (IPlayer)playerObject;
    }
    private void Start()
    {
        InstantiateStage();

        InitializePlayer();
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
    public void TouchScreen()
    {
        if (isRunning)
            return;

        StartGame();
    }
    public void StartGame()
    {
        startButton.gameObject.SetActive(false);
        stage.Run();

        player.Run();

        isRunning = true;
    }
    public void AddScore()
    {

    }
    public void Death()
    {
        stage.Stop();
        player.Stop();

        isRunning = false;
    }
}
