using UnityEngine;
using UnityEngine.UI;

public class PhotoComponent : MonoBehaviour
{
    public string photoPath;
    public Image photoDisplay;

    public void LoadPhoto(string path)
    {
        photoPath = path;
        // Load the photo from the path and display it
        byte[] fileData = System.IO.File.ReadAllBytes(photoPath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);
        photoDisplay.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    // Method to handle the photo upload
    public void UploadPhoto(string path)
    {
        photoPath = path;
        LoadPhoto(photoPath);
    }
}
