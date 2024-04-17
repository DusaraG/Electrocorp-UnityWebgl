using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickHandler : MonoBehaviour
{
    public string key;
    public UI_InputWindow buttonController;
    public TextMeshProUGUI buttonText;

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        buttonController.Show(buttonText.text, "input", key);
    }
}