using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Animator)), RequireComponent(typeof(IMovement))]
public class PlayerAnimation : MonoBehaviour, IAnimator
{
    [SerializeField] private SkinData[] availableSkins; 
    [Header("Internal")]
    [SerializeField] private AnimationCurve rotationCurve;

    private IMovement movementModule;
    private Animator animator;
    private SkinData currentSkin;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movementModule = GetComponent<IMovement>();
    }

    private void Start()
    {
        SelectRandomSkin();
    }

    private void Update()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        if(movementModule == null)
        {
            Debug.LogError("Movement module not found.");
            return;
        }

        var speed = movementModule.GetSpeed();
        var dot = Vector2.Dot(Vector2.down, speed);

        var upDot = Mathf.Clamp(dot, -1, 1);


        movementModule.SetRotation(upDot);
    }

    private void SelectRandomSkin()
    {
        var maxNumber = availableSkins.Length;

        var randomNumber = Random.Range(0, maxNumber);

        SelectSkin(randomNumber);
    }

    private void SelectSkin(int skinIndex)
    {
        if(currentSkin != null)
        {
            animator.SetLayerWeight(currentSkin.LayerIndex, 0);
        }

        var skin = GetSkin(skinIndex);

        currentSkin = skin;

        animator.SetLayerWeight(skin.LayerIndex, 1);
    }

    private SkinData GetSkin(int skinIndex)
    {
        return availableSkins[skinIndex];
    }

    [Button("Next Random Skin")]
    private void GoToNextRandomSkin()
    {
        SelectRandomSkin();
    }

    public void SetSkin(int skinIndex)
    {
        if (skinIndex < 0 || skinIndex > availableSkins.Length)
            return;

        SetSkin(skinIndex);
    }
}
