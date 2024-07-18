using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float spaceBetween;
    [SerializeField] private Sprite sprite;
    [SerializeField] private SpriteRenderer topObject;
    [SerializeField] private SpriteRenderer bottomObject;

    void Start()
    {
        topObject.sprite = sprite;
        bottomObject.sprite = sprite;

        var half = spaceBetween / 2;
        topObject.transform.localPosition = new Vector3 (0, half, 0);
        bottomObject.transform.localPosition = new Vector3 (0, -half, 0);
    }
}
