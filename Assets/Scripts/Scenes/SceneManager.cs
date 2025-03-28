using System;
using UnityEngine;

namespace SugarHitBaby
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance { get; private set; }

        private int currentLevelNumber = 0;
        private int totalScenes;

        private void Awake() => SetInstance();

        private void SetInstance()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            Instance = this;
        }

        private void Start()
        {
            SetTotalScenesNumber();
        }

        private void SetTotalScenesNumber()
        { 
            totalScenes = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        }

        public void SetNewGameLevel()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LEVEL_1");
        }

        public void NextLevel()
        {
            currentLevelNumber = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            currentLevelNumber++;
            if (currentLevelNumber >= totalScenes)
            {
                SetEndGameLevel();
                return;
            }

            Debug.Log(currentLevelNumber);
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentLevelNumber);
        }

        public void SetEndGameLevel()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MAIN_MENU");
        }
    }
}

