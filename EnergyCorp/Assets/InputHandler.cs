using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{

    public TMP_InputField firstname;
    public TMP_InputField lastname;
    public TMP_InputField nic;
    public TMP_InputField email;
    public TMP_InputField phoneNumber;
    public Button submitButton;
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
            www.SetRequestHeader("Authorization", "Bearer " + "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJvdmVyc2lnaHRfZzEyIiwiaWF0IjoxNzEzMDgxOTI1LCJleHAiOjE3MTMxMTc5MjV9.SmWBoS3IhP8hOEZ8IYcnj2IlqTG0HJa6fChDtSqHb55jAou6m5B4M8R9KP9KDkSxA2354YVFE3eESwoYO9rvJw");
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www;
            if (www.error != null)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.result);
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