using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    //Loading the Game Entry Page
    public void GameEntry()
    {
        audioManager.PlaySFX(audioManager.button); 
        SceneManager.LoadSceneAsync(1);
    }
}
