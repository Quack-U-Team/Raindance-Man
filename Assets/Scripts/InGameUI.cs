using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    public GameObject victoryScreen, deathScreen;

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
