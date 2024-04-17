using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class SingleInputHandler : MonoBehaviour
{
    public UI_InputWindow inputWindow;

    public TMPro.TextMeshProUGUI firstname; 
    public TMPro.TextMeshProUGUI lastname;
    public TMPro.TextMeshProUGUI username;
    public TMPro.TextMeshProUGUI nic;
    public TMPro.TextMeshProUGUI email;
    public TMPro.TextMeshProUGUI phoneNumber;

    public Button submitButton;
    public TMPro.TextMeshProUGUI errorText;
    
    public static SingleInputHandler Instance { get; private set; }
    public void Awake()
    {

    }
    public void Start()
    {
        submitButton.enabled = false;
    }
    public void Update()
    {   
        if(!(string.IsNullOrEmpty(firstname.text) || string.IsNullOrEmpty(lastname.text) || string.IsNullOrEmpty(nic.text) ||
            string.IsNullOrEmpty(email.text) || string.IsNullOrEmpty(phoneNumber.text))) 
         submitButton.enabled = true; 
        else
         submitButton.enabled = false;

    }
    public void Submit(string key, string value)
    {   
        
        StartCoroutine(SubmitData(key, value));
    }

    IEnumerator SubmitData(string key, string value)
    {
        switch (key)
        {
            case "firstname":
                firstname.text = value;  
                Debug.Log("Object Name is firstname");
                break;
            case "lastname":
                lastname.text = value;
                Debug.Log("Object Name is lastname");
                break;
            case "username":
                username.text = value;
                Debug.Log("Object Name is username");
                break;
            case "nic":
                nic.text = value;
                Debug.Log("Object Name is nic");
                break;
            case "phoneNumber":
                phoneNumber.text = value;
                Debug.Log("Object Name is phoneNumber");
                break;
            case "email":
                email.text = value;
                Debug.Log("Object Name is email");
                break;
            default:
                // Handle other cases
                Debug.Log("Object Name is something else");
                break;
        }
        
        UserData userData = new UserData();
        userData.firstname = firstname.text;
        userData.lastname = lastname.text;
        userData.nic = nic.text;
        userData.email = email.text;
        userData.phoneNumber = phoneNumber.text;

        string json = JsonUtility.ToJson(userData);
        Debug.Log(json);
        Debug.Log(APIHandler.token);
        using (UnityWebRequest www = UnityWebRequest.Put("http://20.15.114.131:8080/api/user/profile/update", json))
        {   
            www.SetRequestHeader("Authorization", "Bearer " + GameEntry.jwt);//if unauthorized error,check the jwt token.
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();
            yield return www;
            if (www.error != null)
            {
                Debug.Log(www.error);
                errorText.gameObject.SetActive(true);
                errorText.text = "Error: " + www.error;

            }
        }
    }

    [System.Serializable]
    public class UserData
    {
        public string firstname;
        public string lastname;
        public string nic;
        public string email;
        public string phoneNumber;
    }
}