using SugarHitBaby;
using UnityEngine;

public class LevelEndBlock : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            SceneManager.Instance.NextLevel();
        }
    }
}
