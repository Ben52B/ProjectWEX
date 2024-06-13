using UnityEngine;
using UnityEngine.UI;

public class UploadHandler : MonoBehaviour
{
    public InputField photoInputField;
    public InputField videoInputField;

    public void UploadPhoto()
    {
        string path = photoInputField.text;
        // Find the object to upload the photo to
        // For this example, we assume there's only one object with PhotoComponent
        PhotoComponent photoComponent = FindAnyObjectByType<PhotoComponent>();
        if (photoComponent != null)
        {
            photoComponent.UploadPhoto(path);
        }
        else
        {
            Debug.LogWarning("No PhotoComponent found in the scene.");
        }
    }

    public void UploadVideo()
    {
        string path = videoInputField.text;
        // Find the object to upload the video to
        // For this example, we assume there's only one object with VideoComponent
        VideoComponent videoComponent = FindAnyObjectByType<VideoComponent>();
        if (videoComponent != null)
        {
            videoComponent.UploadVideo(path);
        }
        else
        {
            Debug.LogWarning("No VideoComponent found in the scene.");
        }
    }
}
