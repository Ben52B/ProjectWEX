using UnityEngine;
using UnityEngine.Video;

public class VideoComponent : MonoBehaviour
{
    public string videoPath;
    public VideoPlayer videoPlayer;

    public void LoadVideo(string path)
    {
        videoPath = path;
        // Load the video from the path and play it
        videoPlayer.url = videoPath;
        videoPlayer.Prepare();
        videoPlayer.Play();
    }

    // Method to handle the video upload
    public void UploadVideo(string path)
    {
        videoPath = path;
        LoadVideo(videoPath);
    }
}
