using AYellowpaper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(IMovement)), RequireComponent(typeof(IAnimator))]
public class PlayerController : MonoBehaviour, IPlayer
{
    private IMovement movementModule;
    private IAnimator animatorModule;
    private bool isRunning;
    new private Rigidbody2D rigidbody;
    private IGameController gameController;
    private int scoreLayer, obstacleLayer;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        movementModule = GetComponent<IMovement>();
        animatorModule = GetComponent<IAnimator>();
        obstacleLayer = LayerMask.NameToLayer("Obstacle");
        scoreLayer = LayerMask.NameToLayer("ObstacleScore");

        SetTutorial();
    }
    private void Update()
    {
        UpdateRotation();
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
    public void Run()
    {
        isRunning = true;
        EnableInput();
        EnableGravity();
    }
    public void Stop()
    {
        isRunning = false;
        DisableInput();
    }
    private void SetTutorial()
    {
        DisableInput();
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
    private void DisableInput()
    {
        (movementModule).Stop();
    }

    private void EnableInput()
    {
        (movementModule).Run();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {     
        HandleCollision(collision);
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
}
