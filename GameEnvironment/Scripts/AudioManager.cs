using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----------------- Audio Source ----------------")]
    [SerializeField] AudioSource musicSource; //Main background music
    [SerializeField] AudioSource SFXSource; //Other sound effects

    [Header("----------------- Audio Clip ----------------")]
    public AudioClip background;
    public AudioClip button;
    public AudioClip collect;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
