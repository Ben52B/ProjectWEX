using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class GalleryPicker : MonoBehaviour
{
    public Button pickImageButton;
    public Button pickVideoButton;

    void Start()
    {
        pickImageButton.onClick.AddListener(OpenImageGallery);
        pickVideoButton.onClick.AddListener(OpenVideoGallery);
    }

    void OpenImageGallery()
    {
        
        // Request permission to access gallery
        NativeGallery.Permission permission = NativeGallery.RequestPermission(NativeGallery.PermissionType.Read, NativeGallery.MediaType.Image);

        if (permission == NativeGallery.Permission.Granted)
        {
            // Open gallery to pick an image
            NativeGallery.Permission mediaPermission = NativeGallery.RequestPermission(NativeGallery.PermissionType.Read, NativeGallery.MediaType.Image);
            if (mediaPermission == NativeGallery.Permission.Granted)
            {
                NativeGallery.GetImageFromGallery((path) =>
                {
                    if (path != null)
                    {
                        // Find the "Video" GameObject under the "Board" GameObject
                        GameObject videoObject = FindVideoObject();

                        if (videoObject != null)
                        {
                            StartCoroutine(LoadImage(path, videoObject));
                        }
                        else
                        {
                            Debug.LogError("Video GameObject not found under Board GameObject.");
                        }
                    }
                }, "Select an image");
            }
            else
            {
                Debug.LogError("Permission to access gallery denied.");
            }
        }
        else
        {
            Debug.LogError("Permission to access gallery denied.");
        }

    }

    void OpenVideoGallery()
    {
        // Request permission to access gallery
        NativeGallery.Permission permission = NativeGallery.RequestPermission(NativeGallery.PermissionType.Read, NativeGallery.MediaType.Video);
        if (permission == NativeGallery.Permission.Granted)
        {
            // Open gallery to pick a video
            NativeGallery.Permission mediaPermission = NativeGallery.RequestPermission(NativeGallery.PermissionType.Read, NativeGallery.MediaType.Video);
            if (mediaPermission == NativeGallery.Permission.Granted)
            {
                NativeGallery.GetVideoFromGallery((path) =>
                {
                    if (path != null)
                    {
                        // Find the "Video" GameObject under the "Board" GameObject
                        GameObject videoObject = FindVideoObject();

                        if (videoObject != null)
                        {
                            StartCoroutine(LoadVideo(path, videoObject));
                        }
                        else
                        {
                            Debug.LogError("Video GameObject not found under Board GameObject.");
                        }
                    }
                }, "Select a video");
            }
            else
            {
                Debug.LogError("Permission to access gallery denied.");
            }
        }
        else
        {
            Debug.LogError("Permission to access gallery denied.");
        }
    }

    GameObject FindVideoObject()
    {
        // Find the "Board" GameObject in the scene
        GameObject boardObject = GameObject.Find("Board");

        if (boardObject != null)
        {
            // Search for the "Video" GameObject under the "Board" GameObject
            Transform videoTransform = FindChildRecursively(boardObject.transform, "Video");

            if (videoTransform != null)
            {
                // Return the GameObject reference
                return videoTransform.gameObject;
            }
        }

        return null;
    }

    IEnumerator LoadImage(string path, GameObject videoObject)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture("file://" + path))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(www);

                var renderer = videoObject.GetComponent<Renderer>();
                renderer.material.mainTexture = texture;
            }
            else
            {
                Debug.LogError("Failed to load image: " + www.error);
            }
        }
    }

    IEnumerator LoadVideo(string path, GameObject videoObject)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + path, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                var videoPlayer = videoObject.GetComponent<UnityEngine.Video.VideoPlayer>(); // Ensure you have VideoPlayer component attached to your "Video" GameObject
                videoPlayer.url = path;
                videoPlayer.Play();
            }
            else
            {
                Debug.LogError("Failed to load video: " + www.error);
            }
        }
    }

    Transform FindChildRecursively(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                return child;
            }
            else
            {
                Transform found = FindChildRecursively(child, name);
                if (found != null)
                    return found;
            }
        }
        return null;
    }
}
