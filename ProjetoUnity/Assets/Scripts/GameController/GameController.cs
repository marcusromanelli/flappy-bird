using AYellowpaper;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour, IGameController
{
    [RequireInterface(typeof(IStage))]
    [SerializeField] private MonoBehaviour stagePrefab;
    [RequireInterface(typeof(IPlayer))]
    [SerializeField] private MonoBehaviour playerObject;
    [SerializeField] private Button startButton;

    private IStage stage;
    private IPlayer player;
    private void Awake()
    {
        startButton.onClick.AddListener(StartGame);

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
    private void StartGame()
    {
        startButton.gameObject.SetActive(false);
        stage.Run();

        player.Run();
    }
    public void AddScore()
    {

    }
    public void Death()
    {
        stage.Stop();
        player.Stop();
    }
}
