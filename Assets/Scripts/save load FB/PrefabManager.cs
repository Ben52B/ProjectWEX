using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections.Generic;
using System.IO;

public class PrefabManager : MonoBehaviour
{
    public List<GameObject> prefabs; // List of all available prefabs
    public InputField objectNameInput;
    public Button saveButton;
    public Button loadButton;

    private GameObject boardObject; // Reference to the "Board" GameObject in the scene
    private Transform boardTransform; // Transform of the "Board" GameObject
    private GameObject instantiatedObject;

    void Start()
    {
        saveButton.onClick.AddListener(SaveObject);
        loadButton.onClick.AddListener(LoadObject);

        // Find the "Board" GameObject in the scene
        boardObject = GameObject.Find("Board");
        if (boardObject == null)
        {
            Debug.LogError("Board GameObject not found in the scene.");
            return;
        }

        // Get the transform of the "Board" GameObject
        boardTransform = boardObject.transform;
    }

    void SaveObject()
    {
        string objectName = objectNameInput.text;
        if (string.IsNullOrEmpty(objectName))
        {
            Debug.LogError("Object name is empty.");
            return;
        }

        // Find the template under "Board" GameObject
        Transform templateTransform = FindTemplateUnderBoard();

        if (templateTransform == null)
        {
            Debug.LogError("No template found under 'Board' GameObject.");
            return;
        }

        // Get media data from the "Video" child GameObject
        Transform videoTransform = templateTransform.Find("Video");
        if (videoTransform == null)
        {
            Debug.LogError("No 'Video' child GameObject found.");
            return;
        }

        byte[] mediaData = GetMediaDataFromVideo(videoTransform);
        if (mediaData == null)
        {
            Debug.LogError("Failed to get media data.");
            return;
        }

        // Save the object's details
        Dictionary<string, object> objectDetails = new Dictionary<string, object>
        {
            { "templateName", templateTransform.gameObject.name }, // Save the template name
            { "position", new Dictionary<string, float> { { "x", templateTransform.position.x }, { "y", templateTransform.position.y }, { "z", templateTransform.position.z } } },
            { "rotation", new Dictionary<string, float> { { "x", templateTransform.rotation.x }, { "y", templateTransform.rotation.y }, { "z", templateTransform.rotation.z }, { "w", templateTransform.rotation.w } } },
            // Add other properties as needed
        };

        FirebaseManager.Instance.SaveObjectDetails(objectName, objectDetails, mediaData, "video.mp4", 
            () => Debug.Log("Save successful."), 
            (error) => Debug.LogError("Failed to save: " + error));
    }

    void LoadObject()
    {
        string objectName = objectNameInput.text;
        if (string.IsNullOrEmpty(objectName))
        {
            Debug.LogError("Object name is empty.");
            return;
        }

        // Call FirebaseManager to load object details
        FirebaseManager.Instance.LoadObjectDetails(objectName, objectDetails =>
        {
            if (objectDetails != null)
            {
                // Destroy the existing instantiated object if it exists
                if (instantiatedObject != null)
                {
                    Destroy(instantiatedObject);
                }

                string templateName = objectDetails["templateName"].ToString();
                GameObject prefab = prefabs.Find(p => p.name == templateName);

                if (prefab == null)
                {
                    Debug.LogError("Prefab not found.");
                    return;
                }

                // Instantiate the prefab
                instantiatedObject = Instantiate(prefab);

                // Name the instantiated object as the template name
                instantiatedObject.name = templateName;

                // Parent the instantiated object under "Board"
                instantiatedObject.transform.SetParent(boardTransform);

                // Apply the details to the instantiated object
                var position = (Dictionary<string, object>)objectDetails["position"];
                instantiatedObject.transform.position = new Vector3(
                    float.Parse(position["x"].ToString()),
                    float.Parse(position["y"].ToString()),
                    float.Parse(position["z"].ToString())
                );

                var rotation = (Dictionary<string, object>)objectDetails["rotation"];
                instantiatedObject.transform.rotation = new Quaternion(
                    float.Parse(rotation["x"].ToString()),
                    float.Parse(rotation["y"].ToString()),
                    float.Parse(rotation["z"].ToString()),
                    float.Parse(rotation["w"].ToString())
                );

                // Load and apply media
                FirebaseManager.Instance.DownloadMedia(objectName, "video.mp4", mediaData =>
                {
                    ApplyMediaToVideoChild(instantiatedObject.transform.Find("Video"), mediaData);
                    Debug.Log("Object details loaded and media applied successfully.");
                },
                error => Debug.LogError("Failed to load media: " + error));
            }
            else
            {
                Debug.LogError("Failed to load object details.");
            }
        },
        error => Debug.LogError("Failed to load object details: " + error));
    }

    Transform FindTemplateUnderBoard()
    {
        // Iterate through children of "Board" to find the template
        foreach (Transform child in boardTransform)
        {
            // Check if the child is one of the prefabs
            if (prefabs.Exists(p => p.name == child.gameObject.name))
            {
                return child;
            }
        }

        return null;
    }

    byte[] GetMediaDataFromVideo(Transform videoTransform)
    {
        VideoPlayer videoPlayer = videoTransform.GetComponent<VideoPlayer>();
        if (videoPlayer == null || string.IsNullOrEmpty(videoPlayer.url))
        {
            Debug.LogError("No VideoPlayer component or video URL found.");
            return null;
        }

        string videoPath = videoPlayer.url;
        return File.ReadAllBytes(videoPath); // Assumes the video is a local file
    }

    void ApplyMediaToVideoChild(Transform videoTransform, byte[] mediaData)
    {
        VideoPlayer videoPlayer = videoTransform.GetComponent<VideoPlayer>();
        if (videoPlayer == null)
        {
            Debug.LogError("No VideoPlayer component found.");
            return;
        }

        string videoPath = Path.Combine(Application.persistentDataPath, "loadedVideo.mp4");
        File.WriteAllBytes(videoPath, mediaData);

        videoPlayer.url = videoPath;
        videoPlayer.Play();
    }
}
