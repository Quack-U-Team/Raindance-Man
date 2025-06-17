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

    public static void OpenVictoryScreen()
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

    public static void OpenDeathScreen()
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

    public static void Victory()
    {
        
    }
}
