using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour, IMovement
{
    [Header("Internal")]
    [SerializeField] private float upForce;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Vector2 rotationRange;

    new private Rigidbody2D rigidbody2D;
    private Vector3 rotationTarget;
    private bool isRunning;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckTap();
        Rotate();
    }

    private void Rotate()
    {
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(rotationTarget), rotationSpeed * Time.deltaTime);
    }
    private void CheckTap()
    {
        if (!isRunning)
            return;

        if (!Input.GetMouseButtonDown(0))
            return;

        var force = -rigidbody2D.velocity + Vector2.up * upForce; //400 is ok
        
        rigidbody2D.AddForce(force);
    }

    public Vector2 GetSpeed()
    {
        return rigidbody2D.velocity;
    }

    public Vector2 GetGravitySpeed()
    {
        return Physics2D.gravity;
    }

    public void SetRotation(float rotation)
    {
        var myRotation = transform.localRotation.eulerAngles;

        rotation = (rotation + 1) / 2;//Change from -1 <-> 1 to 0 <-> 1

        myRotation.z = Mathf.Lerp(rotationRange.x, rotationRange.y, rotation);

        rotationTarget = myRotation;
    }

    public void Run()
    {
        isRunning = true;
    }

    public void Stop()
    {
        isRunning = false;
    }
}
