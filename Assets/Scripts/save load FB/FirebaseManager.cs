using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    private static FirebaseManager instance;
    public static FirebaseManager Instance => instance;

    private FirebaseFirestore db;
    private FirebaseStorage storage;
    private StorageReference storageRef;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeFirebase();
    }

    private void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            if (task.Result != DependencyStatus.Available)
            {
                Debug.LogError($"Failed to initialize Firebase: {task.Result}");
                return;
            }

            db = FirebaseFirestore.DefaultInstance;
            storage = FirebaseStorage.DefaultInstance;
            storageRef = storage.GetReferenceFromUrl("gs://firestoretest-6af5c.appspot.com");

            Debug.Log("Firebase initialized successfully.");
        });
    }

    public void SaveObjectDetails(string objectName, Dictionary<string, object> objectDetails, byte[] mediaData, string mediaFileName, Action onSuccess, Action<string> onError)
    {
        db.Collection("objects").Document(objectName).SetAsync(objectDetails)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    onError?.Invoke(task.Exception != null ? task.Exception.Message : "Unknown error");
                }
                else
                {
                    Debug.Log($"Object '{objectName}' details saved successfully.");

                    // Example: Upload media file after object details are saved
                    UploadMedia(objectName, mediaData, mediaFileName, onSuccess, onError);
                }
            });
    }

    private void UploadMedia(string objectName, byte[] mediaData, string mediaFileName, Action onSuccess, Action<string> onError)
    {
        StorageReference mediaRef = storageRef.Child($"{objectName}/{mediaFileName}");
        mediaRef.PutBytesAsync(mediaData)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    onError?.Invoke(task.Exception != null ? task.Exception.Message : "Unknown error");
                }
                else
                {
                    Debug.Log($"Media '{mediaFileName}' for '{objectName}' uploaded successfully.");
                    onSuccess?.Invoke();
                }
            });
    }

    public void LoadObjectDetails(string objectName, Action<Dictionary<string, object>> onDataReceived, Action<string> onError)
    {
        db.Collection("objects").Document(objectName).GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    onError?.Invoke(task.Exception != null ? task.Exception.Message : "Unknown error");
                }
                else if (task.IsCompleted)
                {
                    if (task.Result.Exists)
                    {
                        Dictionary<string, object> data = task.Result.ToDictionary();
                        onDataReceived?.Invoke(data);
                    }
                    else
                    {
                        onError?.Invoke($"Object '{objectName}' not found.");
                    }
                }
            });
    }

    public void DownloadMedia(string objectName, string mediaFileName, Action<byte[]> onMediaDownloaded, Action<string> onError)
    {
        StorageReference mediaRef = storageRef.Child($"{objectName}/{mediaFileName}");
        mediaRef.GetBytesAsync(10485760) // 10MB max download size
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    onError?.Invoke(task.Exception != null ? task.Exception.Message : "Unknown error");
                }
                else
                {
                    onMediaDownloaded?.Invoke(task.Result);
                }
            });
    }
}
