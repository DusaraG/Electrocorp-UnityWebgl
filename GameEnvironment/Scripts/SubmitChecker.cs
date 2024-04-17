using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class SubmitChecker : MonoBehaviour
{
    public Button submitButton;
    public UI_InputWindow inputWindow;

    public TMPro.TextMeshProUGUI firstname; 
    public TMPro.TextMeshProUGUI lastname;
    public TMPro.TextMeshProUGUI username;
    public TMPro.TextMeshProUGUI nic;
    public TMPro.TextMeshProUGUI email;
    public TMPro.TextMeshProUGUI phoneNumber;

    private string storefirstname; 
    private string storelastname;
    private string storeusername;
    private string storenic;
    private string storeemail;
    private string storephoneNumber;

    private bool error;
    void Start()
    {
        // Add a listener to the submit button's onClick event
        submitButton.onClick.AddListener(OnSubmitButtonClick);
        storefirstname = firstname.text;
        storelastname = lastname.text;
        storeusername = username.text;
        storenic = nic.text;
        storeemail = email.text;
        storephoneNumber = phoneNumber.text;
    }

    void OnSubmitButtonClick()
    {
        string key = inputWindow.key;
        string value = inputWindow.inputField.text;
        StartCoroutine(SubmitData(key, value));
        Debug.Log("Submit button clicked!");
    }

    IEnumerator SubmitData(string key, string value)
    {
        Debug.Log("Key: " + key + ", Value: " + value);
        
        switch (key)
        {
            case "firstname":
                storefirstname = value;  
                Debug.Log("Object Name is firstname");
                break;
            case "lastname":
                storelastname = value;
                Debug.Log("Object Name is lastname");
                break;
            case "username":
                storeusername = value;
                Debug.Log("Object Name is username");
                break;
            case "nic":
                storenic = value;
                Debug.Log("Object Name is nic");
                break;
            case "phoneNumber":
                storephoneNumber = value;
                Debug.Log("Object Name is phoneNumber");
                break;
            case "email":
                storeemail = value;
                Debug.Log("Object Name is email");
                break;
            default:
                // Handle other cases
                Debug.Log("Object Name is something else");
                break;
        }
        
        UserData userData = new UserData();
        userData.firstname = storefirstname;
        userData.lastname = storelastname;
        userData.nic = storenic;
        userData.email = storeemail;
        userData.phoneNumber = storephoneNumber;

        string json = JsonUtility.ToJson(userData);
        Debug.Log(json);
        Debug.Log(APIHandler.token);
        using (UnityWebRequest www = UnityWebRequest.Put("http://20.15.114.131:8080/api/user/profile/update", json))
        {   
            www.SetRequestHeader("Authorization", "Bearer " + APIHandler.token);//if unauthorized error,check the jwt token.
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();
            yield return www;
            if (www.error != null)
            {
                Debug.Log(www.error);
                inputWindow.titleText.text =  "Error. Make sure input is valid.";

            }
            else{
                firstname.text = storefirstname;
                lastname.text = storelastname;
                username.text = storeusername;
                nic.text = storenic;
                email.text = storeemail;
                phoneNumber.text = storephoneNumber;

                inputWindow.Hide();
                
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
