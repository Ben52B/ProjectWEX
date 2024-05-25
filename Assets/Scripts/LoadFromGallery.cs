using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEditor.Profiling;
using Unity.VisualScripting;

public class LoadFromGallery : MonoBehaviour
{
    public string FinalPath;
    public void LoadFile()
    {
        //string FileType = NativeFilePicker.ConvertExtensionToFileType("*");
        string FileType = "*/*";
        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
        {
            if (path == null)
            {
                Debug.Log("Operation cancelled");
            }
            else
            {
                FinalPath=path;
                Debug.Log("Picked file: " + FinalPath);
                StartCoroutine("LoadTexture");
            }
        }, new string[]{FileType});
    }

    public void SaveFile()
    {
        string filePath = Path.Combine(Application.temporaryCachePath,"test.txt");
        File.WriteAllText(filePath,"hello world");

        NativeFilePicker.Permission permission = NativeFilePicker.ExportFile(filePath, (success) => Debug.Log("File exported: " + success));
    }
    
    public RawImage testimage;
    IEnumerator LoadTexture()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(FinalPath);
        www.SendWebRequest();
        while(!www.isDone)
            yield return null;
        
        Texture2D upladedImage = DownloadHandlerTexture.GetContent(www);
        Debug.Log(DownloadHandlerTexture.GetContent(www).GetType());
        testimage.GetComponent<RawImage>().texture = upladedImage;

    }
}
