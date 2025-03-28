using SugarHitBaby;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.Instance.SetNewGameLevel();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
