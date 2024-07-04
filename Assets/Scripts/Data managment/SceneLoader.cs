
/*
sing UnityEngine;
using System.Collections.Generic;
using System.Linq;
using ES3Types;

public class SceneLoader : MonoBehaviour
{
    public List<GameObject> availablePrefabs;

    public void LoadScene(string fileName)
    {
        
        string fullPath = Application.persistentDataPath + "/" + fileName;
        Debug.Log(fullPath);
        if (ES3.FileExists(fullPath))
        {
            Debug.Log("Loading scene from " + fullPath);
            SceneData sceneData = ES3.Load<SceneData>("sceneData", fullPath);  // Use "sceneData" as key

            foreach (ObjectData data in sceneData.objects)
            {
                GameObject prefab = availablePrefabs.Find(p => p.name == data.prefabName);
                if (prefab != null)
                {
                    GameObject obj = Instantiate(prefab, data.position, data.rotation);
                    obj.transform.localScale = data.scale;

                    if (data.animationData != null)
                    {
                        Animator animator = obj.GetComponent<Animator>();
                        if (animator != null)
                        {
                            AnimationClip clip = animator.runtimeAnimatorController.animationClips
                                .FirstOrDefault(c => c.name == data.animationData.animationClipName);
                            if (clip != null)
                            {
                                animator.Play(clip.name, 0, data.animationData.playbackTime);
                                if (!data.animationData.isPlaying)
                                {
                                    animator.speed = 0; // Pause the animation
                                }
                            }
                        }
                    }

                    PhotoComponent photoComponent = obj.GetComponent<PhotoComponent>();
                    if (photoComponent != null)
                    {
                        photoComponent.LoadPhoto(data.photoPath);
                    }

                    VideoComponent videoComponent = obj.GetComponent<VideoComponent>();
                    if (videoComponent != null)
                    {
                        videoComponent.LoadVideo(data.videoPath);
                    }
                }
                else
                {
                    Debug.LogWarning("Prefab not found: " + data.prefabName);
                }
            }
        }
        else
        {
            Debug.LogError("Save file not found: " + fullPath);
        }
    }
}
*/