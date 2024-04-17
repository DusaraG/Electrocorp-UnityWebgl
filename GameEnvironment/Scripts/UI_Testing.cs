using UnityEngine;

public class UI_Testing : MonoBehaviour
{
    // Start is called before the first frame update
    public string titleString;
    public string key;
    [SerializeField] UI_InputWindow inputWindow;

    public void Show(){
        inputWindow.Show(titleString, "", key);
    }
    
}
