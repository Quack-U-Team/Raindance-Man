using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject[] buttons;

    [Header("Audio")]
    // public AudioSource UISound0, UISound1, UISound2;
    public Slider SoundtrackSlider;
    public Slider SFXSlider;

    public void CheckVolumeLevels()
    {
        if (PlayerPrefs.GetInt("soundtrack_volume_set") == 1)
        {
            SoundtrackSlider.value = PlayerPrefs.GetFloat("soundtrack_volume");
        }
        else
        {
            SoundtrackSlider.value = 1;
            PlayerPrefs.SetInt("soundtrack_volume", 1);
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.GetInt("sfx_volume_set") == 1)
        {
            SFXSlider.value = PlayerPrefs.GetFloat("sfx_volume");
        }
        else
        {
            SFXSlider.value = 1;
            PlayerPrefs.SetInt("sfx_volume", 1);
            PlayerPrefs.Save();
        }
    }

    private void Start()
    {
        Time.timeScale = 1f;
        CheckVolumeLevels();
        //StartCoroutine(ButtonsStaggerAnimation());
    }

    IEnumerator ButtonsStaggerAnimation()
    {
        for (int i = 0; i < buttons.Length; i++) 
        {
            yield return new WaitForSecondsRealtime(0.3f);
            buttons[i].SetActive(true);
        }
    }

    public void StartGame(int i)
    {
        // UISound0.Play(); per esempio
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetSFXVolume()
    {
        PlayerPrefs.SetFloat("sfx_volume", SFXSlider.value);
        PlayerPrefs.SetInt("sfx_volume_set", 1);
        PlayerPrefs.Save();
        print("set sfx volume: " + PlayerPrefs.GetFloat("sfx_volume"));
    }
    
    public void SetSoundtrackVolume()
    {
        PlayerPrefs.SetFloat("soundtrack_volume", SoundtrackSlider.value);
        PlayerPrefs.SetInt("soundtrack_volume_set", 1);
        PlayerPrefs.Save();
        print("set soundtrack volume: "+PlayerPrefs.GetFloat("soundtrack_volume"));
    }

}
