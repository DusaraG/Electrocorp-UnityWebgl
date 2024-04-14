using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameEntry : MonoBehaviour
{   
    public Button newGame;
    public Button continueGame;
    public static string jwt = null;
    void Start()
    {
        newGame.interactable = false;
        continueGame.interactable = false;
        StartCoroutine(GetJwt());

        if (jwt!= null)
        {
            newGame.interactable = true;
            continueGame.interactable = true;
            newGame.onClick.AddListener(StartNewGame);
            continueGame.onClick.AddListener(ContinueGame);
        }

    
    }
    IEnumerator GetJwt() { 
        using (UnityWebRequest webRequest = UnityWebRequest.Get("http://localhost:8080/get_jwt"))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);                
            }
            else
            {
                string response = webRequest.downloadHandler.text;
                Debug.Log(response);
                TokenData data = JsonUtility.FromJson<TokenData>(response);
                jwt = data.token;
            }
        }
    }
    
    public void StartNewGame()
    {         
        PlayerPrefs.SetInt("Continue", 0); // Set Continue to 0 for new game
        SceneManager.LoadScene("Player profile"); // Load your game scene
    }

    public void ContinueGame()
    {
        PlayerPrefs.SetInt("Continue", 1); // Set Continue to 1 for continuing game
        SceneManager.LoadScene("Player profile"); // Load your game scene
    }
}
public class TokenData
{
    public string token;
}