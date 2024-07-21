using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(IMovement)), RequireComponent(typeof(IAnimator))]
public class PlayerController : MonoBehaviour, IPlayer
{
    [SerializeField, OnValueChanged("OnchangeDisabledCallback")] private bool Disabled;


    private IMovement movementModule;
    private IAnimator animatorModule;
    private bool isRunning;
    new private Rigidbody2D rigidbody;
    private IGameController gameController;
    private int scoreLayer, obstacleLayer;

    private void Awake()
    {
        Init();

        SetTutorial();
    }
    private void Update()
    {
        UpdateRotation();
    }
    private void Init()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        movementModule = GetComponent<IMovement>();
        animatorModule = GetComponent<IAnimator>();
        obstacleLayer = LayerMask.NameToLayer("Obstacle");
        scoreLayer = LayerMask.NameToLayer("ObstacleScore");
    }
    private void OnchangeDisabledCallback()
    {
        Init(); //Puting these here because Disabled can be set in editor

        rigidbody.bodyType = Disabled ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;
    }
    private void UpdateRotation()
    {
        if (!isRunning)
            return;

        var speed = movementModule.GetSpeed();
        var dot = Vector2.Dot(Vector2.down, speed);

        var upDot = Mathf.Clamp(dot, -1, 1);

        movementModule.SetRotation(upDot);
    }
    public void Setup(IGameController gameController)
    {
        this.gameController = gameController;
    }
    private void SetTutorial()
    {
        DisableGravity();
    }
    private void DisableGravity()
    {
        rigidbody.gravityScale = 0f;
    }
    private void EnableGravity()
    {
        rigidbody.gravityScale = 1f;
    }
    private void HandleCollision(Collision2D collision)
    {
        if (collision.gameObject.layer == scoreLayer)
        {
            HandleScore();
            return;
        }
        if (collision.gameObject.layer == obstacleLayer)
        {
            HandeDeath();
            return;
        }
    }
    private void HandleScore()
    {
        gameController.AddScore();
    }
    private void HandeDeath()
    {
        gameController.Death();
    }
    public void OnFlapInput(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        gameController.TouchScreen();

        if (!isRunning)
            return;

        movementModule.Flap();
    }
    [Button("Enable")]
    public void Run()
    {
        isRunning = true;
        EnableGravity();
    }
    public void Stop()
    {
        isRunning = false;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision);
    }
}
