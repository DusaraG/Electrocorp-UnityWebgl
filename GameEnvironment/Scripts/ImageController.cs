using UnityEngine;

public class ImageController : MonoBehaviour
{
    public GameObject imageToDeactivate;
    public GameObject imageToActivate;

    private void Start()
    {
        // If imageToDeactivate or imageToActivate are not set in the Inspector, try to find them automatically
        if (imageToDeactivate == null)
        {
            imageToDeactivate = GameObject.Find("ImageToDeactivateName");
        }
        if (imageToActivate == null)
        {
            imageToActivate = GameObject.Find("ImageToActivateName");
        }
    }

    public void ToggleImages()
    {
        if (imageToDeactivate != null && imageToActivate != null)
        {
            imageToDeactivate.SetActive(false); // Deactivate the first image
            imageToActivate.SetActive(true);    // Activate the second image
        }
        else
        {
            Debug.LogError("One or both images are not assigned!");
        }
    }
}