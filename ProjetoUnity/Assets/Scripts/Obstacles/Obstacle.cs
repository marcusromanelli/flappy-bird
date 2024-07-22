using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour, IObstacle, IStartable
{
    [SerializeField] public UnityEvent<Obstacle> onLeftScreen;
    [SerializeField] private float spaceBetweenPipes;
    [SerializeField] private float distanceToLeaveScreen;
    [SerializeField] private float maxVerticalDistanceFromCenter;
    [SerializeField] private Vector3 speed;
    [SerializeField] private Sprite sprite;
    [SerializeField] private SpriteRenderer topObject;
    [SerializeField] private SpriteRenderer bottomObject;

    private bool isRunning;
    private Vector3 startPosition;

    private void Start()
    {
        topObject.sprite = sprite;
        bottomObject.sprite = sprite;


        var half = spaceBetweenPipes / 2;
        topObject.transform.localPosition = new Vector3 (0, half, 0);
        bottomObject.transform.localPosition = new Vector3 (0, -half, 0);
    }

    public void Setup()
    {
        startPosition = transform.position;
    }
    public void Setup(Vector3 startPosition)
    {
        this.startPosition = startPosition;
        transform.position = startPosition;

        RandomizeVerticalPosition();
    }

    public void OnEnabled()
    {
        EnableAndRun();
    }
    private void RandomizeVerticalPosition()
    {
        var verticalPosition = Random.Range(-maxVerticalDistanceFromCenter, maxVerticalDistanceFromCenter);

        transform.position = new Vector3(transform.position.x, verticalPosition, transform.position.z);
    }
    private void EnableAndRun()
    {
        isRunning = true;
        gameObject.SetActive(true);

        RandomizeVerticalPosition();
    }

    public void OnDisabled()
    {
        Reset();
    }

    public void Destroy()
    {
        Reset();
    }

    private void Reset()
    {
        Stop();
        gameObject.SetActive(false);
    }
    private void Update()
    {
        CheckPosition();
    }

    private void CheckPosition()
    {
        if (!isRunning)
            return;

        var nextPosition = transform.position;

        nextPosition += speed * Time.deltaTime;

        transform.position = nextPosition;

        if((transform.position - startPosition).magnitude >= distanceToLeaveScreen)
            HandleLeftScreen();
    }

    private void HandleLeftScreen()
    {        
        onLeftScreen?.Invoke(this);
    }

    public void Run()
    {
        EnableAndRun();
    }

    public void Stop()
    {
        onLeftScreen.RemoveAllListeners();
        isRunning = false;
    }
}
