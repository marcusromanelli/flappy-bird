using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour, IPoolable
{
    [SerializeField] public UnityEvent<Obstacle> onLeftScreen;
    [SerializeField] private float spaceBetween;
    [SerializeField] private Vector3 speed;
    [SerializeField] private Sprite sprite;
    [SerializeField] private SpriteRenderer topObject;
    [SerializeField] private SpriteRenderer bottomObject;

    private int endPointLayer;
    private bool isRunning;

    private void Awake()
    {
        endPointLayer = LayerMask.NameToLayer("EndPoint"); //Placeholder
    }
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
        
    }

    public void OnEnabled()
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
        onLeftScreen.RemoveAllListeners();
        isRunning = false;
        gameObject.SetActive(false);
    }
    private void Update()
    {
        Run();
    }

    private void Run()
    {
        if (!isRunning)
            return;

        var nextPosition = transform.position;

        nextPosition += speed * Time.deltaTime;

        transform.position = nextPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision);
    }

    private void HandleCollision(Collider2D collision)
    {
        if (!isRunning || collision.gameObject.layer != endPointLayer)
            return;
        
        onLeftScreen?.Invoke(this);
    }
}
