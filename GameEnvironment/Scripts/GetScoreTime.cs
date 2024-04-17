using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GetScoreTime : MonoBehaviour
{
    public Text scoreText;
    public Text timeText;
    public Text boostText;
    public int score_val;
    public int time_val;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        StartCoroutine(GetScoreAndTime());
    }

    //Getting the time and score from the quiz
    IEnumerator GetScoreAndTime()
    {
        using (UnityWebRequest www = UnityWebRequest.Post("https://firefly-fluent-personally.ngrok-free.app/unity/get_score", "1", "application/json"))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error: " + www.error);
            }
            else
            {
                string response = www.downloadHandler.text;
                Debug.Log("Response: " + response);
                ScoreTimeData data = JsonUtility.FromJson<ScoreTimeData>(response);
                UpdateUI(data.score, data.time);
            }
        }
    }

    void UpdateUI(string score, string time)
    {
        scoreText.text = "Score: " + score; //Displaying score
        timeText.text = "Time: " + time; //Displaying time taken

        score_val = int.Parse(score);
        time_val = int.Parse(time);

        //Calculating boost using the time taken and overall score
        float scoreBoost = score_val * 5;
        float timeBoost = Mathf.Max(0, 20 - time_val);
        float totalBoost = scoreBoost + timeBoost;

        //Displaying the boost
        boostText.text = "Boost: " + totalBoost.ToString("F2");

        //Storing the boost value to be included in the energy bar
        PlayerPrefs.SetFloat("TotalBoost", totalBoost);
    }

    public void PlayerProfile()
    {
        audioManager.PlaySFX(audioManager.button);
        //Loading the scene to input player details
        SceneManager.LoadSceneAsync(2);
    }

    [System.Serializable]
    class ScoreTimeData
    {
        public string score;
        public string time;
    }
}
