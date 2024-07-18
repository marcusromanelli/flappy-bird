using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class OffsetSprite : MonoBehaviour
{
    [SerializeField] private float speed;


    private Vector2 offset;
    private MaterialPropertyBlock propertyBlock;
    new private Renderer renderer;

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
        offset.x += speed * Time.deltaTime;

        if (offset.x >= 1)
            offset.x = 0;

        renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
