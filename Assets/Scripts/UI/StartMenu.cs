using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    // public AudioSource UISound0, UISound1, UISound2;
    [Header("Audio")]
    public Slider SoundtrackSlider;
    public Slider SFXSlider;

    public void CheckVolumeLevels()
    {
        if (PlayerPrefs.GetInt("soundtrack_volume_set") == 1)
        {
            SoundtrackSlider.value = PlayerPrefs.GetFloat("soundtrack_volume");
        }
        if (PlayerPrefs.GetInt("sfx_volume_set") == 1)
        {
            SFXSlider.value = PlayerPrefs.GetFloat("sfx_volume");
        }
    }

    private void Start()
    {
        Time.timeScale = 1f;

        CheckVolumeLevels();
    }

    public void StartGame(int i)
    {
        // UISound0.Play(); per esempio
        SceneManager.LoadScene(i);
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
