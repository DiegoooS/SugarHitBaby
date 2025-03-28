using UnityEngine;

namespace SugarHitBaby
{
    public class ZombieTrap : MonoBehaviour
    {
        [SerializeField] private int trapDamage = 3;
        [SerializeField] private int playerPushForce = 3;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Player>(out Player player))
            {
                CheckIfPlayerStepOnTrap(player);
            }
        }

        private void CheckIfPlayerStepOnTrap(Player player)
        {
            ReducePlayerHealth(player);
            AddForcePushOnPlayer(player);
        }

        private void AddForcePushOnPlayer(Player player)
        {
            Vector3 pushDirection = (player.transform.position - transform.position).normalized;

            player.GetComponent<Rigidbody2D>().AddForce(pushDirection * playerPushForce, ForceMode2D.Impulse);
            player.GetComponent<PlayerMovement>().InAirState();
        }

        private void ReducePlayerHealth(Player player)
        {
            Health playerHealth = player.gameObject.GetComponent<Health>();

            if (playerHealth == null) return;

            playerHealth.TakeDamage(trapDamage);
        }
    }
}

