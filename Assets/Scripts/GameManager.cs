using TMPro;
using UnityEngine;

namespace SugarHitBaby
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private int zombieKillCounter = 0;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(Instance);
            }
            Instance = this;
        }

        public void AddZombieKillCounter()
        {
            zombieKillCounter++;
            UIManager.Instance.SetZombieKillCounter(zombieKillCounter);
        }
    }
}

