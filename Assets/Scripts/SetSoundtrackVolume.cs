using UnityEngine;

public class SetSoundtrackVolume : MonoBehaviour
{
    private AudioSource soundtrack;
    private void Awake()
    {
       soundtrack = this.GetComponent<AudioSource>();
    }
    private void Start()
    {
        soundtrack.volume = PlayerPrefs.GetFloat("soundtrack_volume");
    }
}
