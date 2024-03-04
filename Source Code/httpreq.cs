using System.Collections;
using System.Globalization;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class TokenClass
{
    public string token;
}
public class httpreq : MonoBehaviour
{   public bool canAccessGame = false; 
    public TextMeshProUGUI todisplay;
    IEnumerator Start()
    {
        string url = "http://20.15.114.131:8080/api/login";
        string jsonPayload = "{\"apiKey\":\"NjVkNDIyMjNmMjc3NmU3OTI5MWJmZGIxOjY1ZDQyMjIzZjI3NzZlNzkyOTFiZmRhNw\"}"; // replace with your JSON payload

        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
 
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {   canAccessGame = true;
            Debug.Log("Response: " + request.downloadHandler.text);
            TokenClass obj = JsonUtility.FromJson<TokenClass>(request.downloadHandler.text);
            Debug.Log("The value of 'token' is: " + obj.token);
            todisplay.text = " Play Game";
        }
        else
        {
            Debug.Log("Error: " + request.error);
            todisplay.text = "Unauthorized";
        }
    }
}
