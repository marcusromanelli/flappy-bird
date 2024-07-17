using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private SkinData[] availableSkins;

    private Animator animator;
    private SkinData currentSkin;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        SelectRandomSkin();
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
