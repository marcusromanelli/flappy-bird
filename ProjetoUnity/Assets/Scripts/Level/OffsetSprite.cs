using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class OffsetSprite : MonoBehaviour, ITexturable, IStartable
{
    [SerializeField] private float speed;


    private Vector2 offset;
    private MaterialPropertyBlock propertyBlock;
    new private Renderer renderer;
    private bool isRunning;

    public void Run()
    {
        isRunning = true;
    }

    public void SetTexture(Texture texture)
    {
        renderer.sharedMaterial.SetTexture("_MainTex", texture);
    }

    public void Stop()
    {
        isRunning = false;
    }

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        UpdateTiling();
    }

    private void UpdateTiling()
    {
        if (!isRunning)
            return;

        offset.x += speed * Time.deltaTime;

        if (offset.x >= 1)
            offset.x = 0;

        renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
    public void TogglePause(bool pause)
    {
        isRunning = !pause;
    }
}
