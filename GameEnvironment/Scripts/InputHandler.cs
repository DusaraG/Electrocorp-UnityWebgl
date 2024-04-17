using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    public InputField firstname;
    public InputField lastname;
    public InputField nic;
    public InputField email;
    public InputField phoneNumber;
    public Button submitButton;
    public TMPro.TextMeshProUGUI errorText;
    private bool iscompleted = false;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void Start()
    {
        submitButton.enabled = true;
    }
    public void Submit()
    {
        StartCoroutine(Isvalid());
        if (iscompleted){
            StartCoroutine(SubmitData());
        }       
    }

    //Handling incomplete data entries
    IEnumerator Isvalid(){
        if(string.IsNullOrEmpty(firstname.text)){
            errorText.text = "First Name is Empty";
        }
        else if(string.IsNullOrEmpty(lastname.text)){
            errorText.text = "Last Name is Empty";
        }
        else if (string.IsNullOrEmpty(nic.text)){
            errorText.text = "NIC is Empty";
        }
        else if (!((nic.text.Length == 12) || (nic.text.Length == 9 && (nic.text.EndsWith("v") || nic.text.EndsWith("V"))))){
            errorText.text = "NIC is Invalid";
        }
        else if(string.IsNullOrEmpty(email.text)){
            errorText.text = "Email is Empty";
        }
        else if(string.IsNullOrEmpty(phoneNumber.text)){
            errorText.text = "Phone Number is Empty";
        }
        else if (phoneNumber.text.Length != 10){
            errorText.text = "Phone Number is Invalid";
        }
        else{
            iscompleted = true;
            errorText.text = "All Fields are Valid";
        }
        yield return null;
    }

    //Saving the data in the backend
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
                //errorText.text = "Error: " + www.error;
                errorText.text = "Error occured. Please check inputs.";
            }
            else
            {   
                audioManager.PlaySFX(audioManager.button);

                //After submitting data, load the ground floor of the game
                SceneManager.LoadSceneAsync(3);
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