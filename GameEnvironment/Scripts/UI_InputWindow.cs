using TMPro;
using UnityEngine;

public class UI_InputWindow : MonoBehaviour
{
    public string key;
    public TextMeshProUGUI titleText;
    public TMP_InputField inputField;
    
    private void Awake(){
        titleText = transform.Find("Title Text").GetComponent<TextMeshProUGUI>();
        inputField = transform.Find("InputField").GetComponent<TMP_InputField>();
        Hide();
    }
    // Start is called before the first frame update
    public void Show(string titleString, string inputString, string key)
    {
        gameObject.SetActive(true);
        titleText.text = titleString;
        inputField.text = inputString;
        this.key = key;
        
    }
    

    // Update is called once per frame
    public void Hide()
    {
        gameObject.SetActive(false);
        
    }
}
