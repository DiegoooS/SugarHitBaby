using SugarHitBaby;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private int maxAmmo = 10;
    [SerializeField] Transform aim;
    [SerializeField] private int weaponDamage = 1;
    [SerializeField] private float attackSpeed = 0.5f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip reloadSound;
    [SerializeField] private float atackRange = 35f;
    
    private int currentAmmo;
    private Animator gunAnimator;
    private AudioSource gunAudioSource;
    private bool canShoot = true;
    private Player player;
    IEnumerator resetAttackCoroutine;

    private void Awake()
    {
        SetGunAnimtor();
        SetPlayer();
        SetGunAudioSource();
    }

    private void SetGunAudioSource() => gunAudioSource = GetComponent<AudioSource>();

    private void SetPlayer() => player = FindAnyObjectByType<Player>();

    private void SetGunAnimtor() => gunAnimator = GetComponent<Animator>();

    private void Start() => SetCurrentAmmo(maxAmmo);

    private void SetCurrentAmmo(int ammo) => currentAmmo = ammo;

    private void Update() => Shoot();

    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!CheckIfCanShoot()) return;

            currentAmmo--;
            PlaySoundEffect(shootSound);

            SetAttackCooldown(attackSpeed);

            RaycastHit2D hit = Physics2D.Raycast(aim.position, transform.right, atackRange);
            gunAnimator.SetTrigger("Shoot");

            if (!hit) return;

            if (hit.collider.gameObject.TryGetComponent<Health>(out Health health))
            {
                health.TakeDamage(weaponDamage);
            }
        }
    }

    private bool CheckIfCanShoot()
    {
        if (!canShoot) return false;

        if (currentAmmo <= 0)
        {
            ReloadAmmo();
            return false;
        }

        return true;
    }

    private void PlaySoundEffect(AudioClip clip) => gunAudioSource.PlayOneShot(clip);

    private void ReloadAmmo()
    {
        PlaySoundEffect(reloadSound);
        player.GetComponent<PlayerMovement>().StopPlayerMovementAnimations();
        player.StartReloading();
        currentAmmo = maxAmmo;

        SetAttackCooldown(player.GetReloadAnimationDuration());
    }

    private void SetAttackCooldown(float atackCooldown)
    {
        canShoot = false;
        if (resetAttackCoroutine != null) resetAttackCoroutine = null;
        resetAttackCoroutine = SetCanShoot(atackCooldown);
        StartCoroutine(resetAttackCoroutine);
    }

    private IEnumerator SetCanShoot(float timeToShoot)
    {
        yield return new WaitForSeconds(timeToShoot);
        canShoot = true;
    }
}
