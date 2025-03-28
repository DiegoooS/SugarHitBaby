using SugarHitBaby;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] AnimationClip relodClip;

    private Animator playerAnimator;

    private void Awake() => SetPlayerAnimator();

    private void SetPlayerAnimator() => playerAnimator = GetComponent<Animator>();

    public void StartReloading() => playerAnimator.SetTrigger("Reloading");

    public float GetReloadAnimationDuration()
    {
        return relodClip.length;
    }

    public void SetHealthUI(int maxHealth, int currentHealth)
    {
        UIManager.Instance.UpdateHealthUI((float)currentHealth / (float)maxHealth);
    }
}
