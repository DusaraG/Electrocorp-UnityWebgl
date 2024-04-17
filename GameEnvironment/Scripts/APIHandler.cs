using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

[System.Serializable]
public class User
{
    public string firstname;
    public string lastname;
    public string username;
    public string nic;
    public string phoneNumber;
    public string email;
}

[System.Serializable]
public class Wrapper
{
    public User user;
}

public class APIHandler : MonoBehaviour
{
    private string apiurl = "http://20.15.114.131:8080/api/login";
    private string profileurl = "http://20.15.114.131:8080/api/user/profile/view";
    string jsonPayload = "{\"apiKey\":\"NjVkNDIyMjNmMjc3NmU3OTI5MWJmZGIxOjY1ZDQyMjIzZjI3NzZlNzkyOTFiZmRhNw\"}";
    private string jwtToken;
    public static string token;  
    public TextMeshProUGUI firstname;
    public TextMeshProUGUI lastname;
    public TextMeshProUGUI username;
    public TextMeshProUGUI nic;
    public TextMeshProUGUI phoneNumber;
    public TextMeshProUGUI email; 
    public TextMeshProUGUI MemberStatus;
    public static APIHandler Instance { get; private set; }

    void Start()
    {
        MemberStatus.text = "False";
        StartCoroutine(GetJWTToken());
    }

    IEnumerator GetJWTToken()
    {        
        var tokenRequest = new UnityWebRequest(apiurl, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
        tokenRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        tokenRequest.downloadHandler = new DownloadHandlerBuffer();
        tokenRequest.SetRequestHeader("Content-Type", "application/json");

        yield return tokenRequest.SendWebRequest();

        if (tokenRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Token request failed: " + tokenRequest.error);
            yield break;
        }
        MemberStatus.text = "True";

        jwtToken = tokenRequest.downloadHandler.text;
        TokenClass obj = JsonUtility.FromJson<TokenClass>(jwtToken);
        token = obj.token;
        Debug.Log("JWT Token: " + token);

        //After getting the JWT token, use it to fetch player details
        StartCoroutine(GetPlayerDetails());
    }

    IEnumerator GetPlayerDetails()
    {
        var playerDetailsRequest = new UnityWebRequest(profileurl, "GET");
        playerDetailsRequest.SetRequestHeader("Authorization", "Bearer " + token);
        playerDetailsRequest.downloadHandler = new DownloadHandlerBuffer();
        yield return playerDetailsRequest.SendWebRequest();

        if (playerDetailsRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Player details request failed: " + playerDetailsRequest.error);
            yield break;
        }
        
        string jsonString = playerDetailsRequest.downloadHandler.text;
        Wrapper wrapper = JsonUtility.FromJson<Wrapper>(jsonString);        

        if (wrapper != null && wrapper.user != null)
        {
            firstname.text =  wrapper.user.firstname;
            lastname.text =  wrapper.user.lastname;
            username.text =  wrapper.user.username;
            nic.text =  wrapper.user.nic;
            phoneNumber.text =  wrapper.user.phoneNumber;
            email.text = wrapper.user.email;
        }

    }
}
