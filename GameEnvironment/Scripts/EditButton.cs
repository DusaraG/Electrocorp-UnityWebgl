using UnityEngine;

public class EditButton : MonoBehaviour
{
    // Reference to the objects to toggle activation
    public GameObject[] objectsToToggle;

    void Start()
    {
        // Deactivate each object in the array initially
        foreach (GameObject obj in objectsToToggle)
        {
            obj.SetActive(false);
        }

        // Get reference to the Button component
        UnityEngine.UI.Button button = GetComponent<UnityEngine.UI.Button>();

        // Add a listener to the button's onClick event
        button.onClick.AddListener(ToggleObjectActivation);
    }

    void ToggleObjectActivation()
    {
        // Toggle activation status for each object in the array
        foreach (GameObject obj in objectsToToggle)
        {
            obj.SetActive(!obj.activeSelf);
        }
    }
}