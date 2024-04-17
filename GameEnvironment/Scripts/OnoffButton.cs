using UnityEngine;

public class OnoffButton : MonoBehaviour
{
    public GameObject[] objectsToDeactivate;
    public GameObject[] objectsToActivate;

    public void OnButtonClick()
    {
        // Deactivate objects
        foreach (GameObject obj in objectsToDeactivate)
        {
            obj.SetActive(false);
        }

        // Activate objects
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }
    }
}
