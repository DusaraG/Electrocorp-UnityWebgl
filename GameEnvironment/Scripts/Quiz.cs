using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Quiz : MonoBehaviour
{   
    AudioManager audioManager;
    public UnityEngine.UI.Button playQuiz;
    public UnityEngine.UI.Button continueGame;
    public static string jwt = null;
    private bool questionnaire_attempted;
    public string url="https://chipper-moxie-687b08.netlify.app/";

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    void Start()
    {
        playQuiz.interactable = true;
        continueGame.interactable = false;
        StartCoroutine(GetJwt());
        StartCoroutine(ResetQuiz());
        StartCoroutine(GetBooleanValue());
    }

    private void Update()
    {
        //Making the continue option available once the questionnaire has been attempted
        continueGame.interactable = questionnaire_attempted;
    }

    //Get JWT Token    
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
            }
        }
    }

    //Check if the questionnaire has been attempted
    IEnumerator GetBooleanValue()
    {
        while (true)
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
                    //Debug.Log("Boolean value from server: " + questionnaire_attempted);
                    Debug.Log("Boolean value from server: " + questionnaire_attempted.ToString());
                }
                else
                {
                    Debug.LogError("Failed to parse boolean value from server response.");
                }
            }
        }
        yield return new WaitForSeconds(5);
        }
    }

    //Since the new game option has been selected, reset the quiz
    IEnumerator ResetQuiz()
    {
        using (UnityWebRequest www = UnityWebRequest.Post("https://firefly-fluent-personally.ngrok-free.app/reset_player", "1","application/json"))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to get response: " + www.error);
            }
            else
            {
                Debug.Log("Quiz reset successfully.");
            }
        }
    }
    
    //Redirect to the quiz page
    public void PlayQuiz()
    {   
        audioManager.PlaySFX(audioManager.button);
        OpenWebsite();
    }

    public void PlayGame()
    {   
        audioManager.PlaySFX(audioManager.button);

        //Load the add player details scene
        SceneManager.LoadScene(6);
    }

    public void OpenWebsite()
    {
        //Opening the quiz website along with the JWT Token
        Application.OpenURL(url+"?token="+GameEntry.jwt);
    }
}
