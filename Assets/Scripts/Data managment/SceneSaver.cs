using UnityEngine;
using System.Collections.Generic;
using ES3Types;


public class SceneSaver : MonoBehaviour
{
    public List<GameObject> objectsToSave;

    public void SaveScene(string fileName)
    {
        SceneData sceneData = new SceneData();

        foreach (GameObject obj in objectsToSave)
        {
            ObjectData data = new ObjectData();
            /*
            data.prefabName = obj.name;
            data.position = obj.transform.position;
            data.rotation = obj.transform.rotation;
            data.scale = obj.transform.localScale;

            Animator animator = obj.GetComponent<Animator>();
            if (animator != null && animator.runtimeAnimatorController != null)
            {
                data.animationData = new AnimationData();
                AnimatorStateInfo animState = animator.GetCurrentAnimatorStateInfo(0);
                data.animationData.animationClipName = animState.IsName(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name) ? animator.GetCurrentAnimatorClipInfo(0)[0].clip.name : null;
                data.animationData.playbackTime = animState.normalizedTime;
                data.animationData.isPlaying = animState.normalizedTime < 1;
            }

            PhotoComponent photoComponent = obj.GetComponent<PhotoComponent>();
            if (photoComponent != null)
            {
                data.photoPath = photoComponent.photoPath;
            }

            VideoComponent videoComponent = obj.GetComponent<VideoComponent>();
            if (videoComponent != null)
            {
                data.videoPath = videoComponent.videoPath;
            }
            
            */
            sceneData.objects.Add(data);
        }

        string fullPath = Application.persistentDataPath + "/" + fileName;
        ES3.Save("sceneData", sceneData, fullPath);  // Use "sceneData" as key
        Debug.Log("Scene saved to " + fullPath);
    }
}
