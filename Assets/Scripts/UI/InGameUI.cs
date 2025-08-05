using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [Header("Audio")]
    public GameObject inGameOst;

    [Header("Other")]
    private static InGameUI instance;
    public GameObject victoryScreen, gameOverScreen;
    public PlayerMovement playerMovement;

    [Header("Player info")]
    public GameObject ansiaUI;
    public GameObject depressioneUI;
    public GameObject[] uiCanes;
    public Animator bulletCounterAnimator;
    public Slider mentalHealthBar;
    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;
    }

    void UpdateAmmoUI()
    {
        if (playerMovement.currentAmmo <= 0)
        {
            bulletCounterAnimator.SetTrigger("reload");
        }
        else if (playerMovement.currentAmmo > 0)
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

    void UpdateMentalHealthUI()
    {
        mentalHealthBar.value = playerMovement.sanitaMentale;
    }

    void UpdateDebuffUI()
    {
        if (playerMovement.ansia)
        {
            ansiaUI.SetActive(true);
        }
        else
        {
            ansiaUI.SetActive(false);
        }
        
        if (playerMovement.depressione)
        {
            depressioneUI.SetActive(true);
        }
        else
        {
            depressioneUI.SetActive(false);
        }
    }

    private void Update()
    {
        UpdateAmmoUI();
        UpdateMentalHealthUI();
        UpdateDebuffUI();
    }

    public static void Victory()
    {
        if (instance != null) 
        {
            Time.timeScale = 0f;
            instance.inGameOst.SetActive(false);
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
            instance.inGameOst.SetActive(false);
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

    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
    }


    public void UnlockDash()
    {
        PlayerPrefs.SetString("canDash", "true");
        PlayerPrefs.Save();
    }
}
