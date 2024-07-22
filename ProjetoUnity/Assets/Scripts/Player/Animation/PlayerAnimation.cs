using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour, IAnimator
{
    private SkinData[] availableSkins;
    private Animator animator;
    private SkinData currentSkin;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void SelectRandomSkin()
    {
        var maxNumber = availableSkins.Length;

        var randomNumber = Random.Range(0, maxNumber);

        SelectSkin(randomNumber);
    }
    public void Setup(SkinData[] skinData)
    {
        availableSkins = skinData;

        SelectRandomSkin();
    }
    private SkinData GetSkin(int skinIndex)
    {
        return availableSkins[skinIndex];
    }
    private void SelectSkin(int skinIndex)
    {
        if (currentSkin != null)
        {
            animator.SetLayerWeight(currentSkin.LayerIndex, 0);
        }

        var skin = GetSkin(skinIndex);

        currentSkin = skin;

        animator.SetLayerWeight(skin.LayerIndex, 1);
    }

}
