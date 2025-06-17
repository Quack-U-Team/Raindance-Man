using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    public GameObject victoryScreen, gameOverScreen;
    private static InGameUI instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public static void Victory()
    {
        if (instance != null) 
        {
            Time.timeScale = 0f;
            instance.victoryScreen.SetActive(true);
        }
        else
        {
            Debug.LogError("InGameUI instance not found");
        }
    }

    public static void GameOver()
    {
        if (instance != null)
        {
            Time.timeScale = 0f;
            instance.gameOverScreen.SetActive(true);
        }
        else
        {
            Debug.LogError("InGameUI instance not found");
        }
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
