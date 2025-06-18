using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    private static InGameUI instance;
    public GameObject victoryScreen, gameOverScreen;
    public PlayerMovement playerMovement;
    public GameObject[] uiCanes;
    public Animator bulletCounterAnimator;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        
        if (playerMovement.currentAmmo <= 0)
        {
            bulletCounterAnimator.SetTrigger("reload");
        }
        else if(playerMovement.currentAmmo > 0)
        {
            bulletCounterAnimator.SetTrigger("idle");
        }

        for (int i = 0; i < uiCanes.Length; i++)
        {

                if (i < playerMovement.currentAmmo)
                {
                    uiCanes[i].SetActive(true);
                }
                else
                {
                    uiCanes[i].SetActive(false);
                }
        }
        
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
