using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour, IPoolable, IStartable
{
    [SerializeField] public UnityEvent<Obstacle> onLeftScreen;
    [SerializeField] private float spaceBetween;
    [SerializeField] private float maxDistanceFromStart;
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

        var half = spaceBetween / 2;
        topObject.transform.localPosition = new Vector3 (0, half, 0);
        bottomObject.transform.localPosition = new Vector3 (0, -half, 0);
    }

    public void Setup()
    {
        startPosition = transform.position;
    }

    public void OnEnabled()
    {
        EnableAndRun();
    }

    private void EnableAndRun()
    {
        isRunning = true;
        gameObject.SetActive(true);
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

        if((transform.position - startPosition).magnitude >= maxDistanceFromStart)
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
