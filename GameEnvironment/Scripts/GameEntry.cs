using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameEntry : MonoBehaviour
{   
    AudioManager audioManager;
    public UnityEngine.UI.Button newGame;
    public UnityEngine.UI.Button continueGame;
    public static string jwt = null;
    private bool questionnaire_attempted;
    
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    void Start()
    {
        newGame.interactable = false;
        continueGame.interactable = false;
        StartCoroutine(GetJwt());
        StartCoroutine(GetBooleanValue());
    }

    private void Update()
    {
        if (jwt != null)
        {
            //If authorized, can start new game
            newGame.interactable = true;

            if (questionnaire_attempted)
            {
                //If authorized and quiz has been completed, can continue game
                continueGame.interactable = true;
            }
        }
        
    }
    
    //Getting the JWT Token
    IEnumerator GetJwt()
    { 
        using (UnityWebRequest webRequest = UnityWebRequest.Get("https://firefly-fluent-personally.ngrok-free.app/get_jwt"))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);                
            }
            else
            {
                string response = webRequest.downloadHandler.text;
                TokenData data = JsonUtility.FromJson<TokenData>(response);
                jwt = data.token;
                Debug.Log(jwt);
            }
        }
        
    }

    //Checking whether the questionnaire has been attempted before
    IEnumerator GetBooleanValue()
    {
        using (UnityWebRequest www = UnityWebRequest.Post("https://firefly-fluent-personally.ngrok-free.app/questionnaire_attempted", "1","application/json"))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to get response: " + www.error);
            }
            else
            {
                if (bool.TryParse(www.downloadHandler.text, out questionnaire_attempted))
                {
                    Debug.Log("Boolean value from server: " + questionnaire_attempted.ToString());
                }
                else
                {
                    Debug.LogError("Failed to parse boolean value from server response.");
                }
            }
        }
        
    }
    
    public void StartNewGame()
    {   
        audioManager.PlaySFX(audioManager.button);
        PlayerPrefs.SetInt("Continue", 0);

        //If New Game, load the quiz scene
        SceneManager.LoadScene(5);
    }

    public void ContinueGame()
    {   
        audioManager.PlaySFX(audioManager.button);
        PlayerPrefs.SetInt("Continue", 1);

        //To Continue previous game, load the ground floor of the game
        SceneManager.LoadScene(3);
    }
}

public class TokenData
{
    public string token;
}

public interface Interactable
{
    void Interact();
}