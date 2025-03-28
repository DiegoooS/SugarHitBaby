using System;
using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;

public abstract class Zombie : MonoBehaviour
{
    public abstract int MovementSpeed { get; protected set; }

    [SerializeField] private float maxRangeToAttackPlayer = 5f;
    [SerializeField] private float attackSpeed = 2f;
    [SerializeField] private Transform aimTransform;
    [SerializeField] private int damage = 1;

    private Rigidbody2D zombieRigidBody;
    private Animator zombieAnimator;
    private Player player;
    private float distanceToPlayer;
    private Vector3 directionTowardsPlayer;
    private bool attackCooldownAtive = false;

    void Start()
    {
        SetGameObjects();
        SetComponents();
    }

    private void SetGameObjects() => player = FindFirstObjectByType<Player>();

    private void SetComponents()
    {
        zombieAnimator = GetComponent<Animator>();
        zombieRigidBody = GetComponent<Rigidbody2D>(); 
    }

    private void Update()
    {
        SetDistanceToPlayer();
        SetDirectionTowardsPlayer();
        SetRotation();
        CheckIfZombieCanMove();
        CheckIfZombieCanAttack();
    }

    private void CheckIfZombieCanMove()
    {
        if (CanMoveTowardsPlayer())
        {
            zombieAnimator.SetBool("isMoving", true);

            SetPlayerBodyType(RigidbodyType2D.Dynamic);
        }
        else
        {
            zombieAnimator.SetBool("isMoving", false);

            SetPlayerBodyType(RigidbodyType2D.Static);
        }    
    }

    // Prevent Zombie from being pushed by the player
    private void SetPlayerBodyType(RigidbodyType2D bodyType)
    {
        zombieRigidBody.bodyType = bodyType;
    }

    private void CheckIfZombieCanAttack()
    {
        if (CanAttackPlayer() && !attackCooldownAtive)
        {
            Attack();
            attackCooldownAtive = true;
            zombieAnimator.SetTrigger("Attack");
            StartCoroutine(SetZombieAttackSpeed());
        }
    }

    IEnumerator SetZombieAttackSpeed()
    {
        yield return new WaitForSeconds(attackSpeed);

        attackCooldownAtive = false;
    }

    private void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(aimTransform.position, transform.right, 1f);

        if (!hit) return;

        if (hit.collider.gameObject.TryGetComponent<Player>(out Player player))
        {
            Health playerHealth = player.GetComponent<Health>();
            playerHealth.TakeDamage(damage);
        }
    }

    private void SetRotation()
    {
        if (player == null) return;

        float zombieRotation = directionTowardsPlayer.x < 0 ? 180 : 0;
        transform.rotation = Quaternion.Euler(0, zombieRotation, 0);
    }

    private void SetDirectionTowardsPlayer()
    {
        if (player == null) return;

        directionTowardsPlayer = (player.transform.position - transform.position).normalized * MovementSpeed;
    }

    private void SetDistanceToPlayer()
    {
        if(player == null) return;

        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
    }

    private bool CanAttackPlayer()
    {
        if (player == null) return false;

        return distanceToPlayer < 1f;
    }

    private bool CanMoveTowardsPlayer()
    {
        if (maxRangeToAttackPlayer < distanceToPlayer || CanAttackPlayer() || player == null)
            return false;

        return true;
    }

    private void FixedUpdate()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (!CanMoveTowardsPlayer()) return;

        zombieRigidBody.linearVelocity = directionTowardsPlayer;
    }
}
