using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{

    public TMP_InputField firstname;
    public TMP_InputField lastname;
    public TMP_InputField nic;
    public TMP_InputField email;
    public TMP_InputField phoneNumber;
    public Button submitButton;
    public TMPro.TextMeshProUGUI errorText;
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
    public void Submit()
    {   
        
        StartCoroutine(SubmitData());
    }

    IEnumerator SubmitData()
    {
        UserData userData = new UserData();
        userData.firstname = firstname.text;
        userData.lastname = lastname.text;
        userData.nic = nic.text;
        userData.email = email.text;
        userData.phoneNumber = phoneNumber.text;

        string json = JsonUtility.ToJson(userData);
        Debug.Log(json);
        Debug.Log(GameEntry.jwt);
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
            else
            {   
                //now the player profile is properly filled,redirect to the questionnare
                SceneManager.LoadScene("Untitled");
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
//unity editor version 2022.3.22f1