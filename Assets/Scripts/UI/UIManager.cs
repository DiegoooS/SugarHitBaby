using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SugarHitBaby
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [SerializeField] private Slider healthBar;
        [SerializeField] private TMP_Text zombieCounterText;

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(Instance);
            }
            Instance = this;
        }

        public void SetZombieKillCounter(int zombieKilled)
        {
            zombieCounterText.text = zombieKilled.ToString();
        }

        public void UpdateHealthUI(float healthPercentage)
        {
            healthBar.value = healthPercentage;
        }
    }
}

