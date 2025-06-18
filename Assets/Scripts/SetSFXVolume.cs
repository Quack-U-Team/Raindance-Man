using UnityEngine;

public class SetSFXVolume : MonoBehaviour
{
    private AudioSource[] soundEffects;

    private void Awake()
    {
        soundEffects = GetComponents<AudioSource>();
    }

    private void Update()
    {
        for (int i = 0; i < soundEffects.Length; i++) 
        {
            soundEffects[i].volume = PlayerPrefs.GetFloat("sfx_volume");
        }
    }
}
