using UnityEngine;

namespace SugarHitBaby
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] public int jumpForce = 5;
        [SerializeField] private int movementSpeed = 3;

        private float playerRotation;
        private float playerDirection;
        private bool isJumping;
        private bool canPlayerJump = true;    
        private Rigidbody2D characterRigidBody2D;
        private Animator characterAnimator;

        private void Awake()
        {
            SetRigidBody();
            SetAnimator();
        }

        private void SetAnimator() => characterAnimator = GetComponent<Animator>();

        private void SetRigidBody() => characterRigidBody2D = GetComponent<Rigidbody2D>();

        private void Update()
        {
            SetPlayerLookDirection();
            PlayerRotate();
            SetPlayerJumpingState();
        }

        private void SetPlayerJumpingState()
        {
            if (Input.GetButtonDown("Jump") && canPlayerJump)
            {
                ActivateJump();
            }
        }

        private void ActivateJump()
        {
            isJumping = true;
            InAirState();
        }

        public void InAirState()
        {
            canPlayerJump = false;
            characterAnimator.SetBool("isJumping", true);
            characterAnimator.SetBool("isMoving", false);
        }

        public void StopPlayerMovementAnimations()
        {
            characterAnimator.SetBool("isJumping", false);
            characterAnimator.SetBool("isMoving", false);
        }

        private void SetPlayerLookDirection() => playerDirection = Input.GetAxisRaw("Horizontal");

        private void FixedUpdate()
        {
            PlayerMove();
            PlayerJump();
        }

        private void PlayerMove()
        {
            if (playerDirection == 0) characterAnimator.SetBool("isMoving", false);
            else if (!isJumping) characterAnimator.SetBool("isMoving", true);

            characterRigidBody2D.linearVelocity = new Vector2(playerDirection * movementSpeed, characterRigidBody2D.linearVelocity.y);
        }

        private void PlayerRotate()
        {
            if (playerDirection == 0) return;

            playerRotation = Input.GetAxisRaw("Horizontal") >= 0 ? 0 : 180;
            transform.rotation = Quaternion.Euler(0f, playerRotation, 0f);
        }

        private void PlayerJump()
        {
            if (!isJumping) return;

            characterRigidBody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); 
            
            isJumping = false;
        }

        private void OnCollisionEnter2D(Collision2D collision) => EndPlayerJumpAnimation(collision);

        private void EndPlayerJumpAnimation(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Ground>() || collision.gameObject.GetComponent<Zombie>())
            {
                characterAnimator.SetBool("isJumping", false);
                canPlayerJump = true;
            }
        }
    }
}
