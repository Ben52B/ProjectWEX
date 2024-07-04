using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using TMPro.Examples;
using JetBrains.Annotations;

[FirestoreData]
public class SaveSystem : MonoBehaviour
{
    private FirebaseFirestore firestore;
    /*
    public void Start() 
    {
        InitializeFirebase();
    }
    private async void InitializeFirebase()
    {
        var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
        if (dependencyStatus == DependencyStatus.Available)
        {
                firestore = FirebaseFirestore.DefaultInstance;
        }
        else
        {
             Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
        }
    }
    */
    public List<GameObject> objectsToSaves;
    public void SaveToCloud()
    {
        SceneData sceneData = new SceneData();
   
        foreach (GameObject obj in objectsToSaves)
        {
            ObjectData data = new ObjectData();
            data.prefabName = obj.name;
            /*
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
            Debug.Log(sceneData.objects[0].prefabName);
   
            
        }
        /*
        [FirestoreProperty]
        string objName;
        {
            get => sceneData.objects[0].prefabName;
            set => sceneData.objects[0].prefabName = value;
        }
        */
       // SaveData saveData = new(); this is the orginal line
       // firestore.Document($"save_data/0").SetAsync(saveData); this was always in comment

        DocumentReference docRef = firestore.Document("save_data/0");
        Debug.Log(docRef);
        // Set data to Firestore
        docRef.SetAsync(sceneData).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Data successfully written to Firestore!");
            }
            else
            {
                Debug.LogError($"Failed to write data to Firestore: {task.Exception}");
            }
        });
    }

    public void LoadFromCloud()
    {
        
    }

}
